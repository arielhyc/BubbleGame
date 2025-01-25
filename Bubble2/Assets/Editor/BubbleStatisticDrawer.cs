using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BubbleStatistic))]
public class BubbleStatisticEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 获取目标对象
        BubbleStatistic bubbleStatistic = (BubbleStatistic)target;

        // 显示默认字段，包括 public 字段
        DrawDefaultInspector();

        // 如果 contentTextData 没有显示，可以手动绘制它
        if (bubbleStatistic.bubbleTextData != null)
        {
            // 绘制 ContentTextData
            EditorGUILayout.ObjectField("Content Text Data", bubbleStatistic.bubbleTextData, typeof(ContentTextData), false);
        }
        
        // 绘制 BubbleDataCollection 选择框
        bubbleStatistic.dataCollection = (BubbleDataCollection)EditorGUILayout.ObjectField(
            "Bubble Data Collection",
            bubbleStatistic.dataCollection,
            typeof(BubbleDataCollection),
            false
        );

        // 检查是否已经选择了 BubbleDataCollection
        if (bubbleStatistic.dataCollection == null)
        {
            EditorGUILayout.HelpBox("Please assign a Bubble Data Collection to continue.", MessageType.Warning);
            return;
        }

        // 显示数据类型选择
        bubbleStatistic.selectedDataType = (BubbleDataCollection.BubbleDataType)EditorGUILayout.EnumPopup("Data Type", bubbleStatistic.selectedDataType);

        // 如果有引用到BubbleDataCollection，显示数据列表
        if (bubbleStatistic.dataCollection != null)
        {
            switch (bubbleStatistic.selectedDataType)
            {
                case BubbleDataCollection.BubbleDataType.Content:
                    ShowContentBubbleList(bubbleStatistic);
                    break;
                case BubbleDataCollection.BubbleDataType.Numeric:
                    ShowNumericBubbleList(bubbleStatistic);
                    break;
            }
        }
        if (bubbleStatistic.dataCollection == null)
        {
            EditorGUILayout.HelpBox("BubbleDataCollection is not assigned.", MessageType.Warning);
            return; // 如果未分配，显示提示并停止绘制
        }

        // 更新属性
        EditorUtility.SetDirty(bubbleStatistic);
    }

    private void ShowContentBubbleList(BubbleStatistic bubbleStatistic)
    {
        // 获取所有Content类型数据
        var contentBubbleList = bubbleStatistic.dataCollection.contentBubbleList;
        if (contentBubbleList != null && contentBubbleList.Count > 0)
        {
            // 显示下拉菜单，供选择具体的ContentData
            string[] contentBubbleNames = new string[contentBubbleList.Count];
            for (int i = 0; i < contentBubbleList.Count; i++)
            {
                contentBubbleNames[i] = contentBubbleList[i].name; // 可以显示内容文本或其他属性
            }

            bubbleStatistic.selectedIndex = EditorGUILayout.Popup("Select Content Data", bubbleStatistic.selectedIndex, contentBubbleNames);
        }
        else
        {
            EditorGUILayout.LabelField("No content bubbles available.");
        }
    }

    private void ShowNumericBubbleList(BubbleStatistic bubbleStatistic)
    {
        // 获取所有Numeric类型数据
        var numericBubbleList = bubbleStatistic.dataCollection.numericBubbleList;
        if (numericBubbleList != null && numericBubbleList.Count > 0)
        {
            // 显示下拉菜单，供选择具体的NumericData
            string[] numericBubbleNames = new string[numericBubbleList.Count];
            for (int i = 0; i < numericBubbleList.Count; i++)
            {
                numericBubbleNames[i] = numericBubbleList[i].name;
            }

            bubbleStatistic.selectedIndex = EditorGUILayout.Popup("Select Numeric Data", bubbleStatistic.selectedIndex, numericBubbleNames);
        }
        else
        {
            EditorGUILayout.LabelField("No numeric bubbles available.");
        }
    }
}
