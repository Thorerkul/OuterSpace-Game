using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Components")]
    public GameObject sword;
    public GameObject wheel;
    public Animator animator;
    public GameObject mesh;
    public Material crystalMaterial;
    public Material swordMaterial;
    public Camera cam;
    public WorldScript world;
    [Header("Movement")]
    public float speed;
    [Range(0.75f, 1f)]
    public float brakeSpeed;
    private Vector3 topDownPos;

    [HideInInspector]
    public bool isHit;
    [Header("Health")]
    public float hitCooldown;
    [HideInInspector]
    public float hitTimer;
    public float maxhp = 100;
    public float hp = 100;
    public float defence;
    [ColorUsageAttribute(false, true)]
    public Color hitColor;
    [ColorUsageAttribute(false, true)]
    public Color defaultColor;

    [Header("Damage")]
    public float knockback;
    public float damage;
    public float armorPenetration = 0;
    [HideInInspector]
    public bool isSwinging;

    private void Start()
    {
        topDownPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
        {
            isSwinging = true;
        } else
        {
            isSwinging = false;
        }

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
        */
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

        //x axis movement
        transform.position += new Vector3(horizontal, 0, 0) * Time.deltaTime;

        transform.localPosition += new Vector3(0, 0, vertical) * Time.deltaTime;

        /*
        // Create a vector for the input.
        Vector3 inputVector = new Vector3(horizontal, 0f, vertical).normalized;

        // Check if the input is not zero, and only then move the object.
        if (inputVector != Vector3.zero)
        {
            // Convert the input vector to the rotated vector using the ReturnNewCoordinates function.
            Vector3 rotatedVector = world.ReturnNewCoordinates(inputVector, world.rotation.x);

            // Translate the object based on the rotated vector.
            transform.Translate(rotatedVector * speed * Time.deltaTime, Space.World);
        }
        */
    }
}
