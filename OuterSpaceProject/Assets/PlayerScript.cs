using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Components")]
    public Rigidbody rb;
    public GameObject sword;
    public Animator animator;
    public GameObject mesh;
    public Material crystalMaterial;
    public Material swordMaterial;
    public Camera cam;
    [Header("Movement")]
    public float speed;
    [Range(0.75f, 1f)]
    public float brakeSpeed;

    [HideInInspector]
    public bool isHit;
    [Header("Health")]
    public float hitCooldown;
    [HideInInspector]
    public float hitTimer;
    public float maxhp = 100;
    public float hp = 100;
    [ColorUsageAttribute(false, true)]
    public Color hitColor;
    [ColorUsageAttribute(false, true)]
    public Color defaultColor;

    [Header("Damage")]
    public float knockback;
    public float damage;
    //[HideInInspector]
    public bool isSwinging;

    // Update is called once per frame
    void Update()
    {
        Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit2;

        if (Physics.Raycast(ray2, out hit2))
        {
            mesh.transform.LookAt(new Vector3(hit2.point.x, mesh.transform.position.y, hit2.point.z));
            mesh.transform.Rotate(new Vector3(0, 180, 0));
        }

        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
                isHit = false;
            }
            crystalMaterial.color = hitColor;
            swordMaterial.color = defaultColor;
        } else
        {
            crystalMaterial.color = defaultColor;
            swordMaterial.color = hitColor;

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    animator.SetTrigger("Swing");
                }
            }
        }
    }

    private void FixedUpdate()
    {
        float vertical = 0;
        float horizontal = 0;

        if (isHit)
        {
            vertical = Input.GetAxisRaw("Vertical") * (speed / 2);
            horizontal = Input.GetAxisRaw("Horizontal") * (speed / 2);
        } else
        {
            vertical = Input.GetAxisRaw("Vertical") * speed;
            horizontal = Input.GetAxisRaw("Horizontal") * speed;
        }

        // str = horizontal.ToString() + ", " + vertical.ToString();
        //Debug.Log(str);
        /*
        if (horizontal > 0f && vertical > 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 45 + 180, transform.rotation.eulerAngles.z);
        } else if (horizontal < 0f && vertical > 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -45 + 180, transform.rotation.eulerAngles.z);
        } else if (horizontal > 0f && vertical < 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 135 + 180, transform.rotation.eulerAngles.z);
        } else if (horizontal < 0f && vertical < 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -135 + 180, transform.rotation.eulerAngles.z);
        } else if (horizontal > 0f && vertical == 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, -90, transform.rotation.eulerAngles.z);
        } else if (horizontal < 0f && vertical == 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 90, transform.rotation.eulerAngles.z);
        } else if (horizontal == 0f && vertical < 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);
        } else if (horizontal == 0f && vertical > 0)
        {
            mesh.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 180, transform.rotation.eulerAngles.z);
        }
        */

        if (vertical != 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, vertical);
        } else
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z * brakeSpeed);
        }
        if (horizontal != 0)
        {
            rb.velocity = new Vector3(horizontal, 0, rb.velocity.z);
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