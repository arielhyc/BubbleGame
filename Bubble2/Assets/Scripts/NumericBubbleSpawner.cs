using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumericBubbleSpawner : MonoBehaviour
{
    public Collider2D spawnArea; // 生成范围 Trigger
    public List<GameObject> numericBubblePrefabs; // Numeric 泡泡预制体列表
    public int initBubbleCount = 10; // 首次生成的泡泡数量
    public LayerMask collisionMask; // 检测其他物体的碰撞层
    public float bubbleRadius = 0.5f; // 泡泡的半径，用于检测重叠
    
    public int minBubbleCount = 3; // Numeric 泡泡的最小数量阈值
    public int newBubbleCount = 1; // 游戏中生成的泡泡数量
    public float spawnDelay = 1f; // 泡泡生成的时间间隔
    private float spawnTimer = 0f;

    private Camera mainCamera;

    private void Start()
    {
        SpawnNumericBubbles(initBubbleCount);
    }
    
    void Update()
    {
        // 检查泡泡的生成计时器
        spawnTimer += Time.deltaTime;

        // 每隔一定时间检查一次
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0f;

            // 获取场景中的所有泡泡
            BubbleStatistic[] allBubbles = FindObjectsOfType<BubbleStatistic>();

            // 筛选出 Numeric 类型的泡泡
            BubbleStatistic[] numericBubbles = System.Array.FindAll(allBubbles, bubble => bubble.selectedDataType == BubbleDataCollection.BubbleDataType.Numeric);

            // 如果 Numeric 泡泡数量小于最小阈值
            if (numericBubbles.Length < minBubbleCount)
            {
                // 生成新的泡泡
                SpawnNumericBubbles(newBubbleCount);
            }
        }
    }

    private void SpawnNumericBubbles(int bubbleCount)
    {
        int spawned = 0;

        while (spawned < bubbleCount)
        {
            Vector2 randomPosition = GetRandomPositionInTrigger();
            if (!IsOverlapping(randomPosition))
            {
                // 随机选择一个泡泡预制件
                GameObject bubblePrefab = numericBubblePrefabs[Random.Range(0, numericBubblePrefabs.Count)];
                Instantiate(bubblePrefab, randomPosition, Quaternion.identity);
                spawned++;
            }
        }
    }

    private Vector2 GetRandomPositionInTrigger()
    {
        Bounds bounds = spawnArea.bounds;

        // 在 Trigger 范围内随机生成一个点
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);

        return new Vector2(randomX, randomY);
    }

    private bool IsOverlapping(Vector2 position)
    {
        // 检测随机生成点附近是否有其他物体
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, bubbleRadius, collisionMask);
        return colliders.Length > 0;
    }

}
