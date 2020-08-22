using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class RedPlane : MonoBehaviour
{
    public float TimeCounter = 0;//出枪间隔
    public Transform[] WayPoints;
    public float bulletNumber = 100f;
    public float attack = 50f;
    public float bulletSpeed = 30f;
    public int i = 0;
    public bool isAttack;
    private Transform abandonedBullets;
    public GameObject bullet;
    public AudioClip fireClip;
    public Transform FireTF;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        abandonedBullets = GameObject.Find("AbandonBullets").transform;
    }
    private void Update()
    {

        TimeCounter += Time.deltaTime;
        FindWayPoint();
    }
    public void FindWayPoint()
    {
        if (WayPoints.Length == i)
        {
            i = 0;
        }
        if (isAttack) return;
        //agent.SetDestination(WayPoints[i].position);
        transform.position = Vector3.MoveTowards(transform.position, WayPoints[i].position, 0.1f);
        //transform.rotation *= Quaternion.Euler(new Vector3(0, 3, 0));
        if (Vector3.SqrMagnitude(transform.position - WayPoints[i].position) < 0.1f)
        {
            i++;
        }
    }
    public void LooKRotation(Vector3 Target)//车子旋转
    {
        Quaternion quaternion = Quaternion.LookRotation(Target - transform.position);
        Vector3 euler = Quaternion.Lerp(transform.rotation, quaternion, 0.1f).eulerAngles;
        transform.eulerAngles = new Vector3(0, euler.y, 0);

    }
    public void Attack(Vector3 position)
    {
        LooKRotation(position);
        if (bulletNumber > 0 && TimeCounter >2f)//出枪间隔固定为1f;
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
}
