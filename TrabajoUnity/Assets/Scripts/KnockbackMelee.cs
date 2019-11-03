using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackMelee : MonoBehaviour
{

    public float knockback;
    public float knockTime;
    public float damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Player"))
        {
            // Change from kinematic to dynamic (so the object (enemy or player) is pulled back)
            Rigidbody2D hitObject = other.GetComponent<Rigidbody2D>();

            if (hitObject != null)
            {
                Vector2 difference = hitObject.transform.position - transform.position;
                difference = difference.normalized * knockback;
                hitObject.AddForce(difference, ForceMode2D.Impulse);
            
                if (other.gameObject.CompareTag("Enemy"))
                {
                    hitObject.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hitObject, knockTime, damage);
                }

                if (other.gameObject.CompareTag("Player"))
                {
                    if (other.GetComponent<PlayerMovement>().currentState != PlayerState.stagger)
                    {
                        hitObject.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                    
                }

            }
        }
    }

    
}
