using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlowerKeyPrompt : MonoBehaviour
{
    public TextMeshProUGUI keyPromptText;

    void Start()
    {
        keyPromptText = GetComponent<TextMeshProUGUI>();
        // 设置初始按键提示内容
        keyPromptText.text = "Move: Follow Mouse | Rotate: Arrow Keys | Blow: Space";
    }

    public void UpdateKeyPrompt(string newPrompt)
    {
        // 动态更新按键提示
        keyPromptText.text = newPrompt;
    }
}
