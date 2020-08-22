using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Gun : MonoBehaviour
{
    public Transform FireTF;
    private float fireTimeCount = 0;
    public int bulletNumber=100;
    public float gunHot = 0;//枪口热量
    public float bulletSpeed=20;//子弹速度
    public AudioClip fireClip;
    private AudioSource audioSource;
    public GameObject bullet;
    public float TimeCounter=0;
    public float attack=50;
    private Transform abandonedBullets;
    public int speedLeavel = 0;
    public float up;
    private Camera camera;
    private bool isBigCamera = false;//用来控制摄像机缩放
    private bool isChangeCamera = false;//用来改变摄像机的位置;
    private Vector3 cameraLocalPosition;//用来记录摄像机的初始位置
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        abandonedBullets = GameObject.Find("AbandonBullets").transform;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraLocalPosition = camera.transform.localPosition;
    }
    private void Update()
    {
        CountAttack();
        ChangeBulletSpeed();
        ChangeFOV();
        TimeCounter += Time.deltaTime;
        fireTimeCount += Time.deltaTime;
        CountHot();
        Fire();
        ChangeCamera();
        //UpdateBulletNumber();
    }
    public void Fire()
    {
        if(Input.GetMouseButton(0))
        {
            if(bulletNumber>0&&TimeCounter>1-PlayerHealth.instance.Level*0.25)
            {
                audioSource.PlayOneShot(fireClip);
                bulletNumber--;
                gunHot += bulletSpeed * bulletSpeed;
                GameObject bulletGO = Instantiate(bullet, FireTF.position, Quaternion.LookRotation(FireTF.forward)) as GameObject;
                bulletGO.transform.SetParent(abandonedBullets);
                bulletGO.GetComponent<PlayerBullet>().bulletParent = this.transform;//子弹的归属是本角色
                bulletGO.GetComponent<PlayerBullet>().init(attack*PlayerHealth.instance.attackRate, bulletSpeed);//将选择好的速度给子弹
                TimeCounter = 0;
                Destroy(bulletGO, 10f);//生成的子弹10秒后销毁
            }
        }
    }
    private void CountHot()
    {
        if (fireTimeCount >= 1)
        {
            if(gunHot>PlayerHealth.instance.MaxGunHot)
            {
                //每秒减少15分之一生命值
                PlayerHealth.instance.HP -= PlayerHealth.instance.MaxHP/15;
            }
            gunHot = gunHot - PlayerHealth.instance.GunDown > 0 ? gunHot - PlayerHealth.instance.GunDown : 0;
            fireTimeCount = 0;
        }
    }
    public void CountAttack()//根据子弹速度计算子弹攻击力
    {
        if(bulletSpeed<=15)
        {
            attack = 50;
        }
        if(bulletSpeed>15)
        {
            attack = 50 + (bulletSpeed-15) * 5;
        }
            
    }
    public void ChangeBulletSpeed()//鼠标滚轮可以用来切换子弹发射速度
    {
        up = Input.GetAxis("Mouse ScrollWheel");
        if(up>0.1)
        {
            speedLeavel += 1;
            bulletSpeed += 1;
        }
        if(up<-0.1)
        {
            speedLeavel -= 1;
            bulletSpeed -= 1;
        }
        if(bulletSpeed>40)
        {
            bulletSpeed = 40;
        }
        if(bulletSpeed<8)
        {
            bulletSpeed = 8;
        }
    }
    public void ChangeFOV()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isBigCamera = !isBigCamera;
        }
        if (!isBigCamera)
        {
            camera.fieldOfView = 60;
        }
        if (isBigCamera)
        {
            camera.fieldOfView = 20;
        }
    }
    private void ChangeCamera()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            isChangeCamera = !isChangeCamera;
        }
        if(isChangeCamera)
        {
            camera.transform.localPosition = new Vector3(-5.1f,27.8f,8.82f);
        }
        if(!isChangeCamera)
        {
            camera.transform.localPosition= cameraLocalPosition;
        }
    }
}
