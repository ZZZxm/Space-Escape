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

    private GameObject[] smallEnemies;

    public int NumOfSmallEnemies;


    public void Start()  //实例化，参数设置为坐标
    {
        
        for (int i = 0; i < smallEnemyTiles.Length; i++) 
        {
            Instantiate(smallEnemyTiles[i], new Vector3(11,1,0), Quaternion.identity);
        }
    }
}
