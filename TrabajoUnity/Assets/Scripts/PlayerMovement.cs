using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle,
    specialPower,
    dead
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed = 1.1f; // Change the speed in the interphace!!!!
    public FloatValue currentHealth;
    public FloatValue currentMana;
    public Image manaBarr;
    public Inventory playerInventory;
    public VectorValue startingPosition;
    public bool attackActivated;
    public bool secondPowerActivated;
    public GameObject[] projectiles;
    public RuntimeAnimatorController[] animatorControllers;
    public AudioClip[] attackSounds;
    
    private float currentPlayerMana;
    private float currentPlayerHealth;
    protected Rigidbody2D _rigidBody2D;
    protected Animator _animator;
    protected Transform _transform;

    private void Awake()
    {
        playerInventory.coins = 0;

        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
            DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("En Start");
        attackActivated = false;
        secondPowerActivated = false;
        currentState = PlayerState.walk;
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        currentPlayerHealth = currentHealth.initialValue;
        currentPlayerMana = currentMana.initialValue;
        Debug.Log("La escena es: " + SceneManager.GetActiveScene().name + " y el startingPosition es: " + startingPosition);
        Debug.Log("La posicion inicial es x: " + startingPosition.initialValue.x + " - y: " +
                  startingPosition.initialValue.y);
        _transform.position = startingPosition.initialValue;
        
    }

    void Update()
    {
        Debug.Log("En Update");
        if (currentState == PlayerState.interact)
        {
            return;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vector3 = new Vector3();
        vector3.x = Input.GetAxisRaw("Horizontal");
        vector3.y = Input.GetAxisRaw("Vertical");
        vector3.Normalize();

        // Left click to attack
        if (Input.GetMouseButtonDown(0) 
            && attackActivated
            && currentState != PlayerState.attack 
            && currentState != PlayerState.stagger)
        {
            Debug.Log("PlayerMovement: Attack");
            StartCoroutine(AttackPlayerCoroutine());
        } 
        else if (Input.GetMouseButtonDown(1) 
                 && currentState != PlayerState.attack 
                 && currentState != PlayerState.stagger
                 && secondPowerActivated
                 && currentPlayerMana >= 0)
        {
            Debug.Log("PlayerMovement: SpecialPower");
            StartCoroutine(SpecialPowerPlayerCoroutine());
        }
        else if (currentState == PlayerState.walk || currentState == PlayerState.idle)
        {
            MovePlayerAnimation(vector3);
        } 
        
    }

    // We use a coroutine to do various things in parallel. Like walking and attacking
    private IEnumerator AttackPlayerCoroutine()
    {
        Debug.Log("EL id del arma es: " + playerInventory.currentWeaponID);
        _animator.SetBool("attack", true);
        currentState = PlayerState.attack;
        yield return null;
        
        // This allows to not come back to the animation of attacking once we do it 
        _animator.SetBool("attack", false);
        this.GetComponent<AudioSource>().PlayOneShot(attackSounds[playerInventory.currentWeaponID]);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.walk;
    }
    
    // We use a coroutine to do various things in parallel. In this case to use the special power
    private IEnumerator SpecialPowerPlayerCoroutine()
    {
        _animator.SetBool("specialPower", true);
        currentState = PlayerState.specialPower;
        currentPlayerMana -= 5;

        // Create the projectile or throwable object
        //MakeProjectileOnPlayerPosition(); IMPORTANT: it is done as an event in the animation
        
        // Change the image display of the barr
        manaBarr.rectTransform.sizeDelta = new Vector2(manaBarr.rectTransform.sizeDelta.x - 30, manaBarr.rectTransform.sizeDelta.y);
        yield return null;
        
        // This allows to not come back to the animation of attacking once we do it 
        _animator.SetBool("specialPower", false);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.walk;
    }

    private void MakeProjectileOnPlayerPosition()
    {
        Debug.Log("The ID of the object throwing is: " + playerInventory.currentWeaponID);
        Vector2 temp = new Vector2(_animator.GetFloat("moveX"),
                                   _animator.GetFloat("moveY"));
        
        // If it´s thor´s hammer we will play the animation to throw it back and forth
        if (playerInventory.currentWeaponID == 1)
        {
            StartCoroutine(ChangeAnim(temp, GameObject.Find("Mjolnir")));
            ThrowableObject projectile = Instantiate(this.projectiles[playerInventory.currentWeaponID], transform.position, Quaternion.identity).GetComponent<ThrowableObject>();
            projectile.GetComponent<SpriteRenderer>().enabled = false;
            projectile.Setup(temp, ChooseProjectileDirection());
        }
        else
        {
            ThrowableObject projectile = Instantiate(this.projectiles[playerInventory.currentWeaponID], transform.position, Quaternion.identity).GetComponent<ThrowableObject>();
            projectile.Setup(temp, ChooseProjectileDirection());
        }

    }

    private IEnumerator ChangeAnim(Vector2 direction, GameObject weapon)
    {
        // Controls movement of the enemy
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                weapon.GetComponent<SpriteRenderer>().enabled = true;
                weapon.GetComponent<Animator>().enabled = true;
                weapon.GetComponent<Animator>().Play("hammerThrowable");
                yield return new WaitForSeconds(1.2f);
                
                weapon.GetComponent<SpriteRenderer>().enabled = false;
                weapon.GetComponent<Animator>().enabled = false;
            } 
            else if (direction.x < 0)
            {
                weapon.GetComponent<SpriteRenderer>().enabled = true;
                weapon.GetComponent<Animator>().enabled = true;

                weapon.GetComponent<Animator>().Play("hammerThrowableLeft");
                yield return new WaitForSeconds(1.2f);
                
                weapon.GetComponent<SpriteRenderer>().enabled = false;
                weapon.GetComponent<Animator>().enabled = false; // To stop the animation. Stop is deprecated
            }
        } 
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                weapon.GetComponent<SpriteRenderer>().enabled = true;
                weapon.GetComponent<Animator>().enabled = true;
                weapon.GetComponent<Animator>().Play("hammerThrowableUp");
                yield return new WaitForSeconds(1.2f);
                
                weapon.GetComponent<SpriteRenderer>().enabled = false;
                weapon.GetComponent<Animator>().enabled = false; // To stop the animation. Stop is deprecated
            } 
            else if (direction.y < 0)
            {
                weapon.GetComponent<SpriteRenderer>().enabled = true;
                weapon.GetComponent<Animator>().enabled = true;
                weapon.GetComponent<Animator>().Play("hammerThrowable");
                yield return new WaitForSeconds(1.2f);
                
                weapon.GetComponent<SpriteRenderer>().enabled = false;
                weapon.GetComponent<Animator>().enabled = false; // To stop the animation. Stop is deprecated
            }
        }
    }

    Vector3 ChooseProjectileDirection()
    {
        float temp = Mathf.Atan2(_animator.GetFloat("moveY"), _animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    
    void MovePlayerAnimation(Vector3 vector3)
    {
        if (vector3 != Vector3.zero)
        {
            MovePlayer(vector3);
            _animator.SetFloat("moveX", vector3.x);
            _animator.SetFloat("moveY", vector3.y);
            _animator.SetBool("walk", true);
        }
        else
        {
            _animator.SetBool("walk", false);
        }
    }
   
    void MovePlayer(Vector3 vector3)
    {
        _rigidBody2D.MovePosition(
            _transform.position + vector3 * speed * Time.deltaTime
        );
    }
    
    public void Knock(float knockTime, float damage)
    {
        GameObject.FindWithTag("Hearth Container").GetComponent<HeartManager>().UpdateHearts(currentPlayerHealth);
        currentPlayerHealth -= damage;
        GameObject.FindWithTag("Hearth Container").GetComponent<HeartManager>().UpdateHearts(currentPlayerHealth);

        if (currentPlayerHealth > 0)
        {
            StartCoroutine(KnockCoroutine(knockTime));
        }
        else
        {
            // Player dies
            _animator.SetBool("die", true);
            //Destroy(this.gameObject);
        }
    }
    
    private IEnumerator KnockCoroutine(float knockTime)
    {
        if (_rigidBody2D != null)
        {
            yield return  new WaitForSeconds(knockTime); // It works like sleep() or wait()
            _rigidBody2D.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            _rigidBody2D.velocity = Vector2.zero;
        }
    }

    public void SetAnimatorController()
    {
        this.GetComponent<Animator>().runtimeAnimatorController = animatorControllers[playerInventory.currentWeaponID] as RuntimeAnimatorController;
    }
    
    void destroyInstance()
    {
        Debug.Log("Entre");
        Enemy.playerDead = true;
        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }
}
