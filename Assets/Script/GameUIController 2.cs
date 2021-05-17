using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏内血条道具等UI生成及控制类
  //假设整体prefab能将子类component都按名生成
    //如若不行再分部分生成
/*
  属性：游戏界面整体UI gameUI
  方法:1. 初始化
       2. 设置当前血条
       3. 设置最大血条
*/
public class GameUIController : MonoBehaviour
{
    public GameObject gameUI;

    public void Init()  //初始化
    {
      //实例化代码
    }

    public void SetCurrentBlood()  //设置当前血条
    {
        //获得gameUI下的血条图片及txt的component并更新它们的显示
    }

     public void SetMaxBlood()  //设置最大血条
    {
        //获得gameUI下的血条txt的component并更新它的显示
    }
}
