using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpHeight = 3f;
    public float gravity = -9.81f;
    public float maxJumpTime = 0.5f; // Max time the player can hold jump
    public float lookSpeedX = 2f;
    public float lookSpeedY = 2f;

    [Header("References")]
    public Camera playerCamera;
    public Transform orientation;
    private CharacterController controller;
    private float currentJumpTime = 0f;
    private float rotationX = 0f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isJumping;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Hide mouse cursor
        Cursor.visible = false;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position - Vector3.up * 0.3f, 0.3f, LayerMask.GetMask("Ground"));

        // Handle movement input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 move = orientation.right * horizontal + orientation.forward * vertical;
        move.Normalize(); // Prevent faster diagonal movement

        // Jumping logic
        if (isGrounded)
        {
            if (Input.GetButton("Jump") && currentJumpTime < maxJumpTime)
            {
                isJumping = true;
                currentJumpTime += Time.deltaTime;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump force based on gravity and height
            }
            else
            {
                isJumping = false;
                currentJumpTime = 0f; // Reset jump time if the player isn't holding jump
            }
        }
        else
        {
            if (!isJumping)
            {
                velocity.y += gravity * Time.deltaTime; // Apply gravity when not jumping
            }
        }

        // Apply movement and gravity
        controller.Move((isGrounded ? (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed) : moveSpeed) * Time.deltaTime * move);
        controller.Move(velocity * Time.deltaTime); // Apply vertical velocity

        // Handle camera rotation (mouse look)
        float mouseX = Input.GetAxisRaw("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * lookSpeedY;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
        orientation.Rotate(Vector3.up * mouseX);

        // Lock cursor when in play mode
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
/*create a movement script for unity 3D that has a variable jump that i can change the max time jumping 
 * and it checks ground by using layers using a character controller 
 * and it controls the camera allowing me to look up down left and right in the same script and makes the mouse disappear*/
}