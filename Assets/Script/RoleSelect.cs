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
        }
        inform.text="";
    }
}
