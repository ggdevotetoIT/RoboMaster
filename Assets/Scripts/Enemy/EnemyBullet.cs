using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemyBullet : Bullet
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isUsable)
            {
                other.transform.GetComponent<PlayerHealth>().Damage(attack, bulletParent);
                isUsable = false;
            }
        }
        if (other.gameObject.CompareTag("Friend"))
        {
            if (isUsable)
            {
                if (other.transform.GetComponent<FriendInfo>())
                {
                    other.transform.GetComponent<FriendInfo>().Damage(attack, bulletParent);
                    isUsable = false;
                }
            }
        }
        if(other.gameObject.CompareTag("RedAutoSolider"))
        {
            if(isUsable)
            {
                other.transform.GetComponent<RedAutoSolider>().Damage(attack, bulletParent);
                isUsable = false;
            }
        }
        if (other.gameObject.CompareTag("RedBase"))
        {
            if (isUsable)
            {
                other.transform.GetComponent<RedBase>().Damage(attack, bulletParent);
                isUsable = false;
            }
        }
        if (other.gameObject.CompareTag("Environment"))
        {
            isUsable = false;
        }
    }
}
