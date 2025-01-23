using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupCollisionHandler : MonoBehaviour
{
    public float forceStrength = 1f; // 随机力的大小
    public float changeForceInterval = 2f; // 改变力方向的时间间隔
    public float bounceFactor = 1.5f; // 反弹力的系数，决定反弹的强度
    public Vector2 boundsMin = new Vector2(-5f, -5f); // 边界最小值
    public Vector2 boundsMax = new Vector2(5f, 5f);   // 边界最大值
    
    private Rigidbody2D[] childRigidbodies; // 子对象的刚体
    private float timer; // 计时器
    private Vector2 randomForceDirection; // 当前随机力的方向
    public float maxSpeed = 2f; // 最大速度
    void Start()
    {
        // 获取所有子物体的 Rigidbody2D
        childRigidbodies = GetComponentsInChildren<Rigidbody2D>();

        // 初始化随机力方向
        SetRandomForceDirection();
        print(this.name + "," + new Vector2(this.transform.position.x,this.transform.position.y));
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

        //print(this.name + "," + new Vector2(this.transform.position.x,this.transform.position.y));
    }

    // 设置新的随机力方向
    private void SetRandomForceDirection()
    {
        randomForceDirection = new Vector2(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
        ).normalized * forceStrength;
    }

 
}
