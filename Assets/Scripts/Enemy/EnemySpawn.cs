using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class EnemySpawn : MonoBehaviour
{
    public WayLine[] lines;
    public GameObject enemy;
    public static int EnemyNumber ;//可以通过生成敌人数量改变游戏难度,也可以利用这个改变敌人出枪频率
    private int SpawnIndex;
    public static GameObject[] allSpawnEnemys ;
    private void Awake()
    {
        EnemyNumber = GameManager.instance.enemyNumber;
        allSpawnEnemys = new GameObject[EnemyNumber] ;
        CalculateWayPoints();
        for(int i=0;i<EnemyNumber;i++)
        {
            CreateEnemy();
        }
    }
    private void CreateEnemy()
    {
        WayLine[] IsAbledLines = SelectIsAbled();
        if(IsAbledLines!=null)
        {
            WayLine enemyline = IsAbledLines[Random.Range(0, IsAbledLines.Length)];
            enemyline.isAbled = false;
            GameObject enemyGO = Instantiate(enemy, enemyline.WayPoints[0], Quaternion.identity) as GameObject;
            Navigation navigation = enemyGO.GetComponent<Navigation>();
            navigation.line = enemyline;
            allSpawnEnemys[SpawnIndex] = enemyGO;
            SpawnIndex++;
        }
    }
    //将每个路线上的点的位置放好
    private void CalculateWayPoints()
    {
        lines = new WayLine[transform.childCount];
        for(int i=0;i<lines.Length;i++)
        {
            lines[i] = new WayLine(transform.GetChild(i).childCount);
            for(int index=0;index<lines[i].WayPoints.Length;index++)
            {
                lines[i].WayPoints[index] = transform.GetChild(i).GetChild(index).position;
            }
        }
    }
    private WayLine[] SelectIsAbled()
    {
        List<WayLine> allLines = new List<WayLine>();
        foreach (var item in lines)
        {
            if(item.isAbled)
            {
                allLines.Add(item);
            }
        }
        return allLines.ToArray();
    }
}
