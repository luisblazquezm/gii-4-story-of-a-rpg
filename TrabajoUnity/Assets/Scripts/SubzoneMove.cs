using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubzoneMove : MonoBehaviour
{
    // If the change is to the right/left we change the value of the X
    // otherwhise we change the value of the Y (up/down)
    
    // IMPORTANT the camera change values are the amount
    // need to position the camera at the beggining of the next subzone
    public Vector2 cameraChange; // How many "positions" will change the camera
    public Vector3 playerChange; // How much if going to shift the player
    private CameraMovement _cam;
    
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Adds the number of points or positions of the new camera change
            _cam.minPosition += cameraChange;
            _cam.maxPosition += cameraChange; // Adds vector2 because the camera does not get to the top in zone2
            other.transform.position += playerChange;

            // IMPORTANT: if the room is not square , we will need 2 vectors
            // One for the amount of change to the minPosition and another for the amount
            // of maxPosition
        }
    }
}
