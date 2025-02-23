using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpForce = 5f;
    public float gravity = 9.8f;

    public Transform playerCamera;
    public float mouseSensitivity = 2f;

    public Transform groundCheck;
    public LayerMask groundMask;

    private Rigidbody rb;
    private float moveSpeed;
    private bool isGrounded;
    private bool canJump = true;
    private float rotationX = 0f;

    private bool isMoving;
    private float footstepTimer;
    public float walkFootstepDelay = 0.5f;
    public float runFootstepDelay = 0.3f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveSpeed = walkSpeed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
        Jump();
        RotateCamera();
        HandleFootsteps();
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.velocity = new Vector3(move.x * moveSpeed, rb.velocity.y, move.z * moveSpeed);

        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        isMoving = (moveX != 0 || moveZ != 0);
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundMask);

        if (isGrounded)
        {
            canJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canJump)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            canJump = false;

            // **Stop footstep sound & play jump sound**
            if (AudioManagerBara.Instance != null)
            {
                AudioManagerBara.Instance.bajingloncat(); //  Play Suara Loncat
            }
        }
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleFootsteps()
    {
        if (!isGrounded || !isMoving)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManagerBara.Instance.sfxSource.Stop();  // Stop Footstep Sound
            Invoke("PlayFootstepSound", 2f);
            AudioManagerBara.Instance.bajingloncat();
        }

        footstepTimer -= Time.deltaTime;
        if (footstepTimer <= 0f)
        {
            footstepTimer = Input.GetKey(KeyCode.LeftShift) ? runFootstepDelay : walkFootstepDelay;

            if (AudioManagerBara.Instance != null)
            {
                AudioManagerBara.Instance.PlayFootstep(); //  Play Suara Langkah Kaki
            }
        }
    }
}
