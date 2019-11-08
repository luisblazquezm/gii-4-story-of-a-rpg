using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public float speed;
    public Rigidbody2D projectileRigidBody;
    private GameObject _theCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _theCamera = GameObject.Find("Main Camera");
    }

    /*
    private void Update()
    {
        if (this.transform.position.x > camera.GetComponent<CameraMovement>().maxPosition.x ||
            this.transform.position.x > camera.GetComponent<CameraMovement>().minPosition.x ||
            this.transform.position.y > camera.GetComponent<CameraMovement>().maxPosition.y ||
            this.transform.position.y > camera.GetComponent<CameraMovement>().minPosition.y)
        {
            Destroy(this.gameObject);
        }
    }
    */

    public void Setup(Vector2 velocity, Vector3 direction)
    {
        projectileRigidBody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
            
    }
}
