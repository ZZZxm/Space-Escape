using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//关卡控制类
  //负责生成地图、道具、玩家等（场景之中除UI的实例）
  //负责当前关卡的游戏逻辑：敌人AI、计时
/*
  属性:1. 地图生成器
       2. 敌人生成器
       3. 玩家生成器
       4. 道具生成器
  方法:1. Awake
       2. 初始化地图
       3. 初始化敌人
       4. 初始化玩家
       5. 敌人AI
       6. 本关卡结束
*/
public class UnitManager : MonoBehaviour
{
    private MapGenerator mapGenerator;
    private EnemyGenerator enemyGenerator;
    private PlayerGenerator playerGenerator;
    private ItemGenerator itemGenerator;
    
    //public List<Enemy> enemies; 

    void Awake()
    {
        mapGenerator=GetComponent<MapGenerator>();
        enemyGenerator=GetComponent<EnemyGenerator>();
        playerGenerator=GetComponent<PlayerGenerator>();
        itemGenerator=GetComponent<ItemGenerator>();
    }

    public void InitMap() //初始化地图
    {

    }

    public void InitEnemy()  //初始化敌人
    {

    }

    public void InitPlayer()  //初始化玩家
    {

    }

    public void EnemyAI()  //敌人AI
    {

    }

    public void Finish()  //本关卡结束
    {

    }
}
