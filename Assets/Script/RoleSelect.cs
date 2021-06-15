using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleSelect : MonoBehaviour
{

    public Text cost;
    public int i;
    public int costs;
    public Text inform;
    // Start is called before the first frame update
    void Start()
    {
         switch(gameObject.name)
        {
            case "role0":
            {
                i=0;
                costs=0;
                    JourneyManager.getInstance().playerHPMax = 500;
                    JourneyManager.getInstance().atts[0] = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerCurHP = JourneyManager.getInstance().playerHPMax;
                    break;
            }
             case "role1":
            {
                i=1;
                costs=200;
                    JourneyManager.getInstance().playerHPMax = 500;
                    JourneyManager.getInstance().atts[0] = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerCurHP = JourneyManager.getInstance().playerHPMax;
                    break;
            }
             case "role2":
            {
                i=2;
                costs=500;
                    JourneyManager.getInstance().playerHPMax = 500;
                    JourneyManager.getInstance().atts[0] = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerCurHP = JourneyManager.getInstance().playerHPMax;
                    break;
            }
            default:
            {
                break;
            }
        }
        
    }

    public void OnValueChanged(bool value)
    {
        if(value)
        {
            cost.text=costs.ToString();
            JourneyManager.getInstance().playerInfo=i;
        }
        inform.text="";
    }
}
