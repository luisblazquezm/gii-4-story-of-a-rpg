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
    public GameObject[] characters;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private int i = 0;
    private AudioSource _audioSongChest;
    private Animator _animatorChest, _animatorPlayer;
    private GameObject _thePlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        _animatorChest = GetComponent<Animator>();
        _animatorPlayer = player.GetComponent<Animator>();
        _audioSongChest = GetComponent<AudioSource>();
        _audioSongChest.Stop();
        _thePlayer = GameObject.FindWithTag("Player");
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
            if (isFinalChest)
            {
                if (!isOpen)
                {
                    // Open the chest
                    openChest();
                }
                else
                {
                    // Chest is already open
                    chestAlreadyOpened();
                }
            }
            else
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

        if (!isFinalChest)
        {
            // Play music
            _audioSongChest.PlayOneShot(_audioClips[0]);
        }

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
        else
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().Stop();
            _audioSongChest.loop = true;
            _audioSongChest.PlayOneShot(_audioClips[playerInventory.currentWeaponID]);
            TransformCharacter();
        }

        //player.transform.GetChild(1).gameObject; // Get the received item
    }

    private void CloseOtherChests()
    {
        GameObject[] otherChests = GameObject.FindGameObjectsWithTag("Chest");
        for (int i = 0; i < otherChests.Length; i++)
        {
            if (otherChests[i].gameObject != this.gameObject)
                otherChests[i].SetActive(false);
        }

        playerInventory.currentWeaponID = this.itemID;
    }

    private void PrepareCharacter()
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
        
        // Activate second power and attack
        _thePlayer.GetComponent<PlayerMovement>().attackActivated = true;
        _thePlayer.GetComponent<PlayerMovement>().secondPowerActivated = true;
        
        // Activates the teleport from forest3 to forest4. Now the player can travel
        GameObject.Find("Sign Forest 6").SetActive(false);
    }

    private void TransformCharacter()
    {
        // Fade with the blank fading
        StartCoroutine(FadeCoroutine());
        
    }
    
    public IEnumerator FadeCoroutine()
    {
        GameObject panelOut = null;
        
        if (fadeOutPanel != null)
        {
            panelOut = Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        
        // Set the new animator controller
        
        // Deactivate default and activate the next player
        //GameObject.Find("Player").SetActive(false);
        
        _thePlayer.GetComponent<PlayerMovement>().SetAnimatorController();
        
        // First activate its sprite and animation of attack depending on the weapon chosed
        //characters[this.itemID].SetActive(true);
        
        while (i < 50)
        {
            i++;
            Debug.Log("JAJAJA");
            yield return null;
        }
        
        Debug.Log("Ole");
        if (fadeInPanel != null)
        {
            Destroy(panelOut, 1);
            GameObject panelIn = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panelIn, 1);
        }
        
        yield return new WaitForSeconds(fadeWait);
    }
}
