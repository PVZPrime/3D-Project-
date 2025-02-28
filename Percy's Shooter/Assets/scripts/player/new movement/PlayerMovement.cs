using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMovment
{
    //https://www.youtube.com/watch?v=WfW0k5qENxM&t=322
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        private float moveSpeed;
        public float walkSpeed;
        public float sprintSpeed;
        public float wallRunSpeed;

        public float GroundDrag;
        public float AirDrag;
        [Header("Jumping")]
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        public bool AirDragActive;
        bool readyToJump;

        [Header("Crouching")]
        public float crouchSpeed;
        public float crouchYScale;
        private float startYScale;
        

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask whatIsGround;
        public float maxGroudTime;
        public bool grounded;

        [Header("Slope Handling")]
        public float maxSlopeAngle;
        private RaycastHit slopeHit;
        private bool ExitingSlope;


        public Transform orientation;

        float horizontalInput;
        float verticalInput;

        float timer;

        Vector3 moveDir;
        Rigidbody rb;
        private StarterAssetsInputs it;
        
        public MovementState state;
        public enum MovementState
        {
            walking,
            sprinting,
            wallrunning,
            crouching,
            air
        }
        public bool crouching;
        public bool wallrunning;

        void Start()
        {
            it = GetComponent<StarterAssetsInputs>();
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true; 
            readyToJump = true;
            startYScale = transform.localScale.y;
            crouching = false;
        }
        private void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + .2f, whatIsGround); 
            MyInput();
            SpeedControl();
            StateHandler();
            if (grounded)
                rb.drag = GroundDrag;
            else if (AirDragActive)
                rb.drag = AirDrag;
            else if (!AirDragActive)
                rb.drag = 0f;
        }

        private void FixedUpdate()
        {
            if (grounded)
                timer += Time.fixedDeltaTime;
            else timer = 0;
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = it.move.x;
            verticalInput = it.move.y;

            if(it.jump && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            if(it.crouch && !crouching && !wallrunning)
            {
                crouching = true;
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
                if (grounded)
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            else if(!it.crouch)
            {
                crouching = false;
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            }
        }
        private void StateHandler()
        {
            if (wallrunning)
            {
                state = MovementState.wallrunning;
                moveSpeed = wallRunSpeed;
            }
            else if (it.crouch)
            {
                state = MovementState.crouching;
                moveSpeed = crouchSpeed;
            }
            else if (grounded && it.sprint)
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }
            else if(grounded)
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }
            else
            {
                state = MovementState.air;
            }
        }

        private void MovePlayer()
        {
            moveDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if(OnSlope() && !ExitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

                if (rb.velocity.y > 0)
                    rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }

            if(grounded)
                rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            else if(!grounded)
                rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);

            if(!wallrunning)
            rb.useGravity = !OnSlope();
        }
        private void SpeedControl()
        {
            if (OnSlope() && !ExitingSlope)
            {
                if(rb.velocity.magnitude > moveSpeed)
                    rb.velocity = rb.velocity.normalized * moveSpeed;
            }
            else
            {
                Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
                if(flatVel.magnitude > moveSpeed)
                {
                    if (grounded && timer >= maxGroudTime && AirDragActive)
                    {
                        Vector3 limetedVel = flatVel.normalized * moveSpeed;
                        rb.velocity = new Vector3(limetedVel.x, rb.velocity.y, limetedVel.z);
                    }
                    else if (!AirDragActive)
                    {
                        Vector3 limetedVel = flatVel.normalized * moveSpeed;
                        rb.velocity = new Vector3(limetedVel.x, rb.velocity.y, limetedVel.z);
                    }
                }

            }

        }
        private void Jump()
        {
            ExitingSlope = true;

            rb.velocity = new Vector3(rb.velocity.x, 0f , rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        private void ResetJump()
        {
            readyToJump = true;

            ExitingSlope = false;
        }

        private bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(moveDir, slopeHit.normal).normalized;
        }







    }
}