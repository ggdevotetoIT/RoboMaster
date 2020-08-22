using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PlayerBullet : Bullet
{
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            if(isUsable)
            {
                other.transform.GetComponent<EnemyInfo>().Damage(attack, bulletParent);
                isUsable = false;
            }
        }
        if (other.gameObject.CompareTag("GreenBase"))
        {
            if (isUsable)
            {
                other.transform.GetComponent<GreenBase>().Damage(attack, bulletParent);
                isUsable = false;
            }
        }
        if(other.gameObject.CompareTag("Environment"))
        {
            isUsable = false;
        }
        if (other.gameObject.CompareTag("GreenAutoSolider"))
        {
            if (isUsable)
            {
                other.transform.GetComponent<GreenAutoSolider>().Damage(attack, bulletParent);
                isUsable = false;
            }
        }
    }
}
