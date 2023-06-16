using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject sword;
    public Animator animator;
    public Camera cam;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //sword.transform.LookAt(new Vector3(hit.point.x, sword.transform.position.y, hit.point.z));
                animator.SetTrigger("Swing");
                //sword.transform.rotation = Quaternion.Euler(sword.transform.rotation.eulerAngles.x + 90, sword.transform.rotation.eulerAngles.y, sword.transform.rotation.eulerAngles.z);
            }
            //sword.transform.rotation = Quaternion.Euler(sword.transform.rotation.eulerAngles.x, sword.transform.rotation.eulerAngles.y, sword.transform.rotation.eulerAngles.z + (100 * Time.deltaTime));
        }
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