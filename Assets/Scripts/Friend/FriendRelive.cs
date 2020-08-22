using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
/// <summary>
/// 
/// </summary>
public class FriendRelive : MonoBehaviour
{
    private FriendGun gun;
    private FriendNavigation navigation;
    private FriendInfo friendInfo;
    private NavMeshAgent navMeshAgent;
    public Slider upSlider;
    public GameObject image1;
    public GameObject image2;
    public Slider reliveSlider;
    private void Start()
    {
        gun = GetComponent<FriendGun>();
        navigation = GetComponent<FriendNavigation>();
        friendInfo = GetComponent<FriendInfo>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ReliveSlider();
        if (friendInfo.isReliveFinished)
        {
            navigation.enabled = true;
            gun.enabled = true;
            navMeshAgent.enabled = true;
            friendInfo.isReliveFinished = false;
        }
        UpdateHP();
        UpdateLevel(); ;
    }
    private void UpdateHP()
    {
        upSlider.value = friendInfo.HP / friendInfo.MaxHP; ;
    }
    private void UpdateLevel()
    {
        if (friendInfo.Level == 2)
        {
            image1.SetActive(true);
        }
        if (friendInfo.Level == 3)
        {
            image2.SetActive(true);
        }
    }
    private void ReliveSlider()
    {
        if (friendInfo.HP <= 0)
        {
            reliveSlider.gameObject.SetActive(true);
            reliveSlider.value = friendInfo.reliveTimeCounter / friendInfo.ReLiveTime;
        }
        if (friendInfo.isReliveFinished)
        {
            reliveSlider.gameObject.SetActive(false);
        }
    }
}
