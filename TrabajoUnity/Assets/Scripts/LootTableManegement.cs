using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable] // Used for the loots to be visible in the interphace
public class Loot
{
    public GameObject lootObject;
    public int lootChance;
}


[CreateAssetMenu]
public class LootTableManegement : ScriptableObject
{
    public Loot[] loots;

    public GameObject LootPowerup()
    {
        int accumulativeProb = 0;
        int currentProb = Random.Range(0, 100);

        for (int i = 0; i < loots.Length; i++)
        {
            accumulativeProb += loots[i].lootChance;
            if (currentProb <= accumulativeProb)
            {
                return loots[i].lootObject;
            }
        }
        
        return null;
    }
}
