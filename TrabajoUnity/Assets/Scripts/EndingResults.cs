using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingResults : MonoBehaviour
{
    private Inventory playerInventory;
    public Text coinDisplay;
    
    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("Player").GetComponent<PlayerMovement>().playerInventory;
        coinDisplay.text = "" + playerInventory.coins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
