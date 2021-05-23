using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNum : MonoBehaviour
{
    public Text numtxt;

    // Start is called before the first frame update
    void Start()
    {
        numtxt=GetComponent<Text>();
        numtxt.text=JourneyManager.getInstance().playNum.ToString();
        JourneyManager.getInstance().gameUIScript.pLayerNum=this;
    }

    
    public void Change() //当关卡数发生变化时，由GameUIController调用
    {
         numtxt.text=JourneyManager.getInstance().playNum.ToString();
    }
}
