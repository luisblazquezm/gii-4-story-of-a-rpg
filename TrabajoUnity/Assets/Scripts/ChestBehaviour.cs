using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ChestBehaviour : Interactable
{
    public Item contentItem;
    public Inventory playerInventory;
    public bool isOpen;
    public GameObject dialogBox;
    public GameObject player;
    public Text dialogText;
    public SpriteRenderer receivedItemSprite;
    public AudioSource _audioSongChest;
    
    private Animator _animatorChest, _animatorPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        _animatorChest = GetComponent<Animator>();
        _animatorPlayer = player.GetComponent<Animator>();
        _audioSongChest = GetComponent<AudioSource>();
        _audioSongChest.Stop();
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
                _audioSongChest.Play();
                
                // Open the chest
                openChest();
                CloseOtherChests();
            }
            else
            {
                // Chest is already open
                chestAlreadyOpened();
            }
        }

    }

    public void openChest()
    {
        // DialogBox on
        dialogBox.SetActive(true);
        
        // Dialog text
        dialogText.text = contentItem.itemDescription;

        // Add contents to the inventory
        // playerInventory.AddItem(contentItem);
        // playerInventory.currentItem = contentItem;
        // Allow attack and print dialogBox

        // Animate the player
        _animatorPlayer.SetBool("receive_item", true);
        _animatorChest.SetBool("openChest", true);
        receivedItemSprite.sprite = contentItem.itemSprite;
        playerInventory.AddItem(contentItem);
            
        // Set the chest to opened
        isOpen = true;

        // Raise the context clue
        contextClue.SetActive(true);
    }
    
    public void chestAlreadyOpened()
    {
        // Dialog off
        dialogBox.SetActive(false);
        
        // Set the current item to empty
        playerInventory.currentItem = null;
        receivedItemSprite.sprite = null;
        
        // Stop animation. Back to idle
        _animatorPlayer.SetBool("receive_item", false);
        contextClue.SetActive(false);

        //player.transform.GetChild(1).gameObject; // Get the received item
    }

    public void CloseOtherChests()
    {
        GameObject[] otherChests = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < otherChests.Length; i++)
        {
            if (otherChests[i].gameObject != this.gameObject)
                otherChests[i].SetActive(false);
        }
    }
}
