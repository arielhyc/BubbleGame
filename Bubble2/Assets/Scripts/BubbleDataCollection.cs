using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleDataCollection", menuName = "Bubble/BubbleDataCollection", order = 1)]
public class BubbleDataCollection : ScriptableObject
{
    public enum BubbleDataType
    {
        Content,
        Numeric,
    }
    
    public BubbleDataType selectedDataType;  // 选择的泡泡数据类型
    
    [Header("Bubble Data List")]
    public List<ContentBubbleData> contentBubbleList; // 存储该类别所有泡泡数据的列表
    public List<NumericBubbleData> numericBubbleList;  // 存储 Numeric 类型泡泡的数据
}
