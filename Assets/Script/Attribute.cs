using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attribute : MonoBehaviour
{
     public Text txt;
     public int i;

    // Start is called before the first frame update
    void Start()
    {
        switch(gameObject.name)
        {
            case "att0":
            {
                i=0;
                break;
            }
             case "att1":
            {
                i=1;
                break;
            }
             case "att2":
            {
                i=2;
                break;
            }
             case "att3":
            {
                i=3;
                break;
            }
            default:
            {
                break;
            }
        }
        txt=GetComponent<Text>();
        txt.text=JourneyManager.getInstance().atts[i].ToString();
        JourneyManager.getInstance().gameUIScript.attributes[i]=this;
    }

    
    public void Change() //当数值发生变化时，由GameUIController调用
    {
         txt.text=JourneyManager.getInstance().atts[i].ToString();
    }
}
