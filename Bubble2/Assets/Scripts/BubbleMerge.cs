using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMerge : MonoBehaviour
{
    public GameObject contentBubblePrefab; // 用于生成新的Content类型泡泡的Prefab
    public float radius = 0.5f;                  // 泡泡的半径
    public float mergeCooldown = 0.5f; // 冷却时间，单位为秒
    public float mergeAnimationDuration = 2f; // 融合动画持续时间

    public float initialSize = 0.01f; // 初始尺寸

    private BubbleDataCollection.BubbleDataType _bubbleDataType;
    private bool isCoolingDown = false; // 用于标记是否在冷却中
    
    private Collider2D[] newBubbleChildColliders; // 新泡泡所有子物体的碰撞器
    private Rigidbody2D[] newBubbleChildRigidbodies; //  新泡泡所有子物体的刚体
    
    public LayerMask collisionMask; // 检测重叠的层
    void Start()
    {
        _bubbleDataType = GetComponent<BubbleStatistic>().selectedDataType;
        // 获取新泡泡的所有刚体和碰撞体
        GetNewBubbleCollidersAndRigidbodies(this.gameObject);
        // 暂时禁用新泡泡物理
         DisableNewBubblePhysics();
        // 设置当前泡泡的初始尺寸
        gameObject.transform.localScale = this.transform.localScale * initialSize;
        BubbleMerge newBubbleComponent = gameObject.GetComponent<BubbleMerge>();

        // 确保新泡泡的 BubbleMovement 脚本启用
        BubbleMovement movementScript = gameObject.GetComponent<BubbleMovement>();
        if (movementScript != null)
        {
            movementScript.enabled = true; // 启用脚本
        }
        // 播放融合放大动画
        StartCoroutine(PlayMergeAnimation(gameObject, radius));
    }

    public void OnChildCollision(BubbleMerge otherBubble)
    {
        if (otherBubble != null && !isCoolingDown && !otherBubble.isCoolingDown)
        {
            // 通过 ID 比较，确保只执行一次融合
            if (this.GetInstanceID() < otherBubble.GetInstanceID())
            {
                HandleCollision(otherBubble);
            }
        }

    }
    
    public void HandleCollision(BubbleMerge otherBubble)
    {
        if (this._bubbleDataType == otherBubble._bubbleDataType)
        {
            // 相同种类，物理反弹（默认由物理引擎处理）
            Debug.Log("Bubbles of the same type collided!");
        }
        else
        {
            // 触发Numeric泡泡对Content泡泡的效果
            TriggerBubbleEffect(otherBubble);
            // 执行融合逻辑
            MergeBubbles(otherBubble);
            Debug.Log("Bubbles of different types collided!");
        }
    }

    private void TriggerBubbleEffect(BubbleMerge otherBubble)
    {
        // 获取泡泡数据
        if( this.gameObject.GetComponent<BubbleStatistic>().selectedDataType == 
            BubbleDataCollection.BubbleDataType.Content)
        {
            ContentBubbleData data1;
            NumericBubbleData data2;
            data1 = this.gameObject.GetComponent<BubbleStatistic>().contentBubbleData;
            data2 = otherBubble.gameObject.GetComponent<BubbleStatistic>().numericBubbleData;
            // 根据 NumericData 的操作类型修改 ContentData
            ApplyNumericEffect(data1, data2);
        }
        else if (this.gameObject.GetComponent<BubbleStatistic>().selectedDataType ==
                 BubbleDataCollection.BubbleDataType.Numeric)
        {
            NumericBubbleData data1;
            ContentBubbleData data2;
            data1 = this.gameObject.GetComponent<BubbleStatistic>().numericBubbleData;
            data2 = otherBubble.gameObject.GetComponent<BubbleStatistic>().contentBubbleData;
            // 根据 NumericData 的操作类型修改 ContentData
            ApplyNumericEffect(data2, data1);
        }
    }
    private void ApplyNumericEffect(ContentBubbleData contentData, NumericBubbleData numericData)
    {
        switch (numericData.operationType)
        {
            case OperationType.Add:
                contentData.value1 += numericData.value1;
                contentData.value2 += numericData.value2;
                contentData.value3 += numericData.value3;
                break;
            case OperationType.Subtract:
                contentData.value1 -= numericData.value1;
                contentData.value2 -= numericData.value2;
                contentData.value3 -= numericData.value3;
                break;
            case OperationType.Multiply:
                contentData.value1 *= numericData.value1;
                contentData.value2 *= numericData.value2;
                contentData.value3 *= numericData.value3;
                break;
            case OperationType.Divide:
                contentData.value1 = numericData.value1 != 0 ? contentData.value1 / numericData.value1 : contentData.value1;
                contentData.value2 = numericData.value2 != 0 ? contentData.value2 / numericData.value2 : contentData.value2;
                contentData.value3 = numericData.value3 != 0 ? contentData.value3 / numericData.value3 : contentData.value3;
                break;
        }
    }
    void MergeBubbles(BubbleMerge otherBubble)
    {
        print("Merge Triggered");
        // 设置冷却状态，避免短时间内重复融合
        this.isCoolingDown = true;
        otherBubble.isCoolingDown = true;
        
        // 计算新的半径 (假设面积 = 半径^2，则面积相加后求新半径)
        float newRadius = Mathf.Sqrt(this.radius * this.radius + otherBubble.radius * otherBubble.radius);

        // 计算新的位置（加权平均位置）
        Vector3 newPosition = (this.transform.position * this.radius + otherBubble.transform.position * otherBubble.radius) / (this.radius + otherBubble.radius);

        // 检测生成位置是否安全
        newPosition = GetSafeSpawnPosition(newPosition, newRadius);

        if (this._bubbleDataType == BubbleDataCollection.BubbleDataType.Content ||
            otherBubble._bubbleDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            // 如果一个泡泡是Content类型，另一个是Numeric类型，则生成一个Content类型的新泡泡
            GameObject newBubble = Instantiate(contentBubblePrefab, newPosition, Quaternion.identity);
        }

        // 销毁原有泡泡
        Destroy(this.gameObject);
        Destroy(otherBubble.gameObject);
        // 恢复冷却状态
        Invoke(nameof(ResetCooldown), mergeCooldown);
    }

    private void GetNewBubbleCollidersAndRigidbodies(GameObject newBubble)
    {
        newBubbleChildColliders = newBubble.GetComponentsInChildren<Collider2D>();
        newBubbleChildRigidbodies = newBubble.GetComponentsInChildren<Rigidbody2D>();
    }

    // 检测生成位置是否安全并返回安全位置
    Vector3 GetSafeSpawnPosition(Vector3 initialPosition, float radius)
    {
        int maxAttempts = 10; // 最大尝试次数
        float offsetStep = 0.1f; // 每次偏移的距离
        int attempt = 0;

        Vector3 safePosition = initialPosition;

        while (attempt < maxAttempts)
        {
            // 检测目标范围内是否有碰撞体
            Collider2D hit = Physics2D.OverlapCircle(safePosition, radius);
            if (hit == null)
            {
                // 没有障碍物，返回安全位置
                return safePosition;
            }

            // 如果有障碍物，偏移位置
            safePosition = initialPosition + (Vector3)(Random.insideUnitCircle * offsetStep);
            attempt++;
        }

        // 如果尝试多次仍无法找到安全位置，返回初始位置
        Debug.LogWarning("Failed to find a safe spawn position after multiple attempts.");
        return initialPosition;
    }
    
    void ResetCooldown()
    {
        isCoolingDown = false;
    }
    
    System.Collections.IEnumerator PlayMergeAnimation(GameObject newBubble, float targetRadius)
    {
        print("MergeAnimation Triggered");
        float elapsedTime = 0f;
        Vector3 startScale = newBubble.transform.localScale;
        Vector3 targetScale = Vector3.one * targetRadius * 2;

        while (elapsedTime < mergeAnimationDuration)
        {
            float t = elapsedTime / mergeAnimationDuration;
            newBubble.transform.localScale = Vector3.Lerp(startScale, targetScale, t); // 平滑缩放
            // 检测并调整位置
            AvoidOverlap();
            //print("newBubble localScale: "+newBubble.transform.localScale);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        newBubble.transform.localScale = targetScale;
        // 启用新泡泡物理
         EnableNewBubblePhysics();
    }
    
    void AvoidOverlap()
    {
        // 获取泡泡当前的半径
        float currentRadius = transform.localScale.x / 2f;

        // 检测是否与其他物体重叠
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(transform.position, currentRadius, collisionMask);
        foreach (var overlap in overlaps)
        {
            if (overlap.gameObject != gameObject) // 排除自己
            {
                // 计算推离方向
                Vector2 direction = (transform.position - overlap.transform.position).normalized;

                // 推离到安全距离
                float overlapDistance = currentRadius + overlap.bounds.extents.x; // 假设圆形碰撞
                transform.position += (Vector3)direction * (overlapDistance * 0.1f); // 推离一点点，避免卡死
            }
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        // 可视化当前的检测范围
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x / 2f);
    }
    
    void DisableNewBubblePhysics()
    {
        foreach (var collider in newBubbleChildColliders)
        {
            collider.enabled = false;
        }

        foreach (var rb in newBubbleChildRigidbodies)
        {
            rb.simulated = false;
        }
    }

    void EnableNewBubblePhysics()
    {
        foreach (var collider in newBubbleChildColliders)
        {
            collider.enabled = true;
        }

        foreach (var rb in newBubbleChildRigidbodies)
        {
            rb.simulated = true;
        }
    }
    
}
