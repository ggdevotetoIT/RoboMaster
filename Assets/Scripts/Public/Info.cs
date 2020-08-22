using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class Info : MonoBehaviour
{
    public float HP = 750;
    public float MaxHP = 750;
    public float MaxGunHot;//最大枪口热量
    public float GunDown;//每秒枪口热量冷却值
    public float ReLiveTime;//复活时间
    public float Exp;
    public int Level = 1;
    public Slider slider;
    public Light light;
    public float reliveTimeCounter;
    public Color lightColor;
    public bool isRelive;//外面通过这个bool值控制复活
    public bool isReliveFinished;//子类通过这个bool值控制脚本是否启用
    public Transform GunTF;
    public float AllExp;//所获得的经验
    public float killNumber;
    public int maxBulletNumber;//最大子弹数量
    public float attackRate=1;//攻击力倍率
    private void Update()
    {
        UpdateLevel();
        UpdateHealth();
        LevelUp();
        if(HP<=0)
        {
            isRelive = true;
        }
        if(HP>=MaxHP)
        {
            HP = MaxHP;
        }
        if(isRelive)
        {
            reliveTimeCounter += Time.deltaTime;
        }
        if(reliveTimeCounter>=ReLiveTime)
        {
            ReLive();
        }
    }
    private void UpdateLevel()
    {
        switch (Level)
        {
            case 1:
                MaxHP = 750;
                MaxGunHot = 1600;
                GunDown = 500;
                ReLiveTime = 5;
                Exp = 2.5f;
                maxBulletNumber = 50;
                break;
            case 2:
                MaxHP = 1000;
                MaxGunHot = 3000;
                GunDown = 1000;
                ReLiveTime = 15;
                Exp = 5f;
                maxBulletNumber = 75;
                break;
            case 3:
                MaxHP = 1500;
                MaxGunHot = 6000;
                GunDown = 1500;
                ReLiveTime = 30;
                Exp = 7.5f;
                maxBulletNumber = 150;
                break;
            default:
                break;
        }
    }
    private void UpdateHealth()
    {
        slider.value = HP / MaxHP;
    }
    //复活后
    public void ReLive()
    {
        reliveTimeCounter = 0;
        isRelive = false;
        light.color = lightColor;
        isReliveFinished = true;
        HP = MaxHP;
        GunTF.localEulerAngles = new Vector3(0, 0, 0);
    }
    //升级
    public void LevelUp()
    {
        if(AllExp>=5&&AllExp<=10)
        {
            if(Level!=2)
            {
                Level = 2;
                HP = HP / 750 * 1000;
                //播放升级UI;
            }
        }
        if(AllExp>10)
        {
            if(Level!=3)
            {
                Level = 3;
                HP = HP / 1000 * 1500;
                //播放升级UI;
            }
        }
    }
}
