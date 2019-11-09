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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            if (this.CompareTag("Diamond"))
            {
                playerInventory.coins += 5;
            }
            else
            {
                playerInventory.coins += 1;
            }
            
            GameObject.FindWithTag("Coin Text").GetComponent<CoinTextManager>().UpdateCoinCount();
            Destroy(this.gameObject);
        }
    }
}
