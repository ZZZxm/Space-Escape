using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地图生成器，实例化代码写在此处，方式：给定坐标生成
  //目前的设计是地图生成器只生成陷阱，基本地形固定
/*
   属性：trap prefab
   方法：instanciate
*/
public class MapGenerator : MonoBehaviour
{
   public GameObject[] trapTiles;

   public void Instanciate()  //实例化，参数设置为坐标
   {
        //实例化代码
   }

}
