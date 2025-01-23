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
    // 你可以根据需要在这里添加更多字段
    public string selectedContentText;
    public float selectedNumericValue;
    
    private TextMeshPro textMeshPro; // 用于显示文字的组件
    
    private void Awake()
    {
        // 获取 TextMeshPro 组件
        textMeshPro = GetComponentInChildren<TextMeshPro>();

        if (textMeshPro == null)
        {
            Debug.LogError("No TextMeshPro component found in the Bubble!");
        }
    }
    private void Start()
    {
        // 初始化泡泡内容
        InitializeBubbleStat();
    }

    // 从数据中初始化泡泡属性
    private void InitializeBubbleStat()
    {
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
            contentBubbleData = dataCollection.contentBubbleList[selectedIndex];
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
            numericBubbleData = dataCollection.numericBubbleList[selectedIndex];
        // 设置文字内容
        if (textMeshPro != null)
        {
            textMeshPro.text = GenerateBubbleText();
        }
    }
    
    private string GenerateBubbleText()
    {
        // 根据泡泡类型生成显示内容
        if (selectedDataType == BubbleDataCollection.BubbleDataType.Content)
        {
            return $"{dataCollection.contentBubbleList[selectedIndex].bubbleText:F1}\n{ dataCollection.contentBubbleList[selectedIndex].value1:F1}\n{dataCollection.contentBubbleList[selectedIndex].value2:F1}\n{dataCollection.contentBubbleList[selectedIndex].value3:F1}";
        }
        else if (selectedDataType == BubbleDataCollection.BubbleDataType.Numeric)
        {
            return $"{dataCollection.numericBubbleList[selectedIndex].value1:F1}\n{dataCollection.numericBubbleList[selectedIndex].value2:F1}\n{dataCollection.numericBubbleList[selectedIndex].value3:F1}";
        }
        return "Unknown";
    }

}
