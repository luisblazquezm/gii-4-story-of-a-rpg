## Introduction

This game in 2D is based on a RPG game where the player must find 3 objects in 3 different scenarios in order to get to a final boss and defeat it. The tools and techniques shown here are used on base of what it has been learnt in class.

## Preview

<Insert preview image of the game here>

## Main Scheme

* **Player**: basic sprite image player. This is the sprite by default.
![Alt Text](https://github.com/luisblazquezm/story-of-a-rpg/tree/master/docs/Player.JPG)

* **Scenarios**: there will be four different scenarios:

    1. Forest
    2. Hell
    
    3. Future: Add sprites for Desert and Subaquatic zone (Atlantis)

* **Enemies**: there will be 2 or 3 in each scenario:

    1. Plain field
       * **enemy_1**: (Log): A slow and not very strong enemy. Basic type of enemy.
       * **enemy_2**: (Goblin): Very strong and pretty fast. Although keeps being very basic.
    2. Hell
       * **enemy_1**: (Skeleton): Very fast enemy. Normal strength.
       * **Final Boos**: (The Demon): Very strong and fast enemy. Two hits and the player will be over.

* **Tools and weapons**: each tool will give the player different habilites
   
   * **ligthsaber**: a jedi´s most powerful weapon.
   * **anduril**: the sword of Aragorn from The Lord of the Rings Saga.
   * **mjolnir**: Thor´s famous hammer. Gives the player the power to throw it back and forth to the enemies.
   
* **Stats**:

| Weapon | Attack | Agility | Special Power |
| --- | --- | --- | --- |
| `ligthsaber` | 20 (+5) | (+10) | (+15) | You can use the force to knockback your enemies |
| `anduril` | 20 (+5) | (+10) | (+15) | Use a bow and arrows |
| `mjolnir` | 20 (+5) | (+10) | (+15) | Throws back and forth Thor´s hammer simulating the effect of a boomerang|
