using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle,
    specialPower
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed = 1.1f; // Change the speed in the interphace!!!!
    protected Transform _transform;
    protected Animator _animator;
    protected Rigidbody2D _rigidBody2D;
    public FloatValue currentHealth;
    private float currentPlayerHealth;
    public FloatValue currentMana;
    private float currentPlayerMana;
    public Image manaBarr;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.walk;
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        currentPlayerHealth = currentHealth.initialValue;
        currentPlayerMana = currentMana.initialValue;
    }

    void Update()
    {
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
            && currentState != PlayerState.attack 
            && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackPlayerCoroutine());
        } 
        else if (Input.GetMouseButtonDown(1) 
                 && currentState != PlayerState.attack 
                 && currentState != PlayerState.stagger
                 && currentPlayerMana >= 0)
        {
            Debug.Log("PlayerMovement: SPecialPower");
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
        _animator.SetBool("attack", true);
        currentState = PlayerState.attack;
        yield return null;
        
        // This allows to not come back to the animation of attacking once we do it 
        _animator.SetBool("attack", false);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.walk;
    }
    
    // We use a coroutine to do various things in parallel. In this case to use the special power
    private IEnumerator SpecialPowerPlayerCoroutine()
    {
        _animator.SetBool("special power", true);
        currentState = PlayerState.specialPower;
        currentPlayerMana -= 5;
        
        // Change the image display of the barr
        var rectTransformPosition = manaBarr.rectTransform.position;
        rectTransformPosition.x -= 31f;
        manaBarr.rectTransform.position = rectTransformPosition;
        manaBarr.rectTransform.sizeDelta = new Vector2(manaBarr.rectTransform.sizeDelta.x - 50, manaBarr.rectTransform.sizeDelta.y);
        yield return null;
        
        // This allows to not come back to the animation of attacking once we do it 
        _animator.SetBool("special power", false);
        yield return new WaitForSeconds(0.3f);
        currentState = PlayerState.walk;
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
    
    void destroyInstance()
    {
        Debug.Log("Entre");
        //Destroy(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
