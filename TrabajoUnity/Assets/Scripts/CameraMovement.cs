using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Transform _target; // Target of the camera
    public float _smoothness; // Smoothing movement of the camera
    public Vector2 maxPosition; // For the camera to not show the borders out of the scene
    public Vector2 minPosition; // For the camera to not show the borders out of the scene
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // Late update is called after Update method
    // So while the player moves in update method the camera goes towards the player in late update
    // After player movements and not while he is moving, so it looks smoother
    void LateUpdate()
    {
        
        // The position of the camera (transform) is not the same of the player (target)
        if (transform.position != _target.position)
        {
            // This is used for the camera to be in front of the player in the Z axis and not behind it
            Vector3 targetPosition = new Vector3(_target.position.x,
                                                 _target.position.y,
                                                 transform.position.z);
            
            // Clamps the maximum and minimum range of the camera
            targetPosition.x = Mathf.Clamp(targetPosition.x,
                                            minPosition.x,
                                            maxPosition.x);
            
            targetPosition.y = Mathf.Clamp(targetPosition.y,
                                            minPosition.y,
                                            maxPosition.y);
            
            // To move towards we use Lerp (Linear interpolation)
            // Find the distance between the camera and the target (the player)
            // And move a percentage of that distance each frame
            transform.position = Vector3.Lerp(transform.position,
                                              targetPosition,
                                              _smoothness);
        }
    }
}
