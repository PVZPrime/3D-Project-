using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NewMovment
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed;

        public float GroundDrag;
        public float AirDrag;

        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        bool readyToJump;

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

        void Start()
        {
            _input = GetComponent<StarterAssetsInputs>();
            rb = GetComponent<Rigidbody>();
            rb.freezeRotation = true; 
            readyToJump = true;
        }
        private void Update()
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + .2f, whatIsGround); 
            MyInput();
            SpeedControl();
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