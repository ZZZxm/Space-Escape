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
                    break;
            }
             case "role1":
            {
                i=1;
                costs=200;
                  
                    break;
            }
             case "role2":
            {
                i=2;
                costs=500;
                 
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
        switch(i)
        {
            case 0:
            {
                    JourneyManager.getInstance().playerHPMax = 500;
                    JourneyManager.getInstance().atts[0] = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerCurHP = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerMPMax = 200;
                    JourneyManager.getInstance().atts[1] = JourneyManager.getInstance().playerMPMax;
                    JourneyManager.getInstance().playerCurMP = JourneyManager.getInstance().playerMPMax;
                  
                    break;
            }
             case 1:
            {
                    JourneyManager.getInstance().playerHPMax = 300;
                    JourneyManager.getInstance().atts[0] = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerCurHP = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerMPMax = 250;
                    JourneyManager.getInstance().atts[1] = JourneyManager.getInstance().playerMPMax;
                    JourneyManager.getInstance().playerCurMP = JourneyManager.getInstance().playerMPMax;
                     
                    break;
            }
             case 2:
            {
                    JourneyManager.getInstance().playerHPMax = 400;
                    JourneyManager.getInstance().atts[0] = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerCurHP = JourneyManager.getInstance().playerHPMax;
                    JourneyManager.getInstance().playerMPMax = 150;
                    JourneyManager.getInstance().atts[1] = JourneyManager.getInstance().playerMPMax;
                    JourneyManager.getInstance().playerCurMP = JourneyManager.getInstance().playerMPMax;
                   
                    break;
            }
            default:
            {
                break;
            }
        }
        }
        inform.text="";
    }
}
