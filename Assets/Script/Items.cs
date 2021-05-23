using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
     public Text txt;
     public int i;

    // Start is called before the first frame update
    void Start()
    {
        switch(gameObject.name)
        {
            case "item0":
            {
                i=0;
                break;
            }
             case "item1":
            {
                i=1;
                break;
            }
             case "item2":
            {
                i=2;
                break;
            }
             case "item3":
            {
                i=3;
                break;
            }
            default:
            {
                break;
            }
        }
        txt.text=JourneyManager.getInstance().items[i].ToString();
        JourneyManager.getInstance().gameUIScript.items[i]=this;
    }

    
    public void Change() //当数值发生变化时，由GameUIController调用
    {
          txt.text=JourneyManager.getInstance().items[i].ToString();
    }
}
