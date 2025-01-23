using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleChildMergeDetector : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        // 检测是否碰到另一个泡泡的子物体
        if (other.gameObject.CompareTag("BubbleChild"))
        {
            print("BubbleChild Collision Detected");
            // 通知父物体
            BubbleMerge parentBubble = GetComponentInParent<BubbleMerge>();
            BubbleMerge otherParentBubble = other.gameObject.GetComponentInParent<BubbleMerge>();

            if (parentBubble != null && otherParentBubble != null)
            {
                parentBubble.OnChildCollision(otherParentBubble);
            }
        }
    }
}
