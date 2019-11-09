using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemyAI : Enemy
{
    public Transform target; // Mainly, the player
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    
    private Animator _animator;
    private Rigidbody2D myRigidBody;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform; // Position of the player
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistanceOfPlayer();
    }

    void CheckDistanceOfPlayer()
    {
        if (!playerDead)
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadius && 
                Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                if (currentState == EnemyState.idle || 
                    currentState == EnemyState.walk && currentState != EnemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, 
                        target.position, 
                        moveSpeed * Time.deltaTime);
                    ChangeAnim(temp - transform.position);
                    myRigidBody.MovePosition(temp);
                
                    ChangeState(EnemyState.walk);
                    _animator.SetBool("wakeUp", true);
                }
            
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
            {
                _animator.SetBool("wakeUp", false);
            }
        }
        
    }

    private void SetAnimFloat(Vector2 setVector)
    {
        _animator.SetFloat("moveX", setVector.x);
        _animator.SetFloat("moveY", setVector.y);
    }

    private void ChangeAnim(Vector2 direction)
    {
        // Controls movement of the enemy
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            } 
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        } 
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            } 
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
    
    void destroyInstance()
    {
        Debug.Log("Entre");
        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }
}
