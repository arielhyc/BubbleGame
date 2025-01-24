using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameBackground : MonoBehaviour
{
    public Image darkBackground; // 分配 DarkBackground 图片
    public float fadeDuration = 1f; // 渐变持续时间

    private void Start()
    {
        // 确保背景初始隐藏
        if (darkBackground != null)
        {
            SetAlpha(0f);
        }
    }

    public void TriggerEndGame()
    {
        if (darkBackground != null)
        {
            StartCoroutine(FadeInBackground());
        }
    }

    private IEnumerator FadeInBackground()
    {
        float elapsedTime = 0f;
        Color color = darkBackground.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration); // 渐变到透明度 1
            darkBackground.color = color;
            yield return null;
        }

        color.a = 1f;
        darkBackground.color = color;
    }

    private void SetAlpha(float alpha)
    {
        if (darkBackground != null)
        {
            Color color = darkBackground.color;
            color.a = alpha;
            darkBackground.color = color;
        }
    }
}
