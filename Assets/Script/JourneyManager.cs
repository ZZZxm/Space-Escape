using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//游戏整体进程的控制类
//人物附加的全局变量如成就值或者在一个回合里的变量如金钱道具等都定义在这个类里
//全局变量一回合结束不清零，一回合里的变量回合结束时清零
//暂不清楚静态物体脚本中实例化的物体在场景切换时要不要destroy，先不考虑，后续有需要再加destroy部分
/*
  属性:1. 静态实例
       2. 关卡控制类unitScript
       3. 游戏内UI控制类gameUIScript
  方法:1. 准备游戏Awake
       2. 选择游戏场景并进行初始化
       3. 一个关卡结束操作
*/
public class JourneyManager : MonoBehaviour
{
    public static JourneyManager instance = null;

    private UnitManager unitScript;
    public GameUIController gameUIScript;
    /*此处开始为全局变量*/
    public int money = 500; //金钱
    public int playNum = 1; //当前回合数，需手动调gameui更改

    public int winNum = 0; // 获胜回合数

    public Dictionary<string, int>[,] CLOTHMAP = new Dictionary<string, int>[4, 4]; //16种防具的属性效果存储在字典数组里

    public int[] ITEMPOWER = new int[4];  //四种道具的作用效果存储在数组里 四种道具:回血、回蓝、加敏捷值、加耐力值【一一对应】

    public EnemyGenerator enemyGenerator;

    public int LEVEL_PER_JOURNEY = 2;

    /*全局变量部分结束*/

    /*此处开始为回合变量*/
    public int playerHPMax;
    public int playerCurHP;
    public int playerMPMax = 100;
    public int playerCurMP = 100;

    public int playerInfo; //选择哪种角色
    public int unitNum = 1; //当前关卡数，需手动调gameui更改

    public float playTime = 0;  //本回合所用时，单位s,需手动调gameui更改

    public int[] items = new int[4];  //四种道具数量:回血、回蓝、加敏捷值、加耐力值【一一对应】
    public int[] atts = new int[4]; //四种属性：最大血、最大蓝，敏捷值、耐力值【一一对应】
    public int[] initalAtts = new int[4]; //人物四种属性基本初始值，与上面相同顺序

    public bool[,] clothes = new bool[4, 4]; //四种防具：帽子、护甲、鞋子、饰品【一一对应】
                                             //每种防具四种等级装备，一共16个防具

    public int[] nowWear = new int[4]; //人物当前穿着防具,0表示没穿，1表示编号1

    public int isWin;  //当前回合是否成功逃脱，0表示失败，1表示成功

   /*此处开始为关卡变量*/
   public int boxNum=0;  //当前关卡剩余宝箱数
   public int winCase=0;  //当前关卡胜利条件，需手动调gameui更改
    public PlayerController playerController;


    //public bool canOut=false; //能否通过关卡出口进入下一关，无Ui

    public float unitTime = 0;  //当前关卡从进入开始所用时间，单位s,需手动调gameui更改

    public int roomNumber = 2;

    public int tileStyle = 0;

    /*关卡变量部分结束*/


