using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemySupply : MonoBehaviour
{
    public bool isSuppy;
    public GameObject Supply;
    public float supplyTimeCounter;
    public bool isBuff;
    public GameObject Buff;
    public float buffTimeCounter;
    public float countTime;//计算补血时间
    private EnemyInfo enemyInfo;
    private EnemyGun gun;
    private void Start()
    {
        enemyInfo = transform.parent.GetComponent<EnemyInfo>();
        gun = transform.parent.GetComponent<EnemyGun>();
    }
    private void Update()
    {
        countTime += Time.deltaTime;
        supplyTimeCounter -= Time.deltaTime;
        if (supplyTimeCounter <= 0f)
        {
            supplyTimeCounter = 0f;
            isSuppy = false;
        }
        buffTimeCounter -= Time.deltaTime;
        if (buffTimeCounter <= 0f)
        {
            buffTimeCounter = 0f;
            isBuff = false;
            enemyInfo.attackRate = 1f;
        }
        UpdateSupply();
        UpdateBuff();
    }
    private void UpdateSupply()
    {
        if (isSuppy)
        {
            Supply.SetActive(true);
        }
        else
        {
            Supply.SetActive(false);
        }
    }
    private void UpdateBuff()
    {
        if (isBuff)
        {
            Buff.SetActive(true);
        }
        else
        {
            Buff.SetActive(false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GreenSupply"))
        {
            isSuppy = true;
            supplyTimeCounter = 0.3f;
            if (countTime >= 1f)
            {
                gun.bulletNumber += 10;
                if (gun.bulletNumber > enemyInfo.maxBulletNumber)
                {
                    gun.bulletNumber = enemyInfo.maxBulletNumber;
                }
                enemyInfo.HP += 100;
                countTime = 0f;
            }
        }
        if (other.CompareTag("Buff"))
        {
            isBuff = true;
            enemyInfo.attackRate = 2f;
            buffTimeCounter = 0.3f;
        }
    }
}
