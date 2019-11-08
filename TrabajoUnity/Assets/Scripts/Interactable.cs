using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool playerInRange;
    public GameObject contextClue;
    
    // Start is called before the first frame update
    void Start()
    {
        contextClue.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ENTER of Trigger Interactable");
            playerInRange = true;
            contextClue.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXITE");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Exit of Trigger Interactable");
            playerInRange = false;
            contextClue.SetActive(false);
        }
    }
}
