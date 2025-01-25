using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class FollowBubble : MonoBehaviour
{
    private Transform bubbleTransform;  // 用来存储泡泡的 Transform

    public Transform bubbleParent;  // 父物体的 Transform，手动设置或通过查找
    private TextMeshPro textMeshPro;    // 当前物体的 TextMeshPro 组件
    private ParticleSystem visualEffect;

    void Start()
    {
        // 获取当前物体的 TextMeshPro 组件
        textMeshPro = GetComponent<TextMeshPro>();
        visualEffect = GetComponent<ParticleSystem>();
        
        // 如果没有指定父物体，可以尝试通过脚本找到
        if (bubbleParent == null)
        {
            bubbleParent = transform.parent;
        }
        if (bubbleParent == null)
        {
            Debug.LogError("No valid references found for TextMeshPro or Bubble parent.");
        }
    }

    void Update()
    {
        if (textMeshPro != null && bubbleParent != null)
        {
            Vector3 totalPosition = Vector3.zero;
            int childCount = 0;

            // 遍历所有子物体并计算它们的位置总和
            foreach (Transform child in bubbleParent)
            {
                totalPosition += child.position;
                childCount++;
            }

            // 计算子物体的平均位置作为文本的新位置
            if (childCount > 0)
            {
                textMeshPro.transform.position = totalPosition / childCount;
            }

            // 保证文本的旋转不受泡泡旋转影响
            textMeshPro.transform.rotation = Quaternion.identity;  // 使文本的旋转重置为无旋转
        }

        if (visualEffect != null && bubbleParent != null)
        {
            Vector3 totalPosition = Vector3.zero;
            int childCount = 0;

            // 遍历所有子物体并计算它们的位置总和
            foreach (Transform child in bubbleParent)
            {
                totalPosition += child.position;
                childCount++;
            }

            // 计算子物体的平均位置作为文本的新位置
            if (childCount > 0)
            {
                visualEffect.transform.position = totalPosition / childCount;
            }

            // 保证文本的旋转不受泡泡旋转影响
            visualEffect.transform.rotation = Quaternion.identity;  // 使文本的旋转重置为无旋转 
        }
        
    }
}
