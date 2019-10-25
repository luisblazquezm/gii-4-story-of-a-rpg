using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 1.1f; // Change the speed in the interphace!!!!
    protected Transform _transform;
    protected Animator _animator;
    
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 vector2 = new Vector2();
        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");
        vector2.Normalize();

        MovePlayer(vector2);
    }
   
    void MovePlayer(Vector2 vector2)
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
        vector2.x *= speed * Time.deltaTime;
        vector2.y *= speed * Time.deltaTime;
        vector2.x = Mathf.Clamp(_transform.position.x + vector2.x, vector2min.x, vector2max.x);
        vector2.y = Mathf.Clamp(_transform.position.y + vector2.y, vector2min.y, vector2max.y);
        _transform.position = new Vector2(vector2.x, vector2.y);
    }
}
