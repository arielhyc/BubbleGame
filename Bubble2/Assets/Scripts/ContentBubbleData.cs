using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleData", menuName = "Bubble/ContentBubbleData", order = 1)]
public class ContentBubbleData : ScriptableObject
{
    [Header("Content Settings")]
    public string bubbleText; // 泡泡的文字内容

    [Header("Values")]
    public float value1; // 数值 1
    public float value2; // 数值 2
    public float value3; // 数值 3
}
