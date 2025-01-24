using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class SwapValueEffect : MonoBehaviour
{
    private void OnEnable()
    {
        // 订阅事件
        BubbleMerge.OnBubbleCollision += SwapTwoRandomAttributes;
    }

    private void OnDisable()
    {
        // 取消订阅事件
        BubbleMerge.OnBubbleCollision -= SwapTwoRandomAttributes;
    }

    // 事件触发时执行属性互换
    private void SwapTwoRandomAttributes(BubbleMerge bubbleMerge)
    {
        ContentBubbleData contentBubbleData = bubbleMerge.tempContentBubbleData;
        if (contentBubbleData == null)
        {
            Debug.LogError("contentBubbleData is null in "+bubbleMerge.gameObject.name);
            return;
        }
        // 创建一个数组来存储三个属性值
        int[] attributes = new int[] { contentBubbleData.value1, contentBubbleData.value2, contentBubbleData.value3 };

        // 随机选择两个不同的索引
        int index1 = Random.Range(0, 3);
        int index2;
        do
        {
            index2 = Random.Range(0, 3);
        } while (index2 == index1);

        // 交换这两个索引对应的属性值
        int temp = attributes[index1];
        attributes[index1] = attributes[index2];
        attributes[index2] = temp;

        // 将数组的值写回到 ContentBubble 的属性中
        contentBubbleData.value1 = attributes[0];
        contentBubbleData.value2 = attributes[1];
        contentBubbleData.value3 = attributes[2];

        bubbleMerge.gameObject.GetComponent<BubbleStatistic>().
        UpdateValues(contentBubbleData.value1, contentBubbleData.value2,
            contentBubbleData.value3);
            
        Debug.Log($"Attributes swapped: {index1} and {index2}");
        Debug.Log($"New values: attribute1 = {contentBubbleData.value1}, attribute2 = {contentBubbleData.value2}, attribute3 = {contentBubbleData.value3}");
    }
}
