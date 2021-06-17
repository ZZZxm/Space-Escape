using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLose : MonoBehaviour
{
  
   public void OnClickButton()
   {
       Time.timeScale=1.0f;
       
      JourneyManager.getInstance().nextJourney();
   }
}
