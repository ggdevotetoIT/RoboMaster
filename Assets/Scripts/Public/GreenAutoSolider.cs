using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class GreenAutoSolider : AutoSolider
{
    public static GreenAutoSolider instance;
    private void Awake()
    {
        instance = this;
    }
    public void Attack(Vector3 position)
    {
        LooKRotation(position);
        if (bulletNumber > 0 && TimeCounter > 0.5f)//出枪间隔固定为0.5f;
        {
            audioSource.PlayOneShot(fireClip);
            bulletNumber--;
            GameObject bulletGO = Instantiate(bullet, FireTF.position, Quaternion.LookRotation(position - FireTF.position)) as GameObject;
            bulletGO.transform.SetParent(abandonedBullets);
            bulletGO.GetComponent<EnemyBullet>().bulletParent = this.transform;
            bulletGO.GetComponent<EnemyBullet>().init(attack, bulletSpeed);//将选择好的速度给子弹
            Destroy(bulletGO, 10f);
            TimeCounter = 0;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (this.enabled == true)
        {
            if (other.GetComponent<PlayerHealth>())
            {
                if (other.CompareTag("Player") && bulletNumber > 0 && !other.GetComponent<PlayerHealth>().isRelive)
                {
                    isAttack = true;
                    Attack(other.transform.position);
                }
                if (bulletNumber <= 0 || other.GetComponent<PlayerHealth>().isRelive)
                {
                    isAttack = false;
                }
            }
            if (other.GetComponent<FriendInfo>())
            {
                if (other.CompareTag("Friend") && bulletNumber > 0 && !other.GetComponent<FriendInfo>().isRelive)
                {
                    isAttack = true;
                    Attack(other.transform.position);
                }
                if (bulletNumber <= 0 || other.GetComponent<FriendInfo>().isRelive)
                {
                    isAttack = false;
                }
            }
            
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (this.enabled == true)
        {
            if (other.CompareTag("Player"))
            {
                isAttack = false;
            }
            if (other.CompareTag("Friend"))
            {
                isAttack = false;
            }
        }

    }
    public void Damage(float attack, Transform bulletParent)
    {
        if (HP == 0) return;
        HP -= attack;
        if (HP <= 0)
        {
            HP = 0;
            Death(bulletParent);
        }
    }
    public void Death(Transform bulletParent)//被敌人击杀
    {
        if (bulletParent.GetComponent<PlayerHealth>())//如果玩家击杀,玩家获得经验
        {
            bulletParent.GetComponent<PlayerHealth>().AllExp += this.Exp;//敌人所得经验
            bulletParent.GetComponent<PlayerHealth>().killNumber += 1;
        }
        if(bulletParent.GetComponent<FriendInfo>())
        {
            bulletParent.GetComponent<FriendInfo>().AllExp += this.Exp;
            bulletParent.GetComponent<FriendInfo>().killNumber += 1;
        }

        UpdateHealth();
        this.enabled = false;
    }
}