    //Awake is always called before any Start functions
    void Awake() //准备游戏
    {
        //Check if instance already exists
        if (instance == null)
            //if not, set instance to this
            instance = this;
        //If instance already exists and it's not this:
        else if (instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        /* ---------------------------         JourneyManager一些属性的初始化     ------------------------------------      */
        unitNum = 1;
        playNum = 1;
        winNum = 0;
        LEVEL_PER_JOURNEY = 2;
        //道具数量初始化
        items[0] = 5;
        items[1] = 5;
        items[2] = 5;
        items[3] = 5;
        //拥有防具数量初始化
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                clothes[i, j] = false;
            }
        }
        clothes[0, 0] = true;
        clothes[0, 1] = true;
        clothes[0, 2] = true;
        clothes[0, 3] = true;
        //穿着防具初始化
        nowWear[0] = 0;
        nowWear[1] = 0;
        nowWear[2] = 0;
        nowWear[3] = 0;
        //若是初始人物有装备则相应初始属性也要改变
        //16种防具属性初始化
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                CLOTHMAP[i, j] = new Dictionary<string, int>();
            }
        }
        for (int j = 0; j < 4; ++j)    //帽子
        {
            CLOTHMAP[0, j].Add("HP", 50 * (j + 1));
            CLOTHMAP[0, j].Add("MP", 0);
            CLOTHMAP[0, j].Add("Agile", 0);
            CLOTHMAP[0, j].Add("Patience", 0);
        }
        for (int j = 0; j < 4; ++j)     //护甲
        {
            CLOTHMAP[1, j].Add("HP", 0);
            CLOTHMAP[1, j].Add("MP", 0);
            CLOTHMAP[1, j].Add("Agile", 0);
            CLOTHMAP[1, j].Add("Patience", 0);
        }
        for (int j = 0; j < 4; ++j)    //鞋子
        {
            CLOTHMAP[2, j].Add("HP", 0);
            CLOTHMAP[2, j].Add("MP", 0);
            CLOTHMAP[2, j].Add("Agile", 0);
            CLOTHMAP[2, j].Add("Patience", 0);
        }
        for (int j = 0; j < 4; ++j)    //饰品
        {
            CLOTHMAP[3, j].Add("HP", 0);
            CLOTHMAP[3, j].Add("MP", 0);
            CLOTHMAP[3, j].Add("Agile", 0);
            CLOTHMAP[3, j].Add("Patience", 0);
        }
        //四属性及血蓝量的初始化 必须在防具属性初始化之后
        //InitializedWithClothes();
        //四种道具效果初始化
        ITEMPOWER[0] = 50;
        ITEMPOWER[1] = 50;
        ITEMPOWER[2] = 5;
        ITEMPOWER[3] = 5;

        boxNum = 3;
        winCase = 2;
        money = 500;
        initWincase();
        /*  --------------------------------        JourneyManager一些属性的初始化     ----------------------------------      */
        unitScript = GetComponent<UnitManager>();
        //gameUIScript = GetComponent<GameUIController>();
    }

    public void ResetJourneyManager()  //回合之间切换一些变量的重置
    {
        //道具数量初始化
        items[0] = 0;
        items[1] = 0;
        items[2] = 0;
        items[3] = 0;

        //拥有防具数量初始化
        for (int i = 0; i < 4; ++i)
        {
            for (int j = 0; j < 4; ++j)
            {
                clothes[i, j] = false;
            }
        }

        //人物属性初始化
        atts[0] = 100;
        atts[1] = 100;
        atts[2] = 100;
        atts[3] = 100;
        playerHPMax = atts[0];
        playerCurHP = playerHPMax;
        playerMPMax = atts[1];
        playerCurMP = playerMPMax;
    }
    public void InitializedWithClothes()  //四属性及血蓝量的初始化
    {

        int changeHP = 0;
        int changeMP = 0;
        int changeAgile = 0;
        int changePatience = 0;
        for (int i = 0; i < 4; ++i)
        {
            initalAtts[i] = 100;
        }
        for (int i = 0; i < 4; ++i)
        {
            if (nowWear[i] != 0)
            {
                changeHP += CLOTHMAP[i, nowWear[i] - 1]["HP"];
                changeMP += CLOTHMAP[i, nowWear[i] - 1]["MP"];
                changeAgile += CLOTHMAP[i, nowWear[i] - 1]["Agile"];
                changePatience += CLOTHMAP[i, nowWear[i] - 1]["Patience"];
            }
        }
        atts[0] = initalAtts[0] + changeHP;
        atts[1] = initalAtts[1] + changeMP;
        atts[2] = initalAtts[2] + changeAgile;
        atts[3] = initalAtts[3] + changePatience;

        playerHPMax = atts[0];
        playerCurHP = playerHPMax;
        playerMPMax = atts[1];
        playerCurMP = playerMPMax;
    }
    void Start()
    {

    }
    void StartUnit(string name)  //选择游戏场景并进行初始化
    {
        //初始化该场景
    }

    public void reloadScene(string name)
    {
        // roomNumber += 1;
        // tileStyle = (tileStyle + 1) % 2;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyGenerator>().NumOfSmallEnemies = 0;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameStart");
    }

    void FinishUint(bool win)  //一个关卡结束
    {
        //结束操作，根据是否胜利切换场景
    }

    public static JourneyManager getInstance()  //调用函数，通过此函数访问JourneyManager,获得其属性或使用其方法
    {
        return instance;  //返回当前静态实例
    }

    public void ChangePlayerHP(int add)   //人物血量改变时调用此函数改变血量
    {
        playerCurHP += add;
        if (playerCurHP <= 0)
        {
            playerCurHP = 0;
            gameUIScript.ChangeHP();
            GameOver(false);
            return;
        }
        if (playerCurHP > playerHPMax)
        {
            playerCurHP = playerHPMax;
            gameUIScript.ChangeHP();
            return;
        }
        gameUIScript.ChangeHP();
    }

    public void ChangePlayerHPMax(int add)   //人物最大血量改变时调用此函数改变最大血量
    {
        int result = playerHPMax + add;
        if (result <= 0)
        {
            playerHPMax = 0;
            playerCurHP = 0;
            gameUIScript.ChangeHP();
            GameOver(false);
            return;
        }
        playerHPMax = result;
        if (playerCurHP > playerHPMax) playerCurHP = playerHPMax;
        gameUIScript.ChangeHP();

    }

    public void ChangePlayerMP(int add)   //人物蓝条改变时调用此函数改变蓝条
    {
        playerCurMP += add;
        if (playerCurMP <= 0)
        {
            playerCurMP = 0;
            gameUIScript.ChangeMP();
            return;
        }
        if (playerCurMP > playerMPMax)
        {
            playerCurMP = playerMPMax;
            gameUIScript.ChangeMP();
            return;
        }
        gameUIScript.ChangeMP();
    }

    public void ChangePlayerMPMax(int add)   //人物最大蓝条改变时调用此函数改变最大蓝条
    {
        int result = playerMPMax + add;
        if (result <= 0)
        {
            playerMPMax = 0;
            playerCurMP = 0;
            gameUIScript.ChangeMP();
            return;
        }
        playerMPMax = result;
        if (playerCurMP > playerMPMax) playerCurMP = playerMPMax;
        gameUIScript.ChangeMP();

    }

    public void ChangeMoney(int add)   //人物金钱改变时调用此函数改变金钱
    {
        money += add;
        gameUIScript.ChangeMoney();
    }


    public void ChangeBoxNum(int add)   //剩余宝箱数改变时调用此函数
    {
        boxNum += add;
        if (boxNum < 0) boxNum = 0;
        gameUIScript.ChangeBoxNum();
    }

    public void ChangeAtts(int i, int add)  //人物四属性改变时调用（最大蓝量和最大红量改变时也调用这个，不是上面那个，注意！！）
    {
        if (i < 0 || i >= 4) return;
        int result = atts[i] + add;
        if (result < 0)
        {
            result = 0;
        }
        atts[i] = result;
        if (i == 0)
        {
            ChangePlayerHPMax(add);
        }
        if (i == 1)
        {
            ChangePlayerMPMax(add);
        }
        gameUIScript.ChangeAttibute(i);
    }

    public void ChangeItems(int i, int add)  //四道具改变时调用
    {
        if (i < 0 || i >= 4) return;
        int result = items[i] + add;
        if (result < 0) return;
        items[i] = result;
        gameUIScript.ChangeItem(i);
    }

    public void UseItem(int i)  //使用四道具时调用，产生效果
    {
        int result = items[i] - 1;
        if (result < 0) return;
        switch (i)
        {
            case 0:
            {
              
                //使用回血道具
                if(playerCurHP==playerHPMax) return;
                ChangePlayerHP(ITEMPOWER[0]);
                    playerController.addHp();
                ChangeItems(0,-1);
                break;
            }
             case 1:
            {
                //使用回蓝道具
                if(playerCurMP==playerMPMax) return;
                ChangePlayerMP(ITEMPOWER[1]);
                    playerController.addMp();
                ChangeItems(1,-1);
                break;
            }
             case 2:
            {
                //使用增敏道具
                ChangeAtts(2,ITEMPOWER[2]);
                    playerController.addPower();
                ChangeItems(2,-1);
                break;
            }
             case 3:
            {
                //使用增耐道具
                ChangeAtts(3,ITEMPOWER[3]);
                    playerController.addPatience();
                ChangeItems(3,-1);
                break;
            }
            default:
            {
                break;
            }
        }
    }

    public void ChangeClothes(int i, int j)  //增加装备【只能增加】 j转换成1到4 //i对应着类别0-3，j对应编号0-3
    {
        clothes[i, j] = true;
        gameUIScript.ChangeDrop(i, j + 1);
    }

    public void ChangeClothes(int i)
    {
        for (int j = 0; j < 3; j++)
        {
            if (!clothes[i, j])
            {
                ChangeClothes(i, j);
                return;
            }
        }
    }

    public void OpenBox(string txt)
    {
        gameUIScript.ChangeOpenBox(txt);
    }

    public void UnitOverToNextLevel()  //从一个关卡进入下一个关卡调用
    {
        GameObject root = GameObject.Find("UnitCanvas(Clone)");
        GameObject uw = root.transform.Find("UnitWin").gameObject;
        uw.SetActive(true);
        Time.timeScale = 0.0f;

    }
    public void GameOver(bool win)   //一个回合结束调用
    {
        if (win)
        {
            isWin = 1;
            winNum++;
            GameObject root = GameObject.Find("UnitCanvas(Clone)");
            GameObject uw = root.transform.Find("FianlWin").gameObject;
            uw.SetActive(true);
            Time.timeScale = 0.0f;
            //nextJourney();
        }
        else
        {
            isWin = 0;
            GameObject root = GameObject.Find("UnitCanvas(Clone)");
            GameObject uw = root.transform.Find("UnitLose").gameObject;
            uw.SetActive(true);
            Time.timeScale = 0.0f;
            this.playNum = 1;
            // nextJourney();
        }
    }

    public void StartJourney()  //从角色选择界面进入关卡界面调用
    {
        SceneManager.LoadScene("GameStart");
        SceneManager.sceneLoaded += CallBack1;
    }

    public void CallBack1(Scene scene, LoadSceneMode sceneType)
    {
        Debug.Log(scene.name + " is load complete!");
        //读取playerInfo选择生成人物
        //生成地图
        //初始化生成敌人

    }

    private void initWincase()
    {
        //winCase = 3;
        winCase = Random.Range(0, 3);
        // 根据wincase设置敌人生成
        switch (winCase)
        {
            case 0:
            {
                roomNumber = Random.Range(2, 5);
                boxNum = 5;
                // enemyGenerator.SetGameMode(GameMode.BeatAll);
                break;
            }
            case 1:
            {
                roomNumber = 7;
                boxNum = 12;
                // enemyGenerator.SetGameMode(GameMode.TreasureAll);
                break;
            }
            case 2:
            {
                roomNumber = 1;
                boxNum = 3;
                // enemyGenerator.SetGameMode(GameMode.Survival);
                break;
            }
            case 3:
            {
                roomNumber = 1;
                break;
            }
        }
    }

    private void setWincase()
    {
        winCase = Random.Range(0, 3);
        // 根据wincase设置敌人生成
        switch (winCase)
        {
            case 0:
            {
                //roomNumber = 1;
                roomNumber = Random.Range(2, 5);
                boxNum = 5;
                enemyGenerator.SetGameMode(GameMode.BeatAll);
                break;
            }
            case 1:
            {
                //roomNumber = 1;
                roomNumber = 7;
                boxNum = 12;
                enemyGenerator.SetGameMode(GameMode.TreasureAll);
                break;
            }
            case 2:
            {
                roomNumber = 1;
                boxNum = 3;
                enemyGenerator.SetGameMode(GameMode.Survival);
                break;
            }
        }
    }

    private void setWincase(GameMode gameMode)
    {
        switch (gameMode)
        {
            case GameMode.BeatAll:
            {
                roomNumber = Random.Range(2, 5);
                //roomNumber = 1;
                boxNum = 5;
                enemyGenerator.SetGameMode(GameMode.BeatAll);
                break;
            }
            case GameMode.TreasureAll:
            {
                roomNumber = 7;
                //roomNumber = 1;
                boxNum = 12;
                enemyGenerator.SetGameMode(GameMode.TreasureAll);
                break;
            }
            case GameMode.Survival:
            {
                roomNumber = 1;
                boxNum = 3;
                enemyGenerator.SetGameMode(GameMode.Survival);
                break;
            }
            case GameMode.Boss:
            {
                roomNumber = 1;
                boxNum = 5;
                enemyGenerator.SetGameMode(GameMode.Boss);
                break;
            }
        }
    }

    public void nextLevel()  //进入下一关
    {
        //关卡变量重置
        enemyGenerator = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<EnemyGenerator>();
        // roomNumber+=1;
        // tileStyle = (tileStyle + 1) % 2;
        unitTime = 0;
        //关卡变量重置
        unitNum++;//关卡数+1
        Debug.Log("unitNum: " + unitNum);

        if (unitNum == LEVEL_PER_JOURNEY + 1)
        {
            Debug.Log("Passssssssssss");
            GameOver(true);
            return;
        }

        if (unitNum != LEVEL_PER_JOURNEY)
        {
            setWincase();
        }
        else
        {
            winCase = 3;
            setWincase(GameMode.Boss);
        }
        reloadScene("GameStart");
    }

    public void nextJourney()  //进入下一回合
    {
        //关卡变量部分重置
        // roomNumber+=1;
        tileStyle = (tileStyle + 1) % 2;
        unitTime = 0;
        if (unitNum != LEVEL_PER_JOURNEY)
        {
            initWincase();
        }
        //回合变量部分重置
        ResetJourneyManager();
        SceneManager.LoadScene("GameOver");
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void Update()
    {

    }


}
