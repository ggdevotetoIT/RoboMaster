using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Bullet : MonoBehaviour
{
    //private Vector3 startPosition;
    private TrailRenderer effects;
    public float movespeed;
    public float attack;
    public Rigidbody bulletRD;
    private Vector3 Target;
    public Transform bulletParent;//用来判断谁发的子弹
    public bool isUsable = true;//避免掉在地上的子弹造成二次伤害
    private void Start()
    {
        bulletRD = GetComponent<Rigidbody>();
        bulletRD.AddForce(transform.forward*movespeed);
        effects = GetComponent<TrailRenderer>();
    }
    private void Update()
    {
        ShutDownEffect();
    }
    //确定子弹射速和攻击力
    public void init(float attack,float moveSpeed)
    {
        this.attack = attack;
        this.movespeed = moveSpeed;
    }
    private void ShutDownEffect()//速度小时关闭拖尾特效
    {
        if(bulletRD.velocity.sqrMagnitude<1f)
        {
            effects.enabled = false;
        }
    }
}
