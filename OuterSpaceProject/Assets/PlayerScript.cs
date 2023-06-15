using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    [Range(0.75f, 1f)]
    public float brakeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (vertical != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, vertical * speed);
        } else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z * brakeSpeed);
        }
        if (horizontal != 0)
        {
            rb.velocity = new Vector3(horizontal * speed, 0, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(rb.velocity.x * brakeSpeed, 0, rb.velocity.z);
        }

        if (horizontal != 0 && vertical != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x * (1 / Mathf.Sqrt(2)), 0, rb.velocity.z * (1 / Mathf.Sqrt(2)));
        }
    }
}
//Dumrika
//Julianduym