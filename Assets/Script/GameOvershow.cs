using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOvershow : MonoBehaviour
{
    public Text wintxt;
    public Text journey;
    public Text unit;
    public Text money;
    public Text time;
    // Start is called before the first frame update
    void Start()
    {
        int win=JourneyManager.getInstance().isWin;
        int journeys=JourneyManager.getInstance().playNum;
        int units=JourneyManager.getInstance().unitNum;
        float times=JourneyManager.getInstance().playTime;
        int moneys=0;
        if(win==0)
        {
            wintxt.text="逃脱失败";
        }
        else{
            wintxt.text="逃脱成功";
        }
        journey.text=journeys.ToString();
        unit.text=units.ToString();
       // money.text=JourneyManager.getInstance().money.ToString();
        time.text=times.ToString();
        if(win==1)
        {
            moneys+=300;
            if(times<300)
            {
                moneys+=100;
            }
        }
        else{
            moneys+=units*10;
        }
        JourneyManager.getInstance().money+=moneys;
        money.text=JourneyManager.getInstance().money.ToString();
    }

}
