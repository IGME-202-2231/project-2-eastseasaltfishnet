# Fish Farm Master

[Markdown Cheatsheet](https://github.com/adam-p/markdown-here/wiki/Markdown-Here-Cheatsheet)

_REPLACE OR REMOVE EVERYTING BETWEEN "\_"_

### Student Info

-   Name: ZhihaoChen
-   Section: _##_

## Simulation Design

Fish Farm Simulator

The player operates a fish farm, where the player needs to raise as many fish as possible within a time limit,   
while guarding against invasive species that appear on the farm.

### Controls

Left click to feed the fish.
Right click to catch a invasive fish.

-   _List all of the actions the player can have in your simulation_
    -   _Include how to preform each action ( keyboard, mouse, UI Input )_
    -   _Include what impact an action has in the simulation ( if is could be unclear )_
    -   
## Agent 1: Fish

### Description
This agent actively seeks out feed placed within the environment and attempts to avoid predatory invasive fish. If it consumes two servings of feed within a five-second window, it reproduces, adding another fish to the simulation. However, it is also prey for the invasive fish, making its survival a balancing act between feeding, breeding, and evading predators.

### State 1: Forage and Breed

#### Objective
The primary focus of this state is for the fish to locate and consume food while also seeking opportunities to breed. It must balance its need to feed with the risk of predation.

#### Steering Behaviors
- **Separation:** Maintain personal space within the school to prevent crowding.
- **Flee:** Evade invasive fish when they come within a defined danger radius.

#### Obstacles
- **Terrain:** Natural features within the fish farm that the fish must navigate around.
- **Future Obstacles:** Additional obstacle types are planned to be added to increase complexity, it may be underwater plants or artificial structures.

#### Separation
- **From Invasive Fish:** Maintains distance from predatory invasive fish, switching to flee behavior when necessary.

#### State Transitions
- **Normal Cruising:** When there is no immediate food or threat, the fish will swim at a steady pace with smooth turns and consistent acceleration.
- **Feeding:** Prioritize moving towards food when it becomes available.
- **Evading Predators:** When a predator is spotted, group together and prioritize escaping the threat over feeding.


### State 2: Escape

#### Objective
The singular focus of this state is to avoid becoming prey. The fish engages in behaviors designed to maximize the distance from predators as quickly as possible.

#### Steering Behaviors
- **Flee:** Take the most direct path away from the nearest invasive fish.
- **Schooling:** Align with the movement of nearby fish to form a group.


#### Obstacles
- **Same as Foraging State:** Even while fleeing, the fish must still navigate around the natural terrain features within the fish farm.

#### Separation
- **From Predators:** The fish will increase its separation from predators, using speed and agility to escape( but usally it wont work, the invading fish is much faster).

#### State Transitions
- **Predator Within Danger Radius:** The fish instantly transitions to this state when a predatory fish enters a defined proximity.
- **Predator No Longer a Threat:** Once the fish has successfully evaded the predator, or the predator leaves the danger radius, the fish can transition back to the foraging or breeding states depending on its energy needs and environmental conditions.


## Agent 2: invading Fish

### Description
The invading fish spawn randomly at positions outside of the player's camera view but close to the fish farm. Their objective is to invade the fish farm and catch the player's fish, introducing an element of risk and challenge in managing the farm.

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



