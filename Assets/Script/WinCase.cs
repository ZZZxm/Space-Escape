using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCase : MonoBehaviour
{
   public Text numtxt;

    // Start is called before the first frame update
    void Start()
    {
        numtxt=GetComponent<Text>();
        int con=JourneyManager.getInstance().winCase;
        switch(con)
        {
            case 0:
            {
                numtxt.text="打败所有敌人";
                break;
            }
            case 1:
            {
                numtxt.text="获得所有宝箱";
                break;
            }
            case 2:
            {
                numtxt.text="存活30s";
                break;
            }
            case 3:
            {
                numtxt.text = "Boss战";
                break;
            }
            default:
            {
                numtxt.text="";
                break;
            }
        }
        JourneyManager.getInstance().gameUIScript.winCase=this;
    }

    
    public void Change() //当关卡数发生变化时，由GameUIController调用
    {
        int con=JourneyManager.getInstance().winCase;
        switch(con)
        {
            case 0:
            {
                numtxt.text="打败所有敌人";
                break;
            }
            case 1:
            {
                numtxt.text="获得所有宝箱";
                break;
            }
            case 2:
            {
                numtxt.text="存活30s";
                break;
            }
            default:
            {
                numtxt.text="";
                break;
            }
        }
    }
}
