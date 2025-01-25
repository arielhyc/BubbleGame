using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EndGameBackground : MonoBehaviour
{
    [FormerlySerializedAs("darkBackground")] public Image endBackground; // 分配 DarkBackground 图片
    public float fadeDuration = 1f; // 渐变持续时间

    private void Start()
    {
        // 确保背景初始隐藏
        if (endBackground != null)
        {
            SetAlpha(0f);
        }
    }

    public IEnumerator FadeInBackground()
    {
        float elapsedTime = 0f;
        Color color = endBackground.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // 渐变到透明度 1
            endBackground.color = color;
            yield return null;
        }

        color.a = 1f;
        endBackground.color = color;
    }

    private void SetAlpha(float alpha)
    {
        if (endBackground != null)
        {
            Color color = endBackground.color;
            color.a = alpha;
            endBackground.color = color;
        }
    }
}
