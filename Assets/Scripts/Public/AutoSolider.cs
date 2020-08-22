using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class AutoSolider : MonoBehaviour
{
    public Transform[] WayPoints;
    public float HP;
    private float MaxHP = 1500;
    public float Exp = 7.5f;
    public float bulletNumber = 500f;//哨兵固定500发子弹
    public float attack = 50f;
    public float bulletSpeed = 30f;
    public int i = 0;
    public Slider slider;
    public Slider UpSlider;
    public bool isAttack;
    public float TimeCounter = 0;//出枪间隔
    public Transform abandonedBullets;
    public AudioSource audioSource;
    public GameObject bullet;
    public AudioClip fireClip;
    public Transform FireTF;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        abandonedBullets = GameObject.Find("AbandonBullets").transform;
    }
    private void Update()
    {
        TimeCounter += Time.deltaTime;
        FindWayPoint();
        UpdateHealth();
    }
  
    public void FindWayPoint()
    {
        if (WayPoints.Length == i)
        {
            i = 0;
        }
        if (isAttack) return;
        //agent.SetDestination(WayPoints[i].position);
        transform.position =Vector3.MoveTowards(transform.position, WayPoints[i].position, 0.1f);
        transform.rotation*= Quaternion.Euler(new Vector3(0, 3, 0));
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
    public void UpdateHealth()
    {
        slider.value = HP / MaxHP;
        UpSlider.value = HP / MaxHP;
    }
}
