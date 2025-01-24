using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspect : MonoBehaviour
{
    void Start()
    {
        // 设置相机的宽高比为 1:1
        Camera.main.aspect = 1.0f;
    }
}
