using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//游戏内血条道具等UI生成及控制类
  //假设整体prefab能将子类component都按名生成
    //如若不行再分部分生成
/*
  属性：游戏界面整体UI gameUI
*/
public class GameUIController : MonoBehaviour
{
    public GameObject gameUI;

    public HPbar hpScript;
    public MPbar mpScript;

    public Money moneyScript;

    public PlayerNum pLayerNum;

    public UnitNum unitNum;

    public PlayTime playTime;

    public UnitTime unitTime;

    public BoxNum boxNum;

    public WinCase winCase;

    public Attribute[] attributes=new Attribute[4];

    public Items[] items=new Items[4];

    public DropdownList[] dropdowns=new DropdownList[4];

    public OpenBox openBox;

    void Awake() 
    {
      Instantiate(gameUI);
      JourneyManager.getInstance().gameUIScript=this;
     
    }
    private void Start() {
      StartCoroutine(Timer()); //计时开始
    }

  IEnumerator Timer() 
  {           //计时器
    while (true) {
        yield return new WaitForSeconds(1.0f);
        JourneyManager.getInstance().unitTime++;   
        JourneyManager.getInstance().playTime++; 
        ChangePlayTime();
        ChangeUnitTime();
        if(JourneyManager.getInstance().winCase==2&&JourneyManager.getInstance().unitTime==30)
        {
          GameObject root = GameObject.Find("UnitCanvas(Clone)");
          GameObject uw=root.transform.Find("UnitWin").gameObject;
          uw.SetActive(true);
          Time.timeScale=0.0f;
        }
    }
  }
  private void OnDestroy() {
    StopCoroutine(Timer());
  }
    public void Init()  //初始化
    {
      //实例化代码
    }

    public void ChangeHP() //当前HP或最大HP发生变化时，由JouenryManager修改全局血量并调用此函数
    {
      hpScript.Change();
    }

    public void ChangeMP() //当前MP或最大MP发生变化时，由JouenryManager修改全局蓝条并调用此函数
    {
      mpScript.Change();
    }

    public void ChangeMoney()//当前Money发生变化时，由JouenryManager修改全局蓝条并调用此函数
    {
      moneyScript.Change();
    }

    public void ChangePlayerNum()
    {
      pLayerNum.Change();
    }

     public void ChangeUnitNum()
    {
      unitNum.Change();
    }

    public void ChangePlayTime()
    {
      playTime.Change();
    }

    public void  ChangeUnitTime()
    {
      unitTime.Change();
    }

    public void ChangeBoxNum()
    {
      boxNum.Change();
    }

    public void ChangeWinCase()
    {
      winCase.Change();
    }

    public void ChangeAttibute(int i)
    {
      if(attributes[i]!=null)
      {
         attributes[i].Change();
      }
    }

    public void ChangeItem(int i)
    {
      if(items[i]!=null)
      {
        items[i].Change();
      }
    }

    public void ChangeDrop(int i,int j)
    {
      if(dropdowns[i]!=null)
      {
        dropdowns[i].Change(j);
      }
    }

    public void ClearDrop(int i)
    {
      if(dropdowns[i]!=null)
      {
        dropdowns[i].Clear();
      }
    }

    public void ChangeOpenBox(string txt)
    {
      openBox.ChangeTo(txt);
    }
   
}
