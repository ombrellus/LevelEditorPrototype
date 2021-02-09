using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rig;
    void Start()
    {
        
    }

    // Simple player movement
    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

       
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rig.MovePosition((Vector2)transform.position + (movement * speed * Time.deltaTime));
    }
}
