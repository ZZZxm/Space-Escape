using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text moneytxt;

    // Start is called before the first frame update
    void Start()
    {
        moneytxt.text=JourneyManager.getInstance().money.ToString();
        JourneyManager.getInstance().gameUIScript.moneyScript=this;
    }

    
    public void Change() //当金钱发生变化时，由GameUIController调用
    {
         moneytxt.text=JourneyManager.getInstance().money.ToString();
    }
}
