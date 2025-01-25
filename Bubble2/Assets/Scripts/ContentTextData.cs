using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewContentTextData", menuName = "Bubble/ContentTextData", order = 4)]
public class ContentTextData : ScriptableObject
{
    // 每个属性的文本映射
    [Header("勇气值文本")]
    public List<String> courageTextLow;     
    public List<String> courageTextMedium;  
    public List<String> courageTextHigh;     

    [Header("健康值文本")]
    public List<String> curiosityTextLow;     
    public List<String> curiosityTextMedium;  
    public List<String> curiosityTextHigh;   

    [Header("饱腹值文本")]
    public List<String> whimsyTextLow;    
    public List<String> whimsyTextMedium; 
    public List<String> whimsyTextHigh;   

    [Header("物体名称文本")]
    public string objectNameText;    // 物体名称文本
}