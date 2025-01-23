using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OperationType
{
    Add,    // 加法
    Subtract, // 减法
    Multiply, // 乘法
    Divide   // 除法
}

[CreateAssetMenu(fileName = "NewBubbleData", menuName = "Bubble/NumericBubbleData" , order = 3)]
public class NumericBubbleData : ScriptableObject
{
    [Header("Content Settings")]
    public string bubbleText; // 泡泡的文字内容

    [Header("Values")]
    public float value1;        // 第一个数值
    public float value2;        // 第二个数值
    public float value3;        // 第三个数值
    
    [Header("ValueOperation")]
    public OperationType operationType;  // 操作类型（加、减、乘、除）
}
