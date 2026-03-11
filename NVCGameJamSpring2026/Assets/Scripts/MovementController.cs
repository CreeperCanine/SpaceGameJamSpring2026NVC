using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform head;
    public float playerSpeed = 5.0f;
    public float playerAcceleration = 2.0f; 
    public float jumpForce = 6.0f;
    public LayerMask groundLayer;



    private Rigidbody rb;
    private Vector3 direction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        direction = Input.GetAxisRaw("Horizontal") * head.right + Input.GetAxisRaw("Vertical") * head.forward;
        rb.velocity = Vector3.Lerp(rb.velocity, direction.normalized * playerAcceleration
            + rb.velocity.y * Vector3.up, playerAcceleration * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isTouchingGround())
         { 
        rb.velocity += jumpForce * Vector3.up;
         }
    }

    private bool isTouchingGround()
    {
        return Physics.CheckBox(transform.position, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, groundLayer);
    }
}
