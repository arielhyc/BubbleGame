using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blower : MonoBehaviour
{
    public float forceStrength = 10f; // 初始吹气力度
    public float maxForceStrength = 20f; // 最大吹气力度
    public float minForceStrength = 5f;  // 最小吹气力度
    public float range = 3f; // 吹气范围
    public float angle = 0f; // 吹气方向（以角度表示，初始为水平右侧）

    public ParticleSystem airFlowParticleSystem; // 粒子系统
    public GameObject rangeIndicator;
    
    public KeyCode increaseForceKey = KeyCode.UpArrow;   // 增加力度的按键
    public KeyCode decreaseForceKey = KeyCode.DownArrow; // 减少力度的按键
    public KeyCode rotateLeftKey = KeyCode.LeftArrow;    // 向左旋转方向的按键
    public KeyCode rotateRightKey = KeyCode.RightArrow;  // 向右旋转方向的按键
    public KeyCode blowKey = KeyCode.Space;              // 吹气的按键

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 更新吹气筒位置（跟随鼠标）
        Vector3 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        mousePos.z = 0;
        transform.position = mousePos;

        // 调节吹气方向（旋转角度）
        if (Input.GetKey(rotateLeftKey))
        {
            angle += 90 * Time.deltaTime; // 旋转速度
        }
        if (Input.GetKey(rotateRightKey))
        {
            angle -= 90 * Time.deltaTime;
        }

        // 调节吹气力度
        if (Input.GetKey(increaseForceKey))
        {
            forceStrength = Mathf.Min(forceStrength + 5 * Time.deltaTime, maxForceStrength);
        }
        if (Input.GetKey(decreaseForceKey))
        {
            forceStrength = Mathf.Max(forceStrength - 5 * Time.deltaTime, minForceStrength);
        }

        // 吹气操作
        if (Input.GetKeyDown(blowKey))
        {
            Blow();
        }

        // 更新吹气筒的旋转
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        // 检测按键按下 显示气流粒子
        if (Input.GetKeyDown(blowKey))
        {
            StartBlowing();
        }
        // 检测按键松开 隐藏气流粒子
        if (Input.GetKeyUp(blowKey))
        {
            StopBlowing();
        }
    }

    void Blow()
    {
        // 获取范围内的所有泡泡
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(rangeIndicator.transform.position, range);
        foreach (Collider2D collider in hitColliders)
        {
            // 检查对象是否有 Rigidbody2D
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // 计算吹气方向
                Vector2 blowDirection = Quaternion.Euler(0, 0, angle) * Vector2.right;

                // 对泡泡施加力
                rb.AddForce(blowDirection * forceStrength, ForceMode2D.Impulse);
            }
        }

        // 可选：播放吹气的粒子效果或声音
        Debug.Log("Blowing bubbles in range!");
    }

    // 在场景中可视化吹气范围
    void StartBlowing()
    {
        if (!airFlowParticleSystem.isPlaying)
        {
            airFlowParticleSystem.Play();
        }
    }

    // 在场景中可视化吹气范围
    void StopBlowing()
    {
        if (airFlowParticleSystem.isPlaying)
        {
            airFlowParticleSystem.Stop();
        }
    }
    
    // 可选：在场景中可视化吹气范围
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(rangeIndicator.transform.position, range);
        Gizmos.color = Color.red;

        // 绘制吹气方向
        Vector3 endPoint = rangeIndicator.transform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * range;
        Gizmos.DrawLine(rangeIndicator.transform.position, endPoint);
    }
}
