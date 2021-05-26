using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人生成器，实例化代码写在此处，方式：给定坐标生成
/*
   属性：enemy prefab
   方法：instanciate
*/
public class EnemyGenerator : MonoBehaviour
{    
    public GameObject[] smallEnemyTiles;
    public GameObject[] midEnemyTiles;
    public GameObject[] bossEnemyTiles;

    public void Create()  //实例化，参数设置为坐标
    {
        //实例化代码
    }
}
