using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 
/// </summary>
public class Navigation : MonoBehaviour
{
    private NavMeshAgent  agent;
    public int i = 0;
    public float waitTime = 0f;
    public WayLine line;
    private EnemyGun enemyGun;
    private EnemyInfo enemyInfo;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyGun = GetComponent<EnemyGun>();
        enemyInfo = GetComponent<EnemyInfo>();
        //agent.SetDestination(agent.transform.position);
    }
    private void Update()
    {
        if(i==4)
        {
            waitTime += Time.deltaTime;
        }
        else
        {
            waitTime = 0f;
        }
        FindWayPoint();
    }
    public void FindWayPoint()
    {
        if(enemyGun.bulletNumber==0||(enemyInfo.HP/enemyInfo.MaxHP)<=0.25f)//当没有子弹或者生命低于百分之20时,回补给站补给
        {
            i = 4;
        }
        if (line.WayPoints.Length==i) return;
        //暂时到达最后一个点停止,后面到达最后一个点将进行巡逻攻击,当子弹用完原返回至补给站(第五个点,停留5秒钟)
        if (enemyGun.isAttack) return;
        agent.SetDestination(line.WayPoints[i]);
        if (Vector3.SqrMagnitude(transform.position - line.WayPoints[i]) < 10f)
        {
            //若到第五个点则进入补给站等待5秒
            if(i!=4)
            {
                i++;
            }
            else
            {
                if(waitTime>=5)
                {
                    i++;
                }
            }
            
        }
    }
}
