using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    // If the change is to the right/left we change the value of the X
    // otherwhise we change the value of the Y (up/down)
    
    // IMPORTANT the camera change values are the amount
    // need to position the camera at the beggining of the next subzone
    public Vector2 cameraMinChange; // How many "positions" will change the camera
    public Vector2 cameraMaxChange; // How many "positions" will change the camera
    private AudioSource _gameGeneralAudioSource;
    public bool isInterior; // If the zone to telepor is interior we deactivate the music
    public bool outOfInterior = false;
    private CameraMovement _cam;
    public GameObject targetZoneToTeleport;

    private bool start = false; // Controls if the transitions or teleport starts or not
    private bool isFadeIn = false; // Controls if the transition is enter or exit
    private float alpha = 0; // Initial opacity of the black frame of the transition
    private float fadeTime = 1f; // 1 second transition
    
    void Start()
    {
        _cam = Camera.main.GetComponent<CameraMovement>();
        _gameGeneralAudioSource = GameObject.Find("Music").GetComponent<AudioSource>();
    }
    
    // Create an event
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Someone entered: " + other.name);
        
        fadeIn();
        
        yield return new WaitForSeconds(fadeTime);
        
        if (other.name.Equals("Player"))
        {
            // IMPORTANT: if the room is not square , we will need 2 vectors
            // One for the amount of change to the minPosition and another for the amount
            // of maxPosition
            
            Debug.Log("The player teleports");
            
            // Adds the number of points or positions of the new camera change
            _cam.minPosition = cameraMinChange;
            _cam.maxPosition = cameraMaxChange;

            other.transform.position = targetZoneToTeleport.transform.GetChild(0).transform.position; // Change the position of the entry with the exit of the targetZone to teleport (which is the child of the entry)
        }
        
        fadeOut();

        if (isInterior)
        {
            _gameGeneralAudioSource.Stop();
        }
        
        // Only resets the music when the player comes out of an interior zone
        // And not every time it passes from one zone to another
        if (outOfInterior)
        {
            _gameGeneralAudioSource.Play();
        }
    }

    /**
     *
     * This part is to add the black frame when passing
     * from one zone to another.
     * 
     */
    
    // Draws a black frame or box that appears when the transition from one zone to another is done
    private void OnGUI()
    {
        if (!start)
            return;
        
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);

        // Create the texture and the frame
        Texture2D tex;
        tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.black);
        tex.Apply();
        
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), tex);

        if (isFadeIn)
        {
            // If it is enter transition, we add opacity
            alpha = Mathf.Lerp(alpha, 1.1f, fadeTime * Time.deltaTime);
        }
        else
        {
            // If it is exit transition, we substract opacity
            alpha = Mathf.Lerp(alpha, -0.1f, fadeTime * Time.deltaTime);
            
            // Opacity reaches 0. Deactivate the transition
            if (alpha < 0)
                start = false;
        }
    }

    /* Black frame appears. Teleport to another scene */
    void fadeIn()
    {
        start = true;
        isFadeIn = true;
    }

    /* Black frame dissapears */
    void fadeOut()
    {
        isFadeIn = false;
    }
}
