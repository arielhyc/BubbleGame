using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTextMeshSorting : MonoBehaviour
{
    void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            // 设置 Sorting Layer 和 Order
            meshRenderer.sortingLayerName = "Text"; // 替换为你的 Sorting Layer 名称
            meshRenderer.sortingOrder = 10; // 确保比泡泡的 SpriteRenderer 高
        }
    }
}
