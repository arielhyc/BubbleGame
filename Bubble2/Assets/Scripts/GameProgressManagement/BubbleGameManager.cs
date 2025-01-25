using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // 用于更新UI元素

public class BubbleGameManager : MonoBehaviour
{
    public float gameTimeLimit = 60f; // 游戏时间限制，单位：秒
    public TextMeshProUGUI timerText; // 用于显示倒计时的UI文本
    public TextMeshPro contentBubbleDisplayText;
    public GameObject endGameUI; // 游戏结束时显示的UI
    public Image endGameBackground;
    public TextMeshProUGUI endGameText; // 游戏结束时显示的文本
    private GameObject contentBubble; // Content 泡泡
    private float remainingTime; // 剩余时间

    private bool isPlaying = false;
    void OnEnable()
    {
        // 订阅事件
        ContentBubbleSpawner.OnBubbleGenerated += HandleBubbleGenerated;
    }

    void OnDisable()
    {
        // 取消订阅事件
        ContentBubbleSpawner.OnBubbleGenerated -= HandleBubbleGenerated;
    }

    // 事件处理方法
    void HandleBubbleGenerated(GameObject bubble)
    {
        // 获取生成的 Content 泡泡
        contentBubble = bubble;
    }

    private void Awake()
    {
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Debug.Log("[GameManager] Start Triggered");
        remainingTime = gameTimeLimit; // 初始化剩余时间
        endGameUI.SetActive(false); // 游戏结束UI一开始不可见
        isPlaying = true;
    }

    void Update()
    {
        if (!isPlaying) return;
        Debug.Log("[GameManager] Update Triggered" + remainingTime);
        // 游戏倒计时
        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0) 
        {
            // 时间到了，检查是否放入 Content 泡泡
            CheckContentBubbleInNet();
        }

        // 更新计时器UI
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        // 显示倒计时
        timerText.text = Mathf.Max(0, remainingTime).ToString("F2") + "s";
    }

    void CheckContentBubbleInNet()
    {
        // Content 泡泡没有放入捕梦网，触发游戏结束结算
        TriggerEndGame("Time Up and No Dream Caught");
    }

    public void TriggerEndGame(string resultText)
    {
        StartCoroutine(TriggerSequence(resultText));
    }

    private void PauseGame(string resultText)
    {
        // 暂停游戏时间
        Time.timeScale = 0;
    }

    IEnumerator TriggerSequence(string resultText)
    {
        
        yield return StartCoroutine(endGameBackground.GetComponent<EndGameBackground>().FadeInBackground()); // 等待 FunctionA 完成
        // 显示游戏结束UI并显示相应文本
        endGameUI.SetActive(true);
        endGameText.text = resultText;
        PauseGame(resultText);
    }
    
    public void RestartGame()
    {
        Debug.Log("[GameManager] RestartGame Triggered");

        // 重载场景
        SceneManager.LoadScene("Scenes/Scene1");
    }

    public void QuitGame()
    {
        // 退出游戏
        Application.Quit();
    }
}
