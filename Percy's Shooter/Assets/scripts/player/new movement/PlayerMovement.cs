using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMovment
{
	//https://www.youtube.com/watch?v=gNt9wBOrQO4
	//https://www.youtube.com/watch?v=WfW0k5qENxM
    //https://www.youtube.com/watch?v=xCxSjgYTw9c&t=197
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        private float moveSpeed;
        public float walkSpeed;
        public float sprintSpeed;

        public float GroundDrag;
        public float AirDrag;
        [Header("Jumping")]
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

        [Header("Crouching")]
        public float crouchSpeed;
        public float crouchYScale;
        private float startYScale;
        bool crouching;

        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask whatIsGround;
        public float maxGroudTime;
        bool grounded;

        public Transform orientation;

        float horizontalInput;
        float verticalInput;

        float timer;

        Vector3 moveDir;

        Rigidbody rb;
        private StarterAssetsInputs _input;
        
        public MovementState state;
        public enum MovementState
        {
            walking,
            sprinting,
            crouching,
            air
        }

        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
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
            else
                rb.drag = AirDrag;
        }

        private void FixedUpdate()
        {
            if (grounded)
                timer += Time.fixedDeltaTime;
            else timer = 0;
            MovePlayer();
            Debug.Log(rb.velocity.magnitude);
        }

        private void MyInput()
        {
            horizontalInput = _input.move.x;
            verticalInput = _input.move.y;

            if(_input.jump && readyToJump && grounded)
            {
                readyToJump = false;
                Jump();

                Invoke(nameof(ResetJump), jumpCooldown);
            }
            if(_input.crouch && !crouching)
            {
                crouching = true;
                transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
                if (grounded)
                rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
            }
            else if(!_input.crouch)
            {
                crouching = false;
                transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            }
        }
        private void StateHandler()
        {
            if (_input.crouch)
            {
                state = MovementState.crouching;
                moveSpeed = crouchSpeed;
            }
            else if (grounded && _input.sprint)
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

            if(grounded)
                rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
            else if(!grounded)
                rb.AddForce(moveDir.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            if(flatVel.magnitude > moveSpeed)
            {
                if (grounded && timer >= maxGroudTime)
                {
                    Vector3 limetedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limetedVel.x, rb.velocity.y, limetedVel.z);
                }
            }
        }
        private void Jump()
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f , rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        private void ResetJump()
        {
            readyToJump = true;
        }
    }
}