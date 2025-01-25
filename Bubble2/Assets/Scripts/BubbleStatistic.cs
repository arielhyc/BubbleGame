using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class BubbleStatistic : MonoBehaviour
{
    [Header("Bubble Configuration")]
    [SerializeField]public BubbleDataCollection dataCollection; // 引用泡泡数据集合

    [SerializeField]public BubbleDataCollection.BubbleDataType selectedDataType;// 当前泡泡的种类
    [SerializeField]public int selectedIndex; // 用于存储选中的数据索引（根据类型选择具体的Data）

    [SerializeField] public ContentBubbleData contentBubbleData;
    [SerializeField] public NumericBubbleData numericBubbleData;

    public int courageValue;
    public int curiosityValue;
    public int whimsyValue;

    public ContentTextData bubbleTextData; // 关联的文本模板
    public TextMeshPro textDisplay; // 用于显示文字的组件

    private void Start()
    {
        InitBubbleData();
        InitValues(courageValue, curiosityValue, whimsyValue);
        UpdateTextBasedOnValues(); // 检查并更新文本
    }

    private void InitBubbleData()
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            contentBubbleData = dataCollection.contentBubbleList[selectedIndex];
            numericBubbleData = null;
            textDisplay = FindObjectOfType<BubbleGameManager>().contentBubbleDisplayText;
        }
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
        {
            contentBubbleData = null;
            numericBubbleData = dataCollection.numericBubbleList[selectedIndex];
            textDisplay = GetComponentInChildren<TextMeshPro>();
        }
    }
    
    public void InitValues(int tastyDelta, int healthDelta, int satietyDelta)
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            courageValue = dataCollection.contentBubbleList[selectedIndex].value1;
            curiosityValue = dataCollection.contentBubbleList[selectedIndex].value2;
            whimsyValue = dataCollection.contentBubbleList[selectedIndex].value3;
        }
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
        {
            courageValue = dataCollection.numericBubbleList[selectedIndex].value1;
            curiosityValue = dataCollection.numericBubbleList[selectedIndex].value2;
            whimsyValue = dataCollection.numericBubbleList[selectedIndex].value3;
        }
        UpdateTextBasedOnValues(); // 数值变化后更新文本
    }

    private void UpdateTextBasedOnValues()
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            if (bubbleTextData == null || textDisplay == null) return;
            // 获取动态部分的文本
            string couragePart = GetTastyText(courageValue);
            string curiosityPart = GetHealthText(curiosityValue);
            string whimsyPart = GetSatietyText(whimsyValue);
            string objectNamePart = bubbleTextData.objectNameText; // 始终显示的物体名称
    
            // 合并成一个完整的文本
            textDisplay.text = $"{couragePart}" + ",\n" +
                               $"{curiosityPart}" + ",\n" +
                               $"{whimsyPart}" + "\n";
            //     $"{courageValue}\n" +
            //     $"{curiosityValue}\n" +
            //    $"{whimsyValue}\n";
        }
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
        {
            if (textDisplay == null) return; // Numeric类型的泡泡不需要使用bubbleTextData，只需要显示原始数据即可
            textDisplay.text = $"{dataCollection.numericBubbleList[selectedIndex].bubbleText}\n";
            //         $"{courageValue}\n" +
            //         $"{curiosityValue}\n" +
            //         $"{whimsyValue}\n";
        }
    }

    // 根据勇气值生成文本
    private string GetTastyText(int value)
    {
        if (value >= 20) return GetRandomElement(bubbleTextData.courageTextHigh);
        if (value >= 10) return GetRandomElement(bubbleTextData.courageTextMedium);
        return GetRandomElement(bubbleTextData.courageTextLow);
    }

    // 根据好奇值生成文本
    private string GetHealthText(int value)
    {
        if (value >= 20) return GetRandomElement(bubbleTextData.curiosityTextHigh);
        if (value >= 10) return GetRandomElement(bubbleTextData.curiosityTextMedium);
        return GetRandomElement(bubbleTextData.curiosityTextLow);
    }

    // 根据奇思值生成文本
    private string GetSatietyText(int value)
    {
        if (value >= 20) return GetRandomElement(bubbleTextData.whimsyTextHigh);
        if (value >= 10) return GetRandomElement(bubbleTextData.whimsyTextMedium);
        return GetRandomElement(bubbleTextData.whimsyTextLow);
    }
    
    public static T GetRandomElement<T>(List<T> list)
    {
        Random random = new Random();
        int index = random.Next(list.Count);  // 生成一个从0到list.Count-1的随机整数
        return list[index];  // 返回随机选中的元素
    }
    
    private void Update()
    {
    }
    
    
    // 模拟数值变化
    public void UpdateValues(int courageDelta, int curiosityDelta, int whimsyDelta)
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            courageValue = (int) Mathf.Max(courageDelta, 0f);
            curiosityValue = (int) Mathf.Max(curiosityDelta, 0f);
            whimsyValue = (int) Mathf.Max(whimsyDelta, 0f);
    
            UpdateTextBasedOnValues(); // 数值变化后更新文本
        }
        
    }
}
