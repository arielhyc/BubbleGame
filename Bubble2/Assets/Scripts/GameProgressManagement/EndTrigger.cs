using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bubbleInfoText; // 显示泡泡内容的Text
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查是否是Content泡泡
        BubbleDataCollection.BubbleDataType bubbleDataType = collision.GetComponentInParent<BubbleStatistic>().selectedDataType;
        if (bubbleDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            // 更新UI信息
            bubbleInfoText.text = $"Final Decision: {collision.GetComponentInParent<BubbleStatistic>().bubbleTextData.objectNameText}\n" +
                                  $"Flavor: {collision.GetComponentInParent<BubbleStatistic>().tastyValue}\n" +
                                  $"Wellness: {collision.GetComponentInParent<BubbleStatistic>().healthValue}\n" +
                                  $"Fullness: {collision.GetComponentInParent<BubbleStatistic>().satietyValue}";

            FindObjectOfType<GameManager>().TriggerEndGame(bubbleInfoText.text);
        }
    }
}
