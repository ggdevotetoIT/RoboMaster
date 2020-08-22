using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class FriendSupply : MonoBehaviour
{
    public bool isSuppy;
    public GameObject Supply;
    public float supplyTimeCounter;
    public bool isBuff;
    public GameObject Buff;
    public float buffTimeCounter;
    public float countTime;//计算补血时间
    private FriendInfo friendInfo;
    private FriendGun gun;
    private void Start()
    {
        friendInfo = transform.parent.GetComponent<FriendInfo>();
        gun = transform.parent.GetComponent<FriendGun>();
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
            friendInfo.attackRate = 1f;
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
        if (other.CompareTag("RedSupply"))
        {
            isSuppy = true;
            supplyTimeCounter = 0.3f;
            if (countTime >= 1f)
            {
                gun.bulletNumber += 10;
                if (gun.bulletNumber > friendInfo.maxBulletNumber)
                {
                    gun.bulletNumber = friendInfo.maxBulletNumber;
                }
                friendInfo.HP += 100;
                countTime = 0f;
            }
        }
        if (other.CompareTag("Buff"))
        {
            isBuff = true;
            friendInfo.attackRate = 2f;
            buffTimeCounter = 0.3f;
        }
    }
}
