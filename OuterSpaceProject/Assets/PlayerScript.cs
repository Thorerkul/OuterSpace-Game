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
    public float rotationSpeed;

    [HideInInspector]
    public bool isHit;
    [Header("Health")]
    public float hitCooldown;
    [HideInInspector]
    public float hitTimer;
    public float maxhp = 100;
    public float hp = 100;
    public float defence;
    [ColorUsage(false, true)]
    public Color hitColor;
    [ColorUsage(false, true)]
    public Color defaultColor;

    [Header("Damage")]
    public float knockback;
    public float damage;
    public float armorPenetration = 0;
    [HideInInspector]
    public bool isSwinging;

    // Update is called once per frame
    void Update()
    {
        // Controller rotation Generated by ChatGPT
        float horizontalInput = Input.GetAxis("RightJoystickHorizontal");
        float verticalInput = Input.GetAxis("RightJoystickVertical");

        // Check if the joystick input is significant enough to rotate the GameObject.
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            // Calculate the angle between the joystick input and the forward direction of the GameObject.
            Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            float angle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;

            // Calculate the target rotation as a local rotation around the Y-axis.
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);

            // Apply the rotation to the GameObject's local rotation using Slerp.
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        MouseRotation();

        
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("SwordSwing"))
        {
            isSwinging = true;
        } else
        {
            isSwinging = false;
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
                animator.SetTrigger("Swing");
            }
        }
        
    }

    void MouseRotation()
    {
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        Vector2 mouseDir = new Vector2(horizontal, vertical);

        Vector2 direction = new Vector2(Mathf.Clamp(mouseDir.x, -1f, 1f), Mathf.Clamp(mouseDir.x, -1f, 1f));

        if (mouseDir.x != 0 || mouseDir.y != 0)
        {
            // Calculate the angle in degrees to the given direction.
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

            LookAtDirection(this.gameObject, direction);
        }

    }

    void LookAtDirection(GameObject gameObject, Vector2 direction)
    {
        // Calculate the angle in degrees to the given direction.
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

        // Set the GameObject's rotation to point in the calculated direction on its local Y-axis.
        gameObject.transform.localRotation = Quaternion.Euler(0f, -angle, 0f);
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
