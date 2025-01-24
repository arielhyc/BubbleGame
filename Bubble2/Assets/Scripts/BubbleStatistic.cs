using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleStatistic : MonoBehaviour
{
    [Header("Bubble Configuration")]
    [SerializeField]public BubbleDataCollection dataCollection; // 引用泡泡数据集合

    [SerializeField]public BubbleDataCollection.BubbleDataType selectedDataType;// 当前泡泡的种类
    [SerializeField]public int selectedIndex; // 用于存储选中的数据索引（根据类型选择具体的Data）

    [SerializeField] public ContentBubbleData contentBubbleData;
    [SerializeField] public NumericBubbleData numericBubbleData;

    public int tastyValue;
    public int healthValue;
    public int satietyValue;

    public ContentTextData bubbleTextData; // 关联的文本模板
    private TextMeshPro textDisplay; // 用于显示文字的组件

    private void Start()
    {
        InitBubbleData();
        InitValues(tastyValue, healthValue, satietyValue);
        UpdateTextBasedOnValues(); // 检查并更新文本
    }

    private void InitBubbleData()
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            contentBubbleData = dataCollection.contentBubbleList[selectedIndex];
            numericBubbleData = null;
        }
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
        {
            contentBubbleData = null;
            numericBubbleData = dataCollection.numericBubbleList[selectedIndex];
        }
    }
    
    public void InitValues(int tastyDelta, int healthDelta, int satietyDelta)
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            tastyValue = dataCollection.contentBubbleList[selectedIndex].value1;
            healthValue = dataCollection.contentBubbleList[selectedIndex].value2;
            satietyValue = dataCollection.contentBubbleList[selectedIndex].value3;
        }
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
        {
            tastyValue = dataCollection.numericBubbleList[selectedIndex].value1;
            healthValue = dataCollection.numericBubbleList[selectedIndex].value2;
            satietyValue = dataCollection.numericBubbleList[selectedIndex].value3;
        }
        UpdateTextBasedOnValues(); // 数值变化后更新文本
    }

    private void UpdateTextBasedOnValues()
    {
        textDisplay = GetComponentInChildren<TextMeshPro>();

        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            if (bubbleTextData == null || textDisplay == null) return;
            // 获取动态部分的文本
            string tastyPart = GetTastyText(tastyValue);
            string healthPart = GetHealthText(healthValue);
            string satietyPart = GetSatietyText(satietyValue);
            string objectNamePart = bubbleTextData.objectNameText; // 始终显示的物体名称
    
            // 合并成一个完整的文本
            textDisplay.text = $"{tastyPart}\n" +
                               $"{healthPart}\n" +
                               $"{satietyPart}\n" +
                               $"{objectNamePart}\n" +
                               $"{tastyValue}\n" +
                               $"{healthValue}\n" +
                               $"{satietyValue}\n";
        }
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
        {
            if (textDisplay == null) return; // Numeric类型的泡泡不需要使用bubbleTextData，只需要显示原始数据即可
            textDisplay.text = $"{dataCollection.numericBubbleList[selectedIndex].bubbleText}\n" +
                               $"{tastyValue}\n" +
                               $"{healthValue}\n" +
                               $"{satietyValue}\n";
        }
    }

    // 根据美味值生成文本
    private string GetTastyText(int value)
    {
        if (value >= 8) return bubbleTextData.tastyTextHigh;
        if (value >= 4) return bubbleTextData.tastyTextMedium;
        return bubbleTextData.tastyTextLow;
    }

    // 根据健康值生成文本
    private string GetHealthText(int value)
    {
        if (value >= 8) return bubbleTextData.healthTextHigh;
        if (value >= 4) return bubbleTextData.healthTextMedium;
        return bubbleTextData.healthTextLow;
    }

    // 根据饱腹值生成文本
    private string GetSatietyText(int value)
    {
        if (value >= 8) return bubbleTextData.satietyTextHigh;
        if (value >= 4) return bubbleTextData.satietyTextMedium;
        return bubbleTextData.satietyTextLow;
    }

    private void Update()
    {
    }
    
    
    // 模拟数值变化
    public void UpdateValues(int tastyDelta, int healthDelta, int satietyDelta)
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            tastyValue = tastyDelta;
            healthValue = healthDelta;
            satietyValue = satietyDelta;
    
            UpdateTextBasedOnValues(); // 数值变化后更新文本
        }
        
    }
}
