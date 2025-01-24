using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private GameObject endGameUI; // 结算UI
    [SerializeField] private TextMeshProUGUI bubbleInfoText; // 显示泡泡内容的Text
    [SerializeField]private EndGameBackground endGameBackground; //变暗背景
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查是否是Content泡泡
        BubbleDataCollection.BubbleDataType bubbleDataType = collision.GetComponentInParent<BubbleStatistic>().selectedDataType;
        if (bubbleDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            // 显示变暗背景
            if (endGameBackground != null)
            {
                endGameBackground.TriggerEndGame();
            }
            // 显示UI
            endGameUI.SetActive(true);

            // 更新UI信息
            bubbleInfoText.text = $"Final Decision: {collision.GetComponentInParent<BubbleStatistic>().bubbleTextData.objectNameText}\n" +
                                  $"Flavor: {collision.GetComponentInParent<BubbleStatistic>().tastyValue}\n" +
                                  $"Wellness: {collision.GetComponentInParent<BubbleStatistic>().healthValue}\n" +
                                  $"Fullness: {collision.GetComponentInParent<BubbleStatistic>().satietyValue}";

            // 停止游戏逻辑（可选）
            Time.timeScale = 0;
        }
    }
}
