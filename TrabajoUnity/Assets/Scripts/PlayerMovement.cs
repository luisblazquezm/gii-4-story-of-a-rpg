using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.1f; // Change the speed in the interphace!!!!
    protected Transform _transform;
    protected Animator _animator;
    protected Rigidbody2D _rigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
        _rigidBody2D = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 vector3 = new Vector3();
        vector3.x = Input.GetAxisRaw("Horizontal");
        vector3.y = Input.GetAxisRaw("Vertical");
        vector3.Normalize();
        MovePlayerAnimation(vector3);
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
}
