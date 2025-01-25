using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionManager : MonoBehaviour
{
    public List<Sprite> instructionImages; // 存储指令图片
    public Image displayImage; // 用于显示图片的 UI 元素
    public Button nextButton; // 下一张按钮
    private int currentImageIndex = 0;

    void Start()
    {
        if (instructionImages.Count == 0 || displayImage == null || nextButton == null)
        {
            Debug.LogError("InstructionManager is not set up properly.");
            return;
        }

        // 显示第一张图片
        UpdateImage();

        // 按钮事件绑定
        nextButton.onClick.AddListener(ShowNextImage);
    }

    void UpdateImage()
    {
        // 设置当前图片
        displayImage.sprite = instructionImages[currentImageIndex];
    }

    void ShowNextImage()
    {
        // 如果还有图片未显示
        if (currentImageIndex < instructionImages.Count - 1)
        {
            currentImageIndex++;
            UpdateImage();
        }
        else
        {
            // 所有图片显示完毕，开始游戏
            StartGame();
        }
    }

    void StartGame()
    {
        // 禁用图片和按钮
        displayImage.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        FindObjectOfType<BubbleGameManager>().StartGame();
    }
}