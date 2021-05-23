using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Text HPtxt;
    public Image HP;


    // Start is called before the first frame update
    void Start()
    {
        HP=GetComponent<Image>();
        int chp=JourneyManager.getInstance().playerCurHP;
        int mhp=JourneyManager.getInstance().playerHPMax;
        HP.fillAmount= (float)chp / (float)mhp;
        HPtxt.text=chp.ToString()+"/"+mhp.ToString();
        JourneyManager.getInstance().gameUIScript.hpScript=this;
    }

   public void Change() //当前HP或最大HP发生变化时，由GameUIController调用
   {
        int chp=JourneyManager.getInstance().playerCurHP;
        int mhp=JourneyManager.getInstance().playerHPMax;
        HP.fillAmount= (float)chp / (float)mhp;
        HPtxt.text=chp.ToString()+"/"+mhp.ToString();
   }
}
