using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewContentTextData", menuName = "Bubble/ContentTextData", order = 4)]
public class ContentTextData : ScriptableObject
{
    // 每个属性的文本映射
    [Header("美味值文本")]
    public string tastyTextLow;      // 低美味文本
    public string tastyTextMedium;   // 中等美味文本
    public string tastyTextHigh;     // 高美味文本

    [Header("健康值文本")]
    public string healthTextLow;     // 低健康文本
    public string healthTextMedium;  // 中等健康文本
    public string healthTextHigh;    // 高健康文本

    [Header("饱腹值文本")]
    public string satietyTextLow;    // 低饱腹文本
    public string satietyTextMedium; // 中等饱腹文本
    public string satietyTextHigh;   // 高饱腹文本

    [Header("物体名称文本")]
    public string objectNameText;    // 物体名称文本
}