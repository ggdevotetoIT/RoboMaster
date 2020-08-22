using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class GreenBase : MonoBehaviour
{
    public float HP = 3000;
    public float MaxHp = 3000;
    public GameObject wall1;
    public GameObject wall2;
    public Slider slider;
    public static bool isGameOver;//用来判断游戏是否结束
    public static GreenBase instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        UpdateHealth();
        UpdateBase();
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
        UpdateHealth();
        isGameOver = true;
    }
    private void UpdateHealth()
    {
        slider.value = HP / MaxHp;
    }
    private void UpdateBase()
    {
        if (GreenAutoSolider.instance.HP == 0)
        {
            Destroy(wall1);
            Destroy(wall2);
        }
    }
}
