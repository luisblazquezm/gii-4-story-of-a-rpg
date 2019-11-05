using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAwardBehaviour : ChestBehaviour
{

    public AudioClip[] audioSongs;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && isOpen)
        {
            chestAlreadyOpened();
        }
        
        // Getkey gives problems so better getkeydown
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !isOpen)
        {
            Debug.Log("OLE2");
            if (!isOpen)
            {
                // Play music
                _audioSongChest.PlayOneShot(audioSongs[playerInventory.currentWeaponID]);
                
                // Open the chest
                openChest();
                CloseOtherChests();
            }
            else
            {
                // Chest is already open
                chestAlreadyOpened();
                
                // Stop song and play general audio
                _audioSongChest.Stop();
            }
        }

    }
    
}
