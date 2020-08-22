using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemyGun : MonoBehaviour
{
    private float bulletSpeed = 30f;
    private float attack = 50;
    //固定敌人子弹速度为30,攻击力为50;
    public GameObject enemyBullet;
    private Transform abandonedBullets;
    public Transform FireTF;
    public AudioClip fireClip;
    public float bulletNumber = 100;
    private AudioSource audioSource;
    private float TimeCounter = 0;
    public Transform enemyGunRotate;
    public bool isAttack = false;
    private EnemyInfo enemyInfo;
    private float lifeRate;
    public Transform EnemyCamera;//敌人炮筒
    private void Start()
    {
        enemyInfo = GetComponent<EnemyInfo>();
        audioSource = GetComponent<AudioSource>();
        abandonedBullets = GameObject.Find("AbandonBullets").transform;
    }
    private void Update()
    {
        TimeCounter += Time.deltaTime;
        lifeRate = enemyInfo.HP / enemyInfo.MaxHP;
    }
    public void Attack(Vector3 position,int speedRate)
    {
        LooKRotation(position);
        if (bulletNumber > 0 && TimeCounter > 1.2-0.1*(EnemySpawn.EnemyNumber+enemyInfo.Level))//通过敌人数量和敌人等级改变敌人出枪频率
        {
            audioSource.PlayOneShot(fireClip);
            bulletNumber--;
            GameObject bulletGO = Instantiate(enemyBullet, FireTF.position, Quaternion.LookRotation(position - FireTF.position)) as GameObject;
            bulletGO.transform.SetParent(abandonedBullets);
            bulletGO.GetComponent<EnemyBullet>().bulletParent = this.transform;
            bulletGO.GetComponent<EnemyBullet>().init(attack*enemyInfo.attackRate, bulletSpeed*speedRate);//敌人打哨兵和步兵的子弹速度不一样
            Destroy(bulletGO, 10f);//生成的子弹10秒后销毁
            TimeCounter = 0;
        }
    }
    public void LooKRotation(Vector3 Target)//车子旋转
    {
        Quaternion quaternion = Quaternion.LookRotation(Target - transform.position);
        Vector3 euler = Quaternion.Lerp(transform.rotation, quaternion, 0.1f).eulerAngles;
        transform.eulerAngles = new Vector3(0,euler.y,0);

    }
    void OnTriggerStay(Collider other)
    {
        if(this.enabled==true)
        {
            if (other.GetComponent<PlayerHealth>())
            {
                if (other.CompareTag("Player") && bulletNumber > 0 && lifeRate >= 0.25f && !other.GetComponent<PlayerHealth>().isRelive)
                {
                    isAttack = true;
                    Attack(other.transform.position,1);
                }
                if (bulletNumber <= 0 || lifeRate < 0.25f || other.GetComponent<PlayerHealth>().isRelive)
                {
                    isAttack = false;
                }
            }
            if (other.GetComponent<FriendInfo>())
            {
                if (other.CompareTag("Friend") && bulletNumber > 0 && lifeRate >= 0.25f && !other.GetComponent<FriendInfo>().isRelive)
                {
                    isAttack = true;
                    Attack(other.transform.position, 1);
                }
                if (bulletNumber <= 0 || lifeRate < 0.25f || other.GetComponent<FriendInfo>().isRelive)
                {
                    isAttack = false;
                }
            }
            if(other.GetComponent<RedAutoSolider>())
            {
                if (other.CompareTag("RedAutoSolider") && bulletNumber > 0 && lifeRate >= 0.25f &&other.GetComponent<RedAutoSolider>().HP>0)
                {
                    isAttack = true;
                    Attack(other.transform.position,3);
                }
                if (bulletNumber <= 0 || lifeRate < 0.25f || other.GetComponent<RedAutoSolider>().HP<=0)
                {
                    isAttack = false;
                }
            }
            if (other.GetComponent<RedBase>())
            {
                if (other.CompareTag("RedBase") && bulletNumber > 0 && lifeRate >= 0.25f && other.GetComponent<RedBase>().HP > 0)
                {
                    isAttack = true;
                    Attack(other.transform.position, 3);
                }
                if (bulletNumber <= 0 || lifeRate < 0.25f || other.GetComponent<RedBase>().HP <= 0)
                {
                    isAttack = false;
                }
            }
        
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(this.enabled==true)
        {
            if (other.CompareTag("Player"))
            {
                isAttack = false;
            }
            if (other.CompareTag("Friend"))
            {
                isAttack = false;
            }
            if (other.CompareTag("RedAutoSolider"))
            {
                isAttack = false;
            }
        }
    }
}
