# Battle-City
## Author
Name: Zero
## Introduction
<https://en.wikipedia.org/wiki/Battle_City>
## Todo(Break Down)

- Audio
  - [x] Start Music
  - [x] Original sound pack
  - [ ] ~~custom sound pack~~
- Art/Sprites
  - Tanks
    - [x] Player A tank
    - [ ] Player B tank
    - [x] Basic Tank
    - [x] Fast Tank
    - [x] Power Tank
    - [ ] Armor Tank
  - Power-Ups
    - [x] Grenade
    - [x] Helmet
    - [x] Shovel
    - [x] Star
    - [x] Tank
    - [x] Timer
  - Environment
    - [x] Brick Wall
    - [x] Steel Wall
    - [x] Trees
    - [x] Water
    - [x] Ice
    - [x] Base(phoenix)
  - UI
    - [x] Enemy indicator
    - [x] Player indicator
    - [ ] ...
- Mechanism
  - System
    - [x] Player Music When game start
    - [ ] Game win condition - Player wins when all 20 enemey are eliminated
    - [ ] Game lose condition - Player lose if no life left or Base is destroyed
    - [ ] Level select Menu
    - [ ] Level Create
    - [ ] Game pause/Unpause
    - [ ] Next level
    - [ ] Score - Record the Score
    - [ ] Storage - Record the highest Score
  - Game Obejcts
    - Tank
      - [x] Animation when moving
      - [x] Audio when moving
      - [x] can move upward ⬆️
      - [x] can move downward ⬇️
      - [x] can move left ⬅️
      - [x] can move right ➡️
      - [x] can only move one direction each time(not allowed diagnal)
      - [x] can fire bullet 🔫
      - [ ] can pick up Power-Ups
      - [x] can level up (increase Tier)
        - [x] **Tier 1**: normal speed. only has one bullet
        - [x] **Tier 2**: faster speed
        - [x] **Tier 3**: has two bullets
        - [ ] **Tier 4**: more powerful bullet. double efficiency when destroy Brick Walls. Can Destroy Steel Walls.
      - [x] can flash red
        - [ ] Aniamtion
        - [ ] release a random power-up if the tank is flashing red
      - [x] Health Points system
        - [ ] Be destroyed when HP reach 0
          - [x] Audio
          - [ ] Aniamtion
      - [ ] lives system
      - Player
        - [x] birth with **Tier 1**
        - [x] 100 HP
        - [ ] birth with shield
        - [x] birth at bottom of map(fixed location)
        - [x] Use keyboard Control the tank
        - [x] Press 'W' to go up
        - [x] Press 'S' to go down
        - [x] Press 'A' to go left
        - [x] Press 'D' to go left
        - [x] Press 'J' to fire
      - Enemy
        - [ ] Birth at Top of map(3 random locations)
        - [ ] Path finding. Can find a path to the base.
        - [x] fire bullets continuously
        - [ ] choose a random direction to move
        - [ ] randomly assign flashing  
        - [ ] Base Tank
          - [x] birth with **Tier 1**
          - [ ] 100 HP
          - [ ] Slow Movement Speed
          - [ ] Slow Bullet Speed
        - [ ] Faster Tank
          - [x] ~~birth with **Tier 2**~~ birth with **Tier 1**
          - [ ] 100 HP
          - [ ] fast Movement Speed
          - [ ] normal Bullet Speed
        - [ ] Power Tank
          - [ ] ~~birth with **Tier 3**~~ birth with **Tier 1**
          - [ ] 100 HP
          - [ ] normal Movement Speed
          - [ ] fast Bullet Speed
          - [ ] double efficiency when destroy Brick Walls.
        - [ ] Armor Tank
          - [ ] birth with **Tier 3**
          - [ ] 400 HP
          - [ ] normal Movement Speed
          - [ ] normal Bullet Speed
          - [ ] double efficiency when destroy Brick Walls
    - Bullets
      - [x] Keep moving forward
      - [ ] Be destoryed when collide with any obstacles
      - [ ] Can destory Brick Walls
      - [ ] Can destory Steel Walls
      - [x] can level up (increase Tier)
      - Player Bullet
        - [ ] Can stop alliance if collide with alliance
        - [ ] reduce enemy 100 HP
      - Enemy Bullet
        - [ ] Can stop alliance if collide with alliance
        - [ ] reduce Player 100 HP
    - Power-Ups
      - [ ] can be picked up by Player
      - [ ] can be picked up by Enemy
      - [ ] **Grenade**: Destory all enemies in the scene
      - [ ] **Helmet**: generate a shild on the tank
      - [ ] **Shovel**: repalce the Wall around the base to Steel Walls
      - [ ] **Star**: increase one Tier of the tank
      - [ ] **Tank**: increase one life of the tank
      - [ ] **Timer**: pause all enemies for 10 seconds(The World)
    - Environment
      - [x] The map size is 13 unit * 13 unit = 169 grids
      - [ ] Each Grid has only one Environment (None/Brick/Steel/Tree/Water/Ice)
      - [ ] Each Grid has its own generation system. Generate Environment randomly
      - Brick Wall - Basic walls. Can be destoryed by any bullets
        - [x] Tanks cannot go through
        - [x] divied into 4*4 parts
        - [x] Normal bullets can destory 1*4 parts
        - [x] Powerful bullets can destory 2*4 parts
      - Steel Wall - soild walls. Can only Be destroyed by powerful bullets
        - [x] Tanks cannot go through
        - [x] divied into 2*2 parts
        - [x] Normal bullets cannot destory
        - [x] Powerful bullets can destory 1*2 parts
      - Trees - Can hide the tanks
        - [x] Tanks can go through
        - [x] Bullets can go through
        - [x] Tanks can hardly be seen under the trees
        - [x] Bullets can hardly be seen under the trees
        - [x] Cannot be Destoryed
      - Ice - Cannot control the tanks on the Ice
        - [x] Tanks can go through
        - [x] Bullets can go through
        - [ ] Tanks cannot be controled on the ice
        - [ ] Tanks cannot stop on the ice
        - [x] Cannot be Destoryed
      - Water - Tanks cannot swim
        - [x] Tanks **cannot** go through
        - [x] Bullets **can** go through
        - [x] Cannot be Destoryed
