using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentBubbleSpawner : MonoBehaviour
{
    public GameObject contentBubblePrefab; // Content 泡泡的预制体
    public float contentBubbleSpawnDelay = 5f; // Content 泡泡的生成延迟时间
    public GameObject spawnPosition; // 固定的生成位置

    private bool contentBubbleSpawned = false; // 标记 Content 泡泡是否已经生成

    public delegate void BubbleGeneratedHandler(GameObject bubble);
    public static event BubbleGeneratedHandler OnBubbleGenerated;
    void Start()
    {
        // 在游戏开始后 5 秒钟生成 Content 泡泡
        Invoke("SpawnContentBubble", contentBubbleSpawnDelay);
    }

    // 生成 Content 泡泡
    void SpawnContentBubble()
    {
        if (!contentBubbleSpawned)
        {
            // 在指定位置生成 Content 泡泡
            GameObject contentBubble = Instantiate(contentBubblePrefab, spawnPosition.transform.position, Quaternion.identity);
            contentBubbleSpawned = true; // 确保 Content 泡泡只生成一次
            // 触发事件，通知 GameManager
            if (OnBubbleGenerated != null)
            {
                OnBubbleGenerated(contentBubble);
            }
        }
    }
}
