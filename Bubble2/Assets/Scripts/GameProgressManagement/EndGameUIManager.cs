using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUIManager : MonoBehaviour
{
    public void RestartGame()
    {
        // 恢复游戏时间
        Time.timeScale = 1;

        // 重载场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        // 退出游戏
        Application.Quit();
    }
}