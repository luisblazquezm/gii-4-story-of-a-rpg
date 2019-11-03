using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public enum EnemyState
{
    walk,
    attack,
    stagger,
    idle
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    private void Start()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Animator _animatorEnemy = GetComponent<Animator>();
            _animatorEnemy.SetBool("die", true);
        }
    }

    public void Knock(Rigidbody2D enemyRB, float knockTime, float damage)
    {
        StartCoroutine(KnockCoroutine(enemyRB, knockTime));
        TakeDamage(damage);
    }
    
    void destroyInstanceEnemy()
    {
        Debug.Log("Entre");
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }
    
    private IEnumerator KnockCoroutine(Rigidbody2D enemyRB, float knockTime)
    {
        if (enemyRB != null)
        {
            yield return  new WaitForSeconds(knockTime); // It works like sleep() or wait()
            enemyRB.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            enemyRB.velocity = Vector2.zero;
        }
    }
}
