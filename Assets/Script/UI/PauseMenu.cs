using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//暂停界面类
/*
  方法:1. 激活暂停界面
       2. 继续游戏
       3. 保存游戏
       4. 返回主菜单
*/
public class PauseMenu : MonoBehaviour
{
   public GameObject pauseUI;
   public bool isPause;

   public void Pause()
   {
       pauseUI.SetActive(true);
       Time.timeScale=0.0f;
       isPause=true;
   }

   public void Continue()
   {
       pauseUI.SetActive(false);
       Time.timeScale=1.0f;
       isPause=false;
   }

   public void Save()
   {

   }

   public void Back()
   {
       JourneyManager.getInstance().DestroyThis();
       JourneyManager.instance=null;
        Time.timeScale=1.0f;  //重要！！因为在暂停时使scale=0，所以不过用哪个按钮都要加这一行代码使得scale恢复正常
        SceneManager.LoadScene("MainMenu");
   }
}
