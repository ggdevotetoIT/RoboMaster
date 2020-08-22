using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RedAutoSolider : AutoSolider
{
    public static RedAutoSolider instance;
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
            bulletGO.GetComponent<PlayerBullet>().bulletParent = this.transform;
            bulletGO.GetComponent<PlayerBullet>().init(attack, bulletSpeed);//将选择好的速度给子弹
            Destroy(bulletGO, 10f);
            TimeCounter = 0;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (this.enabled == true)
        {
            if (other.GetComponent<EnemyInfo>())
            {
                if (other.CompareTag("Enemy") && bulletNumber > 0 && !other.GetComponent<EnemyInfo>().isRelive)
                {
                    isAttack = true;
                    Attack(other.transform.position);
                }
                if (bulletNumber <= 0 || other.GetComponent<EnemyInfo>().isRelive)
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
            if (other.CompareTag("Enemy"))
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
        bulletParent.GetComponent<EnemyInfo>().AllExp += this.Exp;//敌人所得经验
        bulletParent.GetComponent<EnemyInfo>().killNumber += 1;
        UpdateHealth();
        this.enabled = false;
    }
}
