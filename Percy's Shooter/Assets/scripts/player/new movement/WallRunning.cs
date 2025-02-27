using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NewMovment;
using StarterAssets;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask WhatIsWall;
    public LayerMask WhatIsGround;
    public float WallRunForce;
    public float WallJumpUpForce;
    public float WallJumpSideForce;
    public float climbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    private bool upwardsRunning;
    private bool downwardsrunning;
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float WallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft;
    private bool wallRight;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    private PlayerMovement pm;
    private Rigidbody rb;
    private StarterAssetsInputs _input;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        CheckForWall();
        StateMachine();
    }
    void FixedUpdate()
    {
        if(pm.wallrunning)
            WallRunningMovement();
    }
    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallHit, WallCheckDistance, WhatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallHit, WallCheckDistance, WhatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, WhatIsGround);
    }

    private void StateMachine()
    {
        horizontalInput = _input.move.x;
        verticalInput = _input.move.y;

        upwardsRunning = _input.sprint;
        downwardsrunning = _input.crouch;

        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround() && !exitingWall)
        {
            if(!pm.wallrunning)
                StartWallRun();

            if(wallRunTimer > 0)
                wallRunTimer -= Time.deltaTime;
             
            if(wallRunTimer <= 0 && pm.wallrunning)
            {
                exitingWall = true;
                exitWallTimer = exitWallTime;
            }

            if(_input.jump) WallJump();
        }

        else if (exitingWall)
        {
            if(pm.wallrunning)
                StopWallRun();

            if(exitWallTimer > 0)
                exitWallTimer -= Time.deltaTime;
            if(exitWallTimer <= 0)
                exitingWall = false;
        }

        else
        {
            if (pm.wallrunning)
            StopWallRun();
        }
    }
    private void StartWallRun()
    {
        pm.wallrunning = true;
        
        wallRunTimer = maxWallRunTime;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }
    private void WallRunningMovement()
    {
        rb.useGravity = useGravity;

        Vector3 wallNormal = wallRight ? rightWallHit.normal : leftWallHit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        rb.AddForce(wallForward * WallRunForce, ForceMode.Force);

        if(upwardsRunning)
            rb.velocity = new Vector3 (rb.velocity.x, climbSpeed, rb.velocity.z);
        if(downwardsrunning)
            rb.velocity = new Vector3(rb.velocity.x, -climbSpeed, rb.velocity.z);

        if (!(wallLeft && horizontalInput > 0 || !(wallRight && horizontalInput < 0)))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);

        if (useGravity)
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
    }
    private void StopWallRun()
    {
        pm.wallrunning = false;
    }

    private void WallJump()
    {
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallHit.normal: leftWallHit.normal;

        Vector3 forceToApply = transform.up * WallJumpUpForce + wallNormal * WallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce (forceToApply, ForceMode.Impulse);
    }
}
