using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenBox : MonoBehaviour
{
     public Text numtxt;

    // Start is called before the first frame update
    void Start()
    {
        numtxt=GetComponent<Text>();
        numtxt.text="";
        JourneyManager.getInstance().gameUIScript.openBox=this;
    }

    
    public void ChangeTo(string txt) 
    {
       numtxt.text=txt;
    }
}
