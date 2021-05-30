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

    public GameObject player;

    public float intervalTime = 3.0f;

    public GameObject flyingMonster;


    public void Start()  //实例化，参数设置为坐标
    {
        player = GameObject.FindGameObjectWithTag("Player");
        NumOfSmallEnemies = 0;
        InvokeRepeating("CreateSmallMonster", 0.5f, intervalTime);
        flyingMonster = (GameObject)Resources.Load("Prefabs/Enemy/Flying Monster");
    }

    private void Update() 
    {

    }

    private void CreateSmallMonster()
    {
        int hp = player.GetComponent<PlayerController>().currentBlood;
        if (hp <= 0)
        {
            CancelInvoke();
        }
        else
        {
            Vector3 pos = new Vector3(Random.Range(-13, 13), Random.Range(-13, 13), 0);
            Instantiate(flyingMonster, pos, Quaternion.identity);
        }
    }
}
