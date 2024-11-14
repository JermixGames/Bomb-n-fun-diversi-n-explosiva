using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //PLAYER MOVEMENT
    public Rigidbody p_Rigidbody;
    public float moveSpeed = 3.0f;
    public float maxForce = 1.0f;
    public float lookRotationSpeed = 10f;
    public float movementThreshold = 0.1f;
    private Vector2 move;
    private Vector3 lastMovementDirection;

    //PLAYER JUMP
    public float jumpForce = 3.0f;


    //RAYCAST
    Vector3 rayStart;
    Ray floorRay;
    public Vector3 rayOffset = new Vector3(0, .5f, 0);
    public float rayLength = 0.2f;
    bool rayHit = false;
    public bool bOnGround;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }  //FUNCTIONAL

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && bOnGround)
        {
            PlayerJump();
        }
    }  // FUNCTIONAL

    void FixedUpdate()
    {
        IsGrounded();
        PlayerMovement();
    }  //FUNCTIONAL

    void PlayerJump()
    {
        p_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }  //FUNCTIONAL

    void PlayerMovement()
    {
        //CALCULATES TARGET VELOCITY
        Vector3 targetVelocity = new Vector3(move.x, 0f, move.y).normalized * moveSpeed;

        //CALCULATES THE VELOCITY CHANGE
        Vector3 velocityChange = (targetVelocity - p_Rigidbody.linearVelocity);
        velocityChange.y = 0f; //MAKES GRAVITY WORK XD 

        //APPLY FORCE
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);
        p_Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

        //HANDLE ROTATION RESPECT ITS MOVEMENT
        if (move.magnitude > movementThreshold)
        {
            lastMovementDirection = new Vector3(move.x, 0f, move.y).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(lastMovementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lookRotationSpeed * Time.fixedDeltaTime);
        }
    }  //FUNCTIONAL

    void IsGrounded()
    {
        rayStart = transform.position + Vector3.down + rayOffset;
        floorRay = new Ray(rayStart, Vector3.down);
        rayHit = Physics.Raycast(floorRay, rayLength);
        bOnGround = rayHit;

        Debug.DrawRay(transform.position + Vector3.down + (rayOffset), new Vector3(0f, -rayLength, 0f), Color.yellow);
    }   //FUNCTIONAL
}
