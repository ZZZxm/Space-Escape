using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NewJourney : MonoBehaviour
{
    public void OnClickButton()
   {
      JourneyManager.getInstance().isWin=0;
      JourneyManager.getInstance().playNum++;
      JourneyManager.getInstance().unitNum=1;
      JourneyManager.getInstance().playTime=0;
      SceneManager.LoadScene("RoleSelect");
   }
}
