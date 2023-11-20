# Tank Boss Fight

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: ZhihaoChen
-   Section: _##_

## Simulation Design

1v1 Tank fight

You will need to drive your tank to fight with an AI tank.

### Controls

W,A,S,D/arrow key to control the movement
Have to notice that the tank can only 原地转向

玩家有生命值，需要在生命值耗尽之前击毁ai敌人

-   _List all of the actions the player can have in your simulation_
    -   _Include how to preform each action ( keyboard, mouse, UI Input )_
    -   _Include what impact an action has in the simulation ( if is could be unclear )_
    -   
## Agent 1: 敌人Boss

### Description
这就是你需要击毁的目标，他的火力会比你强大，不但会发射主炮还会发射反坦克导弹。他会在地图上寻找玩家的位置并向玩家的位置行驶，他的火炮只有炮弹可以击中玩家他才会开火但是导弹会从炮塔上的导弹发射架一直发射。


### State 1: 寻找玩家

#### Objective
当敌人主炮无法攻击到玩家的时候，他会通过直接获得玩家的位置并且向玩家靠拢，虽然不会朝玩家开炮但是会发射导弹一次两发然后冷却一段时间

#### Steering Behaviors
1：避开墙壁： 防止撞墙。
2；警戒寻找玩家：朝玩家所在的位置行驶
3；锁定玩家： 保持炮塔将持续看向玩家但是不会攻击玩家

#### Obstacles
1： 墙壁 他无法穿越墙壁
2： 玩家 两台坦克无法重叠（但是不包括炮管）

#### State Transitions

会从以下状态切换到本状态
-  攻击模式->警戒模式 （条件：主炮无法攻击到玩家也就是当玩家驶躲在掩体的时候）停止开火向玩家的位置前进，并且一次发射两发导弹

  会从本状态切换到以下状态
  警戒模式->攻击模式 （条件：主炮可以攻击到玩家也就是当玩家驶出掩体的时候）停止追踪玩家，转换到只原地转向，使用主炮向玩家开火并且切换到快速发射4发导弹的模式
 警戒模式->狂暴模式 （条件血量低于20%） 持续追踪并且驶向玩家 装填速度加快，导弹将进行6发骑射

### State 2: 攻击模式

#### Objective
当玩家不在掩体后面时，就会从警戒模式切换到攻击模式，这个时候他会尽力把正对玩家让他装甲最厚的方向面对玩家（本游戏没有装甲机制）。也就是说在看到玩家之后就只会原地转向。  
并且主炮开火而且导弹也会有些差别他会从警戒模式的发射2发导弹到攻击模式快速发射4发导弹


#### Steering Behaviors
- 原地旋转 保持车头指向玩家
- 锁定玩家： 保持炮塔将持续看向玩家，并且火炮会攻击玩家

#### Obstacles
和上面一样
-1： 墙壁 他无法穿越墙壁
2： 玩家 两台坦克无法重叠（但是不包括炮管）

#### State Transitions.
会从以下状态切换到本状态
警戒模式->攻击模式 （条件：主炮可以攻击到玩家也就是当玩家驶出掩体的时候）停止追踪玩家，转换到只原地转向，使用主炮向玩家开火并且切换到快速发射4发导弹的模式

会从本状态切换到以下状态
 攻击模式->警戒模式 （条件：主炮无法攻击到玩家也就是当玩家驶躲在掩体的时候）停止开火向玩家的位置前进，并且一次发射两发导弹
 攻击模式->狂暴模式 （条件血量低于20%） 持续追踪并且驶向玩家 装填速度加快，导弹将进行6发骑射


## Agent 2: 导弹

### Description
这是敌人发射的导弹，会跟踪你所在的位置并且一定程度上有躲避障碍物的能力，会在碰到任何物体或者是

### State 1: Hunting

#### Objective
The primary goal of this state is for the invading fish to hunt and catch the player's fish.

#### Steering Behaviors
- **Chase:** Aggressively pursue the closest fish within the farm(it moves really fast!).
- **Capture:** Execute a catch maneuver when close enough to a target fish.

#### Obstacles
- **Terrain Avoidance:** Like the player's fish, the invading fish cannot navigate over the fish farm's terrain and must go around any environmental obstacles.

#### Separation
- **From Nets:** Evade nets thrown by the player, which are used to catch or deter the invading fish, but the net wont appear unleas player click the right click.

#### State Transitions
- **Shock Avoidance:** If a net thrown by the player's right-click is nearby, the invading fish will temporarily stop hunting and attempt to escape, showing a shock reaction.
- **Resume Hunting:** After the shock from the net has worn off, if no other immediate threat is present, the invading fish will resume its hunting behavior.

### State 2: Fleeing

#### Objective
When threatened by the player's right click, the invading fish will attempt to flee the area to avoid capture or harm.

#### Steering Behaviors
- **Escape:** If the invading fish perceives a net close to it, it will prioritize escaping from the area.

#### Obstacles
- **Same as Hunting State:** The invading fish must continue to navigate around the same environmental obstacles even while fleeing.

#### Separation
- **From Threats:** Maintain a safe distance from the player's attempts to capture or stun it. It wont run away from your mouse, it will only reach when you right click to throw a net.

#### State Transitions
- **Threat Detected:** If the player attempts to catch the invading fish and fails, the fish will enter this state to escape.
- **Safe Zone Reached:** Once the invading fish feels it has escaped the threat, it may transition back to the hunting state if it is far enough away from the player.

  
## Make it Your Own
  
Plan to make all the asset by myself

- _List out what you added to your game to make it different for you_
- _If you will add more agents or states make sure to list here and add it to the documention above_
- _If you will add your own assets make sure to list it here and add it to the Sources section


## Known Issues

-it may be a bit diffcult to keep the fish together from escaping from the invading fish

### Requirements not completed

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_



