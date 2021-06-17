using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MPbar : MonoBehaviour
{
    public Text MPtxt;
    public Image MP;
 

    // Start is called before the first frame update
    void Start()
    {
        MP=GetComponent<Image>();
        int chp=JourneyManager.getInstance().playerCurMP;
        int mhp=JourneyManager.getInstance().playerMPMax;
        MP.fillAmount= (float)chp / (float)mhp;
        MPtxt.text=chp.ToString()+"/"+mhp.ToString();
        JourneyManager.getInstance().gameUIScript.mpScript=this;
    }

   public void Change() //当前MP或最大MP发生变化时，由GameUIController调用
   {
        int chp=JourneyManager.getInstance().playerCurMP;
        int mhp=JourneyManager.getInstance().playerMPMax;
        MP.fillAmount= (float)chp / (float)mhp;
        MPtxt.text=chp.ToString()+"/"+mhp.ToString();
   }
}
