using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxNum : MonoBehaviour
{
    public Text numtxt;

    // Start is called before the first frame update
    void Start()
    {
        numtxt=GetComponent<Text>();
        numtxt.text=JourneyManager.getInstance().boxNum.ToString();
        JourneyManager.getInstance().gameUIScript.boxNum=this;
    }

    
    public void Change() //当宝箱数发生变化时，由GameUIController调用
    {
         numtxt.text=JourneyManager.getInstance().boxNum.ToString();
    }
}
