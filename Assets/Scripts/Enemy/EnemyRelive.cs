using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class EnemyRelive : MonoBehaviour
{
    private EnemyGun gun;
    private Navigation navigation;
    private EnemyInfo enemyInfo;
    private NavMeshAgent navMeshAgent;
    public Slider upSlider;
    public GameObject image1;
    public GameObject image2;
    public Slider reliveSlider;
    private void Start()
    {
        gun = GetComponent<EnemyGun>();
        navigation = GetComponent<Navigation>();
        enemyInfo = GetComponent<EnemyInfo>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ReliveSlider();
        if(enemyInfo.isReliveFinished)
        {
            navigation.enabled = true;
            gun.enabled = true;
            navMeshAgent.enabled = true;
            enemyInfo.isReliveFinished = false;
        }
        UpdateHP();
        UpdateLevel();;
    }
    private void UpdateHP()
    {
        upSlider.value = enemyInfo.HP / enemyInfo.MaxHP; ;
    }
    private void UpdateLevel()
    {
        if(enemyInfo.Level==2)
        {
            image1.SetActive(true);
        }
        if(enemyInfo.Level==3)
        {
            image2.SetActive(true);
        }
    }
    private void ReliveSlider()
    {
        if(enemyInfo.HP<=0)
        {
            reliveSlider.gameObject.SetActive(true);
            reliveSlider.value = enemyInfo.reliveTimeCounter / enemyInfo.ReLiveTime;
        }
        if(enemyInfo.isReliveFinished)
        {
            reliveSlider.gameObject.SetActive(false);
        }
    }
}
