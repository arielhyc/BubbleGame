using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float forceStrength = 1f; // 随机力的大小
    public float changeForceInterval = 2f; // 改变力方向的时间间隔
    public Vector2 boundsMin = new Vector2(-5f, -5f); // 边界最小值
    public Vector2 boundsMax = new Vector2(5f, 5f);   // 边界最大值

    private Rigidbody2D[] rigidbodies; // 子物体的所有 2D Rigidbody
    private float timer; // 计时器
    private Vector2 randomForceDirection; // 当前随机力的方向
    public float maxSpeed = 2f;
    
    void Start()
    {
        // 获取所有子物体的 Rigidbody2D
        rigidbodies = GetComponentsInChildren<Rigidbody2D>();

        if (rigidbodies.Length == 0)
        {
            Debug.LogError("No Rigidbody2D components found in child objects!");
        }

        foreach (Rigidbody2D rb in rigidbodies)
        {
            if (rb != null)
            {
                // 设置连续碰撞检测，减少物体穿透
                rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            }
        }
        // 初始化随机力方向
        SetRandomForceDirection();
    }

    void FixedUpdate()
    {
        // 每隔一定时间改变随机力方向
        timer += Time.fixedDeltaTime;
        if (timer >= changeForceInterval)
        {
            SetRandomForceDirection();
            timer = 0f;
        }

        // 对所有 Rigidbody2D 施加随机力
        ApplyForceToAllRigidbodies();

        // 检查整体是否超出边界
        AdjustGroupPosition();

        // 限制最大线性速度
        LimitLinearVelocity();
    }

    // 随机设置新的力方向
    private void SetRandomForceDirection()
    {
        randomForceDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * forceStrength;
    }

    // 对所有 Rigidbody2D 施加力
    private void ApplyForceToAllRigidbodies()
    {
        foreach (var rb in rigidbodies)
        {
            if (rb != null)
            {
                rb.AddForce(randomForceDirection, ForceMode2D.Impulse);
            }
        }
    }

    // 限制最大线性速度
    private void LimitLinearVelocity()
    {
        foreach (var rb in rigidbodies)
        {
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }
    
    // 计算整体中心点并调整位置
    private void AdjustGroupPosition()
    {
        // 计算所有 Rigidbody2D 的中心点
        Vector2 centerOfMass = Vector2.zero;
        foreach (var rb in rigidbodies)
        {
            if (rb != null)
            {
                centerOfMass += rb.position;
            }
        }
        centerOfMass /= rigidbodies.Length;

        // 检查中心点是否超出边界
        Vector2 clampedCenter = new Vector2(
            Mathf.Clamp(centerOfMass.x, boundsMin.x, boundsMax.x),
            Mathf.Clamp(centerOfMass.y, boundsMin.y, boundsMax.y)
        );

        // 如果中心点被限制，计算修正向量
        Vector2 correction = clampedCenter - centerOfMass;

        // 对每个 Rigidbody2D 应用修正位置
        foreach (var rb in rigidbodies)
        {
            if (rb != null)
            {
                rb.position += correction;
            }
        }
    }
}
