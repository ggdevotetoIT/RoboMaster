using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WayLine
{
    public Vector3[] WayPoints;//存所有的路点,长度在构造函数声明
    public bool isAbled;//该条路是否可用
    public WayLine(int number)
    {
        WayPoints = new Vector3[number];
        isAbled = true;
    }
}
