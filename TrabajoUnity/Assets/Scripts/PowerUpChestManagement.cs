using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpChestManagement : MonoBehaviour
{
    public Vector3 positionOnNextScene;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        transform.position = positionOnNextScene;
    }
}
