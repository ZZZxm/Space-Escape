using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleInitial : MonoBehaviour
{
    public Text money;

    public Text cost;

    public Text inform;
    // Start is called before the first frame update
    void Start()
    {
        money.text=JourneyManager.getInstance().money.ToString();
        cost.text="";
        inform.text="";
    }

    public void OnClickStart()
    {
        int moneys=int.Parse(money.text);
        int costs=int.Parse(cost.text);
        if(moneys<costs)
        {
            inform.text="金钱不足!";
            return;
        }
        else
        {
            JourneyManager.getInstance().money-=costs;
            JourneyManager.getInstance().StartJourney();
        }
    }

}
