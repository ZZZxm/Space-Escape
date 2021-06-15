using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
   public void HP()
   {
       JourneyManager.getInstance().UseItem(0);
   }

   public void MP()
   {
       JourneyManager.getInstance().UseItem(1);
   }

   public void Agile()
   {
       JourneyManager.getInstance().UseItem(2);
   }

   public void Patience()
   {
        JourneyManager.getInstance().UseItem(3);
   }
}
