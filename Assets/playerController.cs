using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public GameObject body;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.05f;
    public LayerMask groundMask;

    private Vector3 velocity;
    public float inertia;

    public bool isJumping { get; private set; }
    public bool isWallRunning;

    private void Start()
    {
        isJumping = false;
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");
        if (velocity.y < 0) { isJumping = false; }

        if (IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (IsGrounded())
        {
            inertia = Mathf.Clamp(inertia - Time.deltaTime * 0.3f, 1, 2);
        }
        else
        {
            inertia = Mathf.Clamp(inertia + Time.deltaTime * 0.3f, 1, 2);
        }

        Vector3 move = transform.right * xAxis + transform.forward * zAxis * inertia;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButton("Jump") && IsGrounded())
        {
            Jump();
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        if (isWallRunning)
            return false;
        return Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    public void Jump(WallPosition wallpos = WallPosition.Null)
    {
        isJumping = true;

        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
}