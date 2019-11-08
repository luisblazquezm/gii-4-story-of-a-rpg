using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyObject : MonoBehaviour
{
    private void Awake()
    {        
        if(SceneManager.GetActiveScene().name.Equals("SampleScene"))
            DontDestroyOnLoad(this.gameObject);
    }
}
