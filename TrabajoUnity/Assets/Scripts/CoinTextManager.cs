using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    private void Start()
    {
        coinDisplay.text = "" + 0;
    }

    public void UpdateCoinCount()
    {
        coinDisplay.text = "" + playerInventory.coins;
    }
}
