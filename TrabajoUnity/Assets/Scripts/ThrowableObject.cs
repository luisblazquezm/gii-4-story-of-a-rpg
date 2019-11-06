using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    public float speed;
    public Rigidbody2D projectileRigidBody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
