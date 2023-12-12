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
- **Movement**: Use `W`, `A`, `S`, `D`, or the arrow keys for movement.
- **Turning**: The tank can only pivot in place.
- **Turret Rotation & Fire**: move your mouse and keep aim at the enemy, your tank will fire automatically

## Player
The player must maintain a hitpoint while attempting to destroy the AI enemy.


## AI Tank (Enemy Boss)

### Description
This is the boss an AI tank with stronger firepower, capable of shooting main cannon rounds and anti-tank missiles.

### States

#### State 1: Searching Player
- **Objective**: Seek out the player's position and approach.
- **Behaviors**:
  - Seek player: Turning and then moving toward the player after facing the player
  - Lock on the player: Turret continuously faces the player. It will automatically fire when it finishes reloading
- **Transitions**:
  - To avoid Obstacles mode when there is a wall ahead (when the avoid force returned is strong enough)

#### State 2: avoid Obstacles Mode
- **Objective**: Find the best way to go around obstacles (There are hidden mesh in the map to represent the obstacles)
- **Behaviors**:
  - The enemy will look for the best way around the obstacle and will continue to track the player after it has gone around the obstacle.
- **Transitions**:
  - To search for Player when obstacles have been circumvented or the current position is beyond the edge of the map.
 
  #### Steering Behaviors
1. Avoid Walls:  ****IMPORTANT* the forece return from the method AvoidObstacle in agent only helps in determining whether do enemy needs to turn or not. It will turn the enemy in the best direction and apply a force toward the front. To match the way the tanks move***：  
2. Search Player: A force will be applied toward the front of the tank (this fits the way a tank moves)

#### Obstacles
- **Walls**: Hidden objects that have the tag obstacles. The way of advoid obstacles mehtod work on the tank is the method will turn the body of the tank toward the best direction to get around the obstacles. but the force will only apply foward on the hull of tha tnak


#### Separation
- **Important**: The enemy doesn't have separation, but since there is only one enemy I think it is reasonable. So to compensate I added separation to the other two agents,  
the missile has a really weak separation force but the hp box has a strong one




## Missile

### Description
Missiles launched by the AI tank will track the player's position and can avoid obstacles to some extent.

### States

#### State 1: Seeking Player
- **Objective**: Acquire and move towards the player's position.
- **Behaviors**:
  - Seek target: Head straight for the player's location.
- **Transitions**:
  - To Avoid walls if an obstacle is detected players will need to be behind the obstacles.

#### State 2: Avoiding Walls
- **Objective**: Avoid walls (any object with a "Wall" tag) by applying an upward force to the missile.
- **Transitions**:
  - Seek the player if no walls are detected after a cooldown.

#### Steering Behaviors
- Seek Target: The missile heads straight for the player's location.
- Avoid Walls: A force directly toward the top will be applied and it will pull the missile up.

#### Obstacles
- **Object will wall tag**: Missile have unique obstacles avoid method. I found that letting the missile fly upward makes people feel more like this is a missile

#### Separation
- The missile will have a light separation force from another missile.


## HpBox

### Description
Add 5 hp to the player when the player reaches the hp box on the map

### States

#### State 1: wander
- **Objective**: just move around
- **Behaviors**: just move around

#### Steering Behaviors
- Wander just moves around its spawn position
- STAY inbound: Stay in the bounds when approaching the bounds of the map

#### Obstacles
- **Walls**:Hidden objects that have the tag obstacles.

#### Separation
- The HP box will have a strong separation force will other HP boxes.



  
## Make it Your Own
  
3D: I made all the 3D asset in the game. From the factory to the missiles and shells. All 3d objects have textures but can't be uploaded due to build size.  
Animation of the drone  
Particles and material of smoke and explosion were downloaded   
Link:https://github.com/Tvtig/RocketLauncher/tree/main/Assets/Tvtig/Rocket%20Launcher/Art/Textures




## Known Issues

-Player will be able to move out the map   
-The HP box will stuck a bit when it is trying to avoid the obstacle   
-The Hp of enemy is not showing correctly. I have to pull it into the scene to make the Ui image function


