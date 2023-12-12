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
- **Turret Rotation & Fire**: move you mouse and keep aim at the enemy, you tank will fire automatically

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
- **Walls**: Hidden objects that have the tag obstacles.


#### Separation
- **Important**: The enemy dont have separation, but since there is only one enemy I think it is reasonable. So to compensate I added separation to the other two agents,  
the missile have a really weak separation force but the hp box have a strong one




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
- **Object will wall tag**: Missile have a unqiue obstacles advoid method. I found that let the missile fly upward makes people feel more like this is a missile

#### Separation
- The missile will have a light separation force from other missile.


## HpBox

### Description
Add 5 hp to the player when player reach the hp box on the map

### States

#### State 1: wander
- **Objective**: just move around
- **Behaviors**: just move around

#### Steering Behaviors
- Wander just move around it's spwan position
- STAY in bound: Stay in the bound when aproching the bound of the map

#### Obstacles
- **Walls**:Hidden objects that have the tag obstacles.

#### Separation
- The hp box will have a stong sepration force will other hp box.



  
## Make it Your Own
  
3D: I made all the 3D asset in the game. From the factory to the missile and shells. All 3d objects have textures but can't be uploaded due to build size.
Particle and material of smoke and explosion were downloaded  
Link:https://github.com/Tvtig/RocketLauncher/tree/main/Assets/Tvtig/Rocket%20Launcher/Art/Textures




## Known Issues

-PLayer will be able to move out the map 


