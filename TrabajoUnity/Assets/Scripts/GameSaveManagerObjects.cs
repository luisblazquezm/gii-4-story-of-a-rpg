using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManagerObjects : MonoBehaviour
{
    public ScriptableObject[] objects;

    private void Awake()
    {
        DontDestroyOnLoad(this);

        for (int i = 0; i < objects.Length; i++)
        {
            DontDestroyOnLoad(objects[i]);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
