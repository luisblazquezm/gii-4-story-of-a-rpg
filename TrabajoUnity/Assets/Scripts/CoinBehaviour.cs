using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    public Inventory playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory.coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coins += 1;
            GameObject.FindWithTag("Coin Text").GetComponent<CoinTextManager>().UpdateCoinCount();
            Destroy(this.gameObject);
        }
    }
}
