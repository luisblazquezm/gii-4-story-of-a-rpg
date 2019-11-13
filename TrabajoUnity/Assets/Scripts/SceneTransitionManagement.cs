using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class SceneTransitionManagement : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerNewPosition;
    public VectorValue playerStorage;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;
    public bool introScene;
    
    /*private void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }
*/
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("En OnTrigger");

        if (other.CompareTag("Player") && !other.isTrigger && introScene)
        {
            Debug.Log("The new position will be x: " + playerNewPosition.x +" y: " + playerNewPosition.y );
            playerStorage.initialValue = playerNewPosition;
            StartCoroutine(FadeCoroutine());
        }
        else
        {
            Debug.Log("Entered in Zone 1 or Zone 2");
            GameObject.Find("Player").transform.position = playerNewPosition;
            GameObject.Find("Main Camera").transform.position = playerNewPosition;
            StartCoroutine(FadeCoroutine());
        }
        
    }

    public IEnumerator FadeCoroutine()
    {
        if (fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
