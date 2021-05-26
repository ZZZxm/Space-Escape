using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitNum : MonoBehaviour
{
    public Text numtxt;

    // Start is called before the first frame update
    void Start()
    {
        numtxt=GetComponent<Text>();
        numtxt.text=JourneyManager.getInstance().unitNum.ToString()+"/9";
        JourneyManager.getInstance().gameUIScript.unitNum=this;
    }

    
    public void Change() //当关卡数发生变化时，由GameUIController调用
    {
         numtxt.text=JourneyManager.getInstance().unitNum.ToString()+"/9";
    }
}
