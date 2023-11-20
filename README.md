# Tank Boss Fight

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: ZhihaoChen
-   Section: _##_

# 1v1 Tank Fight Simulation

## Overview
In this simulation, you'll be driving your tank to engage in combat with an AI-controlled tank. Your goal is to destroy the enemy tank before your health runs out.

## Controls
- **Movement**: Use `W`, `A`, `S`, `D` or the arrow keys for movement.
- **Turning**: The tank can only pivot in place.
- **Fire**: Left click
- Ps make add aiming system

## Player
The player has a health value that must be maintained while attempting to destroy the AI enemy.


## AI Tank (Enemy Boss)

### Description
This is the boss an AI tank with stronger firepower, capable of shooting main cannon rounds and anti-tank missiles.

### States

#### State 1: Searching for Player
- **Objective**: Seek out the player's position and approach.
- **Behaviors**:
  - Avoid walls to prevent collision.
  - Seek player: Move towards the player's last known position.
  - Lock on player: Turret continuously faces the player but doesn't attack.
- **Transitions**:
  - To: Attacking mode when the main cannon can hit the player.
  - To: Berserk mode when health drops below 20%.

#### State 2: Attack Mode
- **Objective**: Attack the player when they are not behind cover.
- **Behaviors**:
  - Rotate in place to keep the front of the tank towards the player.
- **Transitions**:
  - To: Searching mode if the player goes behind cover.
  - To: Berserk mode when health drops below 20%.
 
  #### Steering Behaviors
1. Avoid Walls: Prevents the tank from colliding with walls.
2. Alert Search for Player: Moves towards the player's last known location.
3. Lock on Player: The turret continuously aims at the player but does not fire.

#### Obstacles
- **Walls**: The tank cannot pass through walls and must avoid collisions.
- **Player**: Tanks cannot overlap with each other, ensuring physical separation during maneuvers.


  
## Missile

### Description
Missiles launched by the AI tank will track the player's position and can avoid obstacles to some extent.

### States

#### State 1: Seeking Player
- **Objective**: Acquire and move towards the player's position.
- **Behaviors**:
  - Seek target: Head straight for the player's location.
- **Transitions**:
  - To: Avoiding walls if an obstacle is detected.

#### State 2: Avoiding Walls
- **Objective**: Avoid walls by applying an upward force to the missile.
- **Transitions**:
  - To: Seeking player if no walls are detected after a cooldown.

#### Steering Behaviors
- Seek Target: The missile heads straight for the player's location.
- Avoid Walls: The missile will adjust its path to avoid collisions with walls.

#### Obstacles
- **Any Collider**: Colliding with any object with a collider will cause the missile to explode.

  
## Make it Your Own
  
3D: I made all the 3D asset in the game
Some Particle. 
material of smoke and explosion were downloaded

- _List out what you added to your game to make it different for you_
- _If you will add more agents or states make sure to list here and add it to the documention above_
- _If you will add your own assets make sure to list it here and add it to the Sources section


## Known Issues

-it may be a bit diffcult to keep the fish together from escaping from the invading fish

### Requirements not completed

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_


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
你需要知道的内容：我很想把导弹状态的判断前方有没有墙壁写成一个method但是当我写到方法里并且使用的时候，导弹容易在寻找玩家和躲避墙壁之间快速切换，导致导弹产生奇怪的飞行轨迹。所以我就把他们都放在一起了

### Description
这是敌人发射的导弹，会跟踪你所在的位置并且一定程度上有躲避障碍物的能力，会在碰到任何物体或者是发射后6秒后爆炸。只有命中玩家才会造成伤害

### State 1: 寻找玩家

#### Objective
获取玩家位置并且飞向玩家

#### Steering Behaviors
- seek target 获取玩家所在位置径直飞向玩家

#### Obstacles
所有有coliider的物体都会让导弹爆炸

#### State Transitions
会从以下状态切换到本状态
躲避墙壁->寻找玩家（条件当躲避墙壁的倒计时结束时，并且没有检测到墙壁就会朝玩家飞过去）就是朝玩家飞过去命中后爆炸

会从本状态切换到以下状态
寻找玩家->躲避墙壁（当前方有墙壁并且玩家不在墙壁前方时） 施加一个向上的力躲避墙壁

### State 2: 躲避墙壁

#### Objective
此方法会通过向前方发射射线检测墙壁如果在射线碰到玩家之前先检测到墙壁（通过对比tag）就是开始躲避墙壁，如果玩家在墙壁前面就将不会出发本状态


#### Obstacles
所有有colider的物体

#### Steering Behaviors
- 躲避墙壁  将会对导弹施加一个向上的力

#### State Transitions
会从以下状态切换到本状态
寻找玩家->躲避墙壁（当前方有墙壁并且玩家不在墙壁前方时） 施加一个向上的力躲避墙壁

会从本状态切换到以下状态
躲避墙壁->寻找玩家（条件当躲避墙壁的倒计时结束时，并且没有检测到墙壁就会朝玩家飞过去）就是朝玩家飞过去命中后爆炸
