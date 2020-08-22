using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerHealth :Info
{
   
    public static PlayerHealth instance;
    private Gun playerGun;
    private PlayerMove playerMove;
    public Vector3 startPosition;
    public Quaternion startRotation;
    private void Awake()
    {
        instance = this;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    private void Start()
    {
        playerGun = GetComponent<Gun>();
        playerMove = GetComponent<PlayerMove>();
        lightColor = light.color;
    }
    public void Damage(float attack,Transform bulletParent)
    {
        if (!isRelive)//当不在复活时
        {
            HP -= attack;
            if (HP <= 0)
            {
                HP = 0;
                Death(bulletParent);
            }
        }
    }
    public void Death(Transform bulletParent)//被敌人击杀
    {
        if (bulletParent.GetComponent<EnemyInfo>())
        {
            bulletParent.GetComponent<EnemyInfo>().AllExp += this.Exp;//敌人所得经验
            bulletParent.GetComponent<EnemyInfo>().killNumber += 1;
        }
        playerGun.gunHot = 0;
        light.color = Color.yellow;
        GunTF.localEulerAngles = new Vector3(20, 0, 0);
        playerGun.enabled = false;
        playerMove.enabled = false;
    }
    public void Death()//非正常死亡
    {
        playerGun.gunHot = 0;
        light.color = Color.yellow;
        GunTF.localEulerAngles = new Vector3(20, 0, 0);
        playerGun.enabled = false;
        playerMove.enabled = false;
    }
}
