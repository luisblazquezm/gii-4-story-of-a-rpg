using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SetupScene : MonoBehaviour
{
    private GameObject _thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "FinalScene")
        {
            _thePlayer = GameObject.Find("Player");
            
            // Get position of the player for the camea
            Debug.Log("En FinalScene");
            Transform temp = _thePlayer.transform;
            GameObject.Find("Main Camera").GetComponent<CameraMovement>()._target = temp;

            // Get Mana barr
            _thePlayer.GetComponent<PlayerMovement>().manaBarr = GameObject.Find("Mana Energy").GetComponent<Image>();
            
            // Update coins
            GameObject.FindWithTag("Coin Text").GetComponent<CoinTextManager>().UpdateCoinCount();
            
            // PowerUpChest
            GameObject.Find("PowerUpChest").GetComponent<ChestBehaviour>().dialogBox = GameObject.Find("Dialog box");
            GameObject.Find("PowerUpChest").GetComponent<ChestBehaviour>().dialogText = GameObject.Find("Text").GetComponent<Text>();
            GameObject.Find("Dialog box").SetActive(false);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
