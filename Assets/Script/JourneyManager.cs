using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏整体进程的控制类
  //人物附加的全局变量如成就值或者在一个回合里的变量如金钱道具等都定义在这个类里
    //全局变量一回合结束不清零，一回合里的变量回合结束时清零
      //暂不清楚静态物体脚本中实例化的物体在场景切换时要不要destroy，先不考虑，后续有需要再加destroy部分
/*
  属性:1. 静态实例
       2. 关卡控制类unitScript
       3. 游戏内UI控制类gameUIScript
  方法:1. 准备游戏Awake
       2. 选择游戏场景并进行初始化
       3. 一个关卡结束操作
*/
public class JourneyManager : MonoBehaviour
{
   public static JourneyManager instance=null;

   private UnitManager unitScript;
   private GameUIController gameUIScript;

    //Awake is always called before any Start functions
    void Awake() //准备游戏
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);    
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        unitScript = GetComponent<UnitManager>();
        gameUIScript = GetComponent<GameUIController>();
    }

    void StartUnit(string name)  //选择游戏场景并进行初始化
    {
        //初始化该场景
    }

    void FinishUint(bool win)  //一个关卡结束
    {
        //结束操作，根据是否胜利切换场景
    }
}
