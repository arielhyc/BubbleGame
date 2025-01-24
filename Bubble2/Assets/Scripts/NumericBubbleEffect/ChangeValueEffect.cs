using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeValueEffect : MonoBehaviour
{
    private void OnEnable()
    {
        // 订阅事件
        BubbleMerge.OnBubbleCollision += ApplyChangeValueEffect;
    }

    private void OnDisable()
    {
        // 取消订阅事件
        BubbleMerge.OnBubbleCollision -= ApplyChangeValueEffect;
    }
    
    private void ApplyChangeValueEffect(BubbleMerge bubbleMerge)
    {
        ContentBubbleData contentData = bubbleMerge.tempContentBubbleData;
        NumericBubbleData numericData = bubbleMerge.tempNumericBubbleData;
        if (contentData == null)
        {
            Debug.LogError("contentData is null in "+bubbleMerge.gameObject.name);
            return;
        }
        if (numericData == null)
        {
            Debug.LogError("numericData is null in "+bubbleMerge.numericBubbleMerge.gameObject.name);
            return;
        }
        switch (numericData.operationType)
        {
            case OperationType.Add:
                contentData.value1 += numericData.value1;
                contentData.value2 += numericData.value2;
                contentData.value3 += numericData.value3;
                bubbleMerge.gameObject.GetComponent<BubbleStatistic>().
                    UpdateValues(contentData.value1, contentData.value2,
                        contentData.value3);
                break;
            case OperationType.Subtract:
                contentData.value1 -= numericData.value1;
                contentData.value2 -= numericData.value2;
                contentData.value3 -= numericData.value3;
                bubbleMerge.gameObject.GetComponent<BubbleStatistic>().
                    UpdateValues(contentData.value1, contentData.value2,
                        contentData.value3);
                break;
            case OperationType.Multiply:
                contentData.value1 *= numericData.value1;
                contentData.value2 *= numericData.value2;
                contentData.value3 *= numericData.value3;
                bubbleMerge.gameObject.GetComponent<BubbleStatistic>().
                    UpdateValues(contentData.value1, contentData.value2,
                        contentData.value3);
                break;
            case OperationType.Divide:
                contentData.value1 = numericData.value1 != 0 ? contentData.value1 / numericData.value1 : contentData.value1;
                contentData.value2 = numericData.value2 != 0 ? contentData.value2 / numericData.value2 : contentData.value2;
                contentData.value3 = numericData.value3 != 0 ? contentData.value3 / numericData.value3 : contentData.value3;
                bubbleMerge.gameObject.GetComponent<BubbleStatistic>().
                    UpdateValues(contentData.value1, contentData.value2,
                        contentData.value3);
                break;
            case OperationType.Other:
                break;
        }
    }
}
