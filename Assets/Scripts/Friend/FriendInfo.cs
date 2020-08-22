using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// 
/// </summary>
public class FriendInfo : Info
{
    private FriendGun gun;
    private FriendNavigation navigation;
    private NavMeshAgent navMeshAgent;
    //public AudioClip audioclip;
    private AudioSource audioSource;
    private void Start()
    {
        gun = GetComponent<FriendGun>();
        navigation = GetComponent<FriendNavigation>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        lightColor = light.color;
    }
    public void Damage(float amount, Transform bulletParent)
    {
        if (!isRelive)//当不在复活时
        {
            HP -= amount;
            if (HP <= 0)
            {
                HP = 0;
                Death(bulletParent);
            }
        }
    }
    private void Death(Transform bulletParent)
    {
        if (bulletParent.GetComponent<EnemyInfo>())
        {
            bulletParent.GetComponent<EnemyInfo>().AllExp += Exp;//加上该等级的经验
            bulletParent.GetComponent<EnemyInfo>().killNumber += 1;
        }
        //audioSource.PlayOneShot(audioclip);
        //播放击杀音效
        //播放击杀UI;
        //light.color = Color.magenta;
        light.color = Color.yellow;
        GunTF.localEulerAngles = new Vector3(20, 0, 0);
        gun.enabled = false;
        navigation.enabled = false;
        navMeshAgent.enabled = false;
        //敌人死后寻路和攻击将停止
    }
}
