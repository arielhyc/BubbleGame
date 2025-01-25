using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    public GameObject startMenuUI; // 游戏开始界面
    public GameObject instructionUI; // 指令界面
    public Button playButton; // Play 按钮
    public Button exitButton; // Exit 按钮

    void Start()
    {
        // 显示开始界面，隐藏指令界面
        startMenuUI.SetActive(true);
        instructionUI.SetActive(false);

        // 绑定按钮事件
        playButton.onClick.AddListener(StartInstructions);
        exitButton.onClick.AddListener(ExitGame);
    }

    void StartInstructions()
    {
        // 隐藏开始界面，显示指令界面
        startMenuUI.SetActive(false);
        instructionUI.SetActive(true);
    }

    void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();
    }
}