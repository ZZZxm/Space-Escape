using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownList : MonoBehaviour
{
    public Dropdown thisDrop;
    public int i;
    public string itemname;

     void Start()
    {
        switch(gameObject.name)
        {
            case "Dropdown0":
            {
                i=0;
                itemname="帽子";
                break;
            }
             case "Dropdown1":
            {
                i=1;
                itemname="护甲";
                break;
            }
             case "Dropdown2":
            {
                i=2;
                itemname="鞋子";
                break;
            }
             case "Dropdown3":
            {
                i=3;
                itemname="饰品";
                break;
            }
            default:
            {
                break;
            }
        }
        thisDrop=GetComponent<Dropdown>();
        thisDrop.options.Clear();
        thisDrop.options.Add(new Dropdown.OptionData("无"));
        for(int j=1;j<=4;++j)
        {
            if(JourneyManager.getInstance().clothes[i,j-1])
            {
                thisDrop.options.Add(new Dropdown.OptionData(itemname+j.ToString()));
            }
        }
       
        JourneyManager.getInstance().gameUIScript.dropdowns[i]=this;
        thisDrop.value=JourneyManager.getInstance().nowWear[i];
        
    }

    
    public void Change(int j) //当数值发生变化时，由GameUIController调用
    {
       
       if(j<1||j>4) return;
       thisDrop.options.Add(new Dropdown.OptionData(itemname+j.ToString())); 
       
    }

    public void Clear(){        //清空选项
        thisDrop.options.Clear();
        thisDrop.value=0;
    }

    
   public void OnChoose(int index)
   {                                         //针对十六种防具的每一种给出对应措施
       int j=-1;
       index=thisDrop.value;
       int changeHP=0;
       int changeMP=0;
       int changeAgile=0;
       int changePatience=0;
      if(thisDrop.options[index].text.Length==3)
      {
          j=int.Parse(thisDrop.options[index].text[2].ToString())-1;
          changeHP=JourneyManager.getInstance().CLOTHMAP[i,j]["HP"];
          changeMP=JourneyManager.getInstance().CLOTHMAP[i,j]["MP"];
          changeAgile=JourneyManager.getInstance().CLOTHMAP[i,j]["Agile"];
          changePatience=JourneyManager.getInstance().CLOTHMAP[i,j]["Patience"];
      }
       int nowHP=0;
       int nowMP=0;
       int nowAgile=0;
       int nowPatience=0;
       int nowindex=JourneyManager.getInstance().nowWear[i]-1;
       if(nowindex>=0)
       {
           nowHP=JourneyManager.getInstance().CLOTHMAP[i,nowindex]["HP"];
           nowMP=JourneyManager.getInstance().CLOTHMAP[i,nowindex]["MP"];
           nowAgile=JourneyManager.getInstance().CLOTHMAP[i,nowindex]["Agile"];
           nowPatience=JourneyManager.getInstance().CLOTHMAP[i,nowindex]["Patience"];
       }
       JourneyManager.getInstance().nowWear[i]=j+1;
       JourneyManager.getInstance().ChangeAtts(0,changeHP-nowHP);
       JourneyManager.getInstance().ChangeAtts(1,changeMP-nowMP);
       JourneyManager.getInstance().ChangeAtts(2,changeAgile-nowAgile);
       JourneyManager.getInstance().ChangeAtts(3,changePatience-nowPatience);


   }
}
