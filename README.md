# The Last Templar

## 游戏简介
...
## 游戏背景
宇航员 外星生物
## 游戏类型
* 2D 
* Rougelike
## 适用平台
* Windows
* MacOS

## 游戏玩法
1. 选择角色
2. WASD控制角色移动，右键攻击，QE特殊技能
3. 每个Journey包含9个随机关卡
4. 每个随机关卡需完成随机任务
5. 道具能增加玩家HP,MP等

## 操作说明
| Keyboard/Mouse | Game |
| -------- | ------- |
|   <kbd>↑</kbd>/<kbd>W</kbd> | 上    
|   <kbd>↓</kbd>/<kbd>S</kbd> | 下    
|   <kbd>←</kbd>/<kbd>A</kbd> | 左    
|   <kbd>→</kbd>/<kbd>D</kbd> | 右   
| 鼠标右键| 攻击
|   <kbd>Q</kbd>     | 技能1    
|   <kbd>E</kbd>     | 技能2     
|   <kbd>SPACE</kbd>     | 闪避 


Player1太空武士：

|Attack| Effect|
|---|---|
|普通攻击 |一刀扣去敌人200点血量
|技能Q|攻击2秒内攻击力倍增
|技能E|1秒内速度倍增

Player2太空射手:

|Attack| Effect|
|---|---|
|普通攻击 |向面前发射普通子弹
|技能Q|向面前发射加大子弹，可以直接打死一个小怪
|技能E|向面前110&deg;方向内发射12颗子弹

Player3太空炸弹人：

|Attack| Effect|
|---|---|
|普通攻击 |范围为1的十字形炸弹 有1.5秒的冷却时间
|技能Q|增加攻击范围为3
|技能E|2秒内消除冷却时间限制


## 功能实现

- [x] 支持背包系统（玩家可切换装备）
- [x] 支持道具系统（回血道具、回蓝道具、加速道具、加防道具）
- [x] 支持四种随机关卡模式（击败所有敌人、幸存模式、宝箱模式、Boss战）
- [x] 支持三种人物选择(近战, 射击, 炸弹)
- [x] 支持三种敌人（Flying Monster, Green Monster, Boss）
- [x] 支持敌人自动追踪玩家
- [x] 支持四种敌人生成机制（根据随机关卡而改变）

## 开发环境

* Unity 2019.4.22

## 游戏设计



### 玩家设计

**Player1近战砍刀**

![](docpic/player1.png)

|攻击| 效果|
|---|---|
|普通攻击 |一刀扣去敌人200点血量
|技能Q|攻击2秒内攻击力倍增
|技能E|1秒内速度倍增

|属性|设置|
|---|---|
|生命值|初始值500|
|技能值|初始值200，技能Q消耗3点蓝，技能E消耗5点蓝|
|速度|初始值10

**Player2远程射击**

![](docpic/player2.png)

|攻击| 效果|
|---|---|
|普通攻击 |发射小子弹，攻击力为100
|技能Q|发射大子弹，怪物直接死亡
|技能E|发射多方向子弹

|属性|设置|
|---|---|
|生命值|初始值300|
|技能值|初始值250，技能Q消耗3点蓝，技能E消耗5点蓝|
|速度|初始值10

**Player3炸弹人**

![](docpic/player3.png)

|攻击| 效果|
|---|---|
|普通攻击 |范围为1的十字形无差别攻击炸弹，攻击力为50，有1.5秒的冷却时间
|技能Q|增加攻击范围为3
|技能E|2秒内消除冷却时间限制

|属性|设置|
|---|---|
|生命值|初始值400|
|技能值|初始值150，技能Q消耗5点蓝，技能E消耗3点蓝|
|速度|初始值10

### 敌人设计

#### 敌人种类

游戏共设计3种敌人，分别为Flying Monster, Green Monster, Boss，具体设计如下。每种敌人都会随着通关回合数的增加而逐渐变强，从而提高后续闯关难度。

敌人被击败后，将随机掉落金币或道具。其中金币掉落的概率为70%，道具掉落的概率为30%，各道具掉落概率相同。

1. Flying Monster

   | 属性     | 详细数据                        |
   | -------- | ------------------------------- |
   | 血量     | 500（通关回合数+1则血量提高50） |
   | 攻击力   | 50（通关回合数+1则攻击提高5）   |
   | 防御力   | 20（通关回合数+1则防御提高3）   |
   | 攻击范围 | 40.0f                           |

2. Green Monster

   | 属性     | 详细数据                        |
   | -------- | ------------------------------- |
   | 血量     | 600（通关回合数+1则血量提高60） |
   | 攻击力   | 40（通关回合数+1则攻击提高7）   |
   | 防御力   | 30（通关回合数+1则防御提高3）   |
   | 攻击范围 | 50.0f                           |

3. Boss 

   | 属性     | 详细数据                          |
   | -------- | --------------------------------- |
   | 血量     | 5000（通关回合数+1则血量提高500） |
   | 攻击力   | 100（通关回合数+1则攻击提高10）   |
   | 防御力   | 35（通关回合数+1则血量提高10）    |
   | 攻击范围 | 150.0f                            |

#### 敌人AI



#### 敌人生成机制

游戏中每一关卡的敌人生成机制由当前关卡随机的**胜利条件**决定，不同的胜利条件对应于不同的敌人生成机制。

1. 胜利条件：击败所有敌人

   地图中将以**5s**为间隔陆续生成**10**个敌人，玩家需要将所有敌人全部击败才能获胜。

2. 胜利条件：找到所有宝箱

   地图中将以**5s**为间隔陆续生成敌人，同时存在的敌人上限为**15**，玩家需要击败敌人的同时找到所有宝箱才能获胜。

3. 胜利条件：存活30s

   地图中将以**2s**为间隔陆续生成敌人，同时存在的敌人上限为**100**，此机制中生成的敌人将**不存在待机模式**，所有敌人将始终追赶玩家。

4. 胜利条件：Boss战

   地图中将生成一个Boss敌人，玩家需要击败Boss才能获胜。

### 地图设计

贴图

### 关卡设计


### 道具设计
|道具类型|UI|效果|数量|备注|
|---|---|---|---|---|

### 装备设计

|装备类型|UI|种类数|效果|备注|
|---|---|---|---|---|
|帽子||4| 增加人物耐力值，装备等级越高增加的量越多 |同种类型的装备只能装备一件|
|护甲||4|增加人物最大HP，装备等级越高增加的量越多|同种类型的装备只能装备一件|
|鞋子||4|增加人物最大MP，装备等级越高增加的量越多|同种类型的装备只能装备一件|
|饰品||4|增加人物攻击值，装备等级越高增加的量越多|同种类型的装备只能装备一件|


### 逻辑设计

流程图

### UI设计

贴图


## 项目文件结构

## 游戏开发



## 小组成员分工

| 学号    | 姓名     | 分工                                       |
| ------- | -------- | ------------------------------------------ |
| 1751551 | 黄颖    |玩家1&玩家3动画制作、动画自动机设计、攻击实现|
| 1753307 | 蔡方俊妍 | 所有UI制作、场景制作、场景切换逻辑设计、总体架构设计 |
| 1850250 | 赵浠明   | 敌人动画制作、敌人AI实现、敌人生成机制实现 |
| 1852137 | 张艺腾   |                                            |
| 1853829 | 杨雨辰   |                                            |

