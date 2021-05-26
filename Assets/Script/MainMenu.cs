using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//开始界面类
/*
  方法:1. 新游戏
       2. 加载存档
       3. 更改设置
       4. 退出游戏
*/
public class MainMenu : MonoBehaviour
{

    public void Awake()
    {
        Screen.SetResolution(1200, 550, false);
    }

    public void NewGame()  //新游戏
    {
        SceneManager.LoadScene("GameStart");
    }

    public void LoadGame()  //加载游戏
    {

    }

    public void Settings()  //设置
    {

    }

    public void Quit()  //退出游戏
    {
        Application.Quit();
    }
}
