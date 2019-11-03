using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;
    
    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            hearts[i].gameObject.SetActive(true);
            hearts[i].sprite = fullHeart;
        }
    }

    public void UpdateHearts(float currentPlayerHealth)
    {
        float tempHealth = currentPlayerHealth / 2;
        for (int i = 0; i < heartContainers.initialValue; i++)
        {
            if (i <= tempHealth - 1)
            {
                // Full hearth
                Debug.Log("FULL HEARTH");
                hearts[i].sprite = fullHeart;
            } 
            else if (i >= tempHealth)
            {
                // Empty Hearth
                Debug.Log("EMPTY HEARTH");
                hearts[i].sprite = emptyHeart;
            } 
            else
            {
                // Half full hearth
                Debug.Log("HALF HEARTH");
                hearts[i].sprite = halfFullHeart;
            }
            
        }
    }
}
