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
            bubbleInfoText.text = "Nice Dream: \n"+$"{collision.GetComponentInParent<BubbleStatistic>().textDisplay.text}\n" +
                                  $"Courage: {collision.GetComponentInParent<BubbleStatistic>().courageValue}\n" +
                                  $"Curiosity: {collision.GetComponentInParent<BubbleStatistic>().curiosityValue}\n" +
                                  $"Whimsy: {collision.GetComponentInParent<BubbleStatistic>().whimsyValue}";
            FindObjectOfType<BubbleGameManager>().TriggerEndGame(bubbleInfoText.text);
        }
    }
}
