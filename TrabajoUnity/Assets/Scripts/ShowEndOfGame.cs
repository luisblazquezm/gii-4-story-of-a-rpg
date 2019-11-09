using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowEndOfGame : MonoBehaviour
{
    public GameObject fadeOutPanel;
    
    public void Show()
    {
        Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
    }
}
