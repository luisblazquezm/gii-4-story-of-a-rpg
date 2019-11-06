using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ChestBehaviour : Interactable
{
    public int itemID;
    public Item contentItem;
    public Inventory playerInventory;
    public bool isOpen;
    public bool isFinalChest;
    public GameObject dialogBox;
    public GameObject player;
    public Text dialogText;
    public SpriteRenderer receivedItemSprite;
    public AudioClip[] _audioClips;
    
    private AudioSource _audioSongChest;
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
            if (!isOpen)
            {
                // Open the chest
                openChest();
                CloseOtherChests();
                PrepareCharacter();
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

        if (isFinalChest)
        {
            _audioSongChest.loop = true;
            _audioSongChest.PlayOneShot(_audioClips[playerInventory.currentWeaponID]);
        }
        else
        {
            // Play music
            _audioSongChest.PlayOneShot(_audioClips[0]);
        }

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
        
        if (!isFinalChest)
        {
            // Play music
            _audioSongChest.Stop();
        }

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

        playerInventory.currentWeaponID = this.itemID;
    }

    public void PrepareCharacter()
    {
        Debug.Log("Preparing Character");
        // First activate its sprite and animation of attack depending on the weapon chosed
        switch (this.itemID)
        {
            case 0:
                _animatorPlayer.SetBool("weapon0", true);
                break;
            case 1:
                _animatorPlayer.SetBool("weapon1", true);
                break; 
            case 2:
                _animatorPlayer.SetBool("weapon2", true);
                break;
            default:
                break;
        }
        
        // Activate second power
        GameObject.FindWithTag("Player").GetComponent<PlayerMovement>().secondPowerActivated = true;
        
        // Activates the teleport from forest3 to forest4. Now the player can travel
        GameObject.FindWithTag("TeleportAttack").SetActive(true);
    }
}
