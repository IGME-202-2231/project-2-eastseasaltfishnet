# Tank Boss Fight

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

![Factory Image](https://github.com/IGME-202-2231/project-2-eastseasaltfishnet/blob/main/RenderedImage/Factory.png)

![Player Image](https://github.com/IGME-202-2231/project-2-eastseasaltfishnet/blob/main/RenderedImage/Player.png)
_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: ZhihaoChen
-   Section: _##_

## Overview
In this simulation, you'll be driving your tank to engage in combat with an AI-controlled tank. Your goal is to destroy the enemy tank before your health runs out.

## Controls
- **Movement**: Use `W`, `A`, `S`, `D` or the arrow keys for movement.
- **Turning**: The tank can only pivot in place.
- **Fire**: Left click

## Player
The player  must maintained hitpoint while attempting to destroy the AI enemy.


## AI Tank (Enemy Boss)

### Description
This is the boss an AI tank with stronger firepower, capable of shooting main cannon rounds and anti-tank missiles.

### States

#### State 1: Searching Player
- **Objective**: Seek out the player's position and approach.
- **Behaviors**:
  - Seek player: Turning and then moving toward the player after facing at the player
  - Lock on player: Turret continuously faces the player. It will automatic fire when it finish reload
- **Transitions**:
  - To: advoid Obstacles mode when there is a wall ahead (when the advoid force returned is strong enough)

#### State 2: advoid Obstacles Mode
- **Objective**: Find the best way to go around a obstacles (There are hidden mesh in the map to represent the obstacles)
- **Behaviors**:
  - The enemy will look for the best way around the obstacle and will continue to track the player after it has gone around the obstacle.
- **Transitions**:
  - To: Searching Player when obstacles have been circumvented or the current position is beyond the edge of the map.
 
  #### Steering Behaviors
1. Avoid Walls:  ****IMPORTANT* the forece return from the method AvoidObstacle in agent only helps in detemine do enemy need to turn or not. Basilcy it will turn the enemy to the best direction and apply a force toward the front. To match the way the tanks move***：  
2. Serach Player: A force will be applied toward the front of the tank (this fits the way a tank move)

#### Obstacles
- **Walls**: The tank cannot pass through walls and must avoid collisions.
- **Player**: Enemy Tanks wont overlap with player, except player ran. Unless the player tries to overlap with it. It will always keep a distace with player

  
## Missile

### Description
Missiles launched by the AI tank will track the player's position and can avoid obstacles to some extent.

### States

#### State 1: Seeking Player
- **Objective**: Acquire and move towards the player's position.
- **Behaviors**:
  - Seek target: Head straight for the player's location.
- **Transitions**:
  - To: Avoiding walls if an obstacle is detected and players will need to be behind the obstacles.

#### State 2: Avoiding Walls
- **Objective**: Avoid walls (any object with "Wall" tag) by applying an upward force to the missile.
- **Transitions**:
  - To: Seeking player if no walls are detected after a cooldown.

#### Steering Behaviors
- Seek Target: The missile heads straight for the player's location.
- Avoid Walls: A force directly toward top will be apply and it will pull the missile up.

#### Obstacles
- **Object will wall tag**: Any object with a tag of Wall will cause the missile change into Avoiding Walls mode.
- **Any Collider**: Colliding with any object with a collider will cause the missile to explode.

  
## Make it Your Own
  
3D: I made all the 3D asset in the game. From the factory to the missile and shells. All 3d objects have textures but can't be uploaded due to build size.
Particle and material of smoke and explosion were downloaded  
Link:https://github.com/Tvtig/RocketLauncher/tree/main/Assets/Tvtig/Rocket%20Launcher/Art/Textures




## Known Issues

-it may be a bit diffcult to keep the fish together from escaping from the invading fish

### Requirements not completed

-   _List all project sources here –models, textures, sound clips, assets, etc._
-   _If an asset is from the Unity store, include a link to the page and the author’s name_


# 坦克大战

## Agent 1: 敌人Boss

### Description
这就是你需要击毁的目标，他的火力会比你强大，不但会发射主炮还会发射反坦克导弹。他会在地图上寻找玩家的位置并向玩家的位置行驶，他的火炮只有炮弹可以击中玩家他才会开火，但是导弹会从炮塔上的导弹发射架一直发射。

### State 1: 寻找玩家

#### Objective
当敌人主炮无法攻击到玩家的时候，他会通过直接获得玩家的位置并且向玩家靠拢，虽然不会朝玩家开炮但是会发射导弹一次两发然后冷却一段时间。

#### Steering Behaviors
1. 避开墙壁：防止撞墙。
2. 警戒寻找玩家：朝玩家所在的位置行驶。
3. 锁定玩家：保持炮塔将持续看向玩家但是不会攻击玩家。

#### Obstacles
1. 墙壁：他无法穿越墙壁。
2. 玩家：两台坦克无法重叠（但是不包括炮管）。

#### State Transitions
- 从攻击模式切换到警戒模式（条件：主炮无法攻击到玩家，即玩家躲在掩体时）。停止开火，向玩家位置前进，并且一次发射两发导弹。
- 从警戒模式切换到攻击模式（条件：主炮可以攻击到玩家，即玩家驶出掩体时）。停止追踪玩家，转换到只原地转向，使用主炮向玩家开火，并且切换到快速发射4发导弹的模式。
- 从警戒模式切换到狂暴模式（条件：血量低于20%）。持续追踪并驶向玩家，装填速度加快，导弹将进行6发齐射。

### State 2: 攻击模式

#### Objective
当玩家不在掩体后面时，敌人将从警戒模式切换到攻击模式。这时，他会尽力让最厚的装甲面对玩家（虽然游戏中没有装甲机制）。看到玩家后，将只进行原地转向。主炮开火，导弹行为也有所变化，从警戒模式的发射两发导弹切换到攻击模式快速发射四发导弹。

#### Steering Behaviors
- 原地旋转：保持车头指向玩家。
- 锁定玩家：炮塔持续对准玩家，主炮攻击。

#### Obstacles
与上面相同。
1. 墙壁：无法穿越墙壁。
2. 玩家：两台坦克无法重叠（但是不包括炮管）。

#### State Transitions
- 从警戒模式切换到攻击模式（条件：主炮可以攻击到玩家，即玩家驶出掩体时）。停止追踪玩家，转换到只原地转向，使用主炮向玩家开火，并且切换到快速发射4发导弹的模式。
- 从攻击模式切换到警戒模式（条件：主炮无法攻击到玩家，即玩家躲在掩体时）。停止开火，向玩家位置前进，并发射两发导弹。
- 从攻击模式切换到狂暴模式（条件：血量低于20%）。持续追踪并驶向玩家，装填速度加快，导弹进行6发齐射。

## Agent 2: 导弹

### Description
这是敌人发射的导弹，会跟踪你所在的位置，并在一定程度上能够避免障碍物。会在碰到任何物体或者是发射后6秒后爆炸。只有命中玩家才会造成伤害。

### State 1: 寻找玩家

#### Objective
获取玩家位置并飞向玩家。

#### Steering Behaviors
- 寻找目标：直接飞向玩家位置。

#### Obstacles
所有有collider的物体都会导致导弹爆炸。

#### State Transitions
- 从躲避墙壁状态切换到寻找玩家状态（条件：躲避墙壁的倒计时结束且没有检测到墙壁时）。导弹朝玩家飞去并在命中后爆炸。
- 从寻找玩家状态切换到躲避墙壁状态（条件：前方有墙壁且玩家不在墙壁前方时）。对导弹施加向上的力来躲避墙壁。

### State 2: 躲避墙壁

#### Objective
此状态会通过向前方发射射线来检测墙壁。如果射线在碰到玩家之前先检测到墙壁（通过比较标签），导弹将开始躲避墙壁。如果玩家在墙壁前面，则不会触发此状态。

#### Obstacles
所有有collider的物体。

#### Steering Behaviors
- 躲避墙壁：对导弹施加向上的力来躲避墙壁。

#### State Transitions
- 从寻找玩家状态切换到躲避墙壁状态（条件：前方有墙壁且玩家不在墙壁前方时）。对导弹施加向上的力来躲避墙壁。
- 从躲避墙壁状态切换到寻找玩家状态（条件：躲避墙壁的倒计时结束且没有检测到墙壁时）。导弹朝玩家飞去并在命中后爆炸。
