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
    public float movementThreshold = 0.1f;
    private Vector2 move;

    //PLAYER LOOK
    private Vector2 look;
    public float rotationSpeed = 10f;

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

    public void OnLook(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        IsGrounded();
        PlayerMovement();
        PlayerRotation();
    }  //FUNCTIONAL

    void PlayerJump()
    {
        p_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }  //FUNCTIONAL

    void PlayerMovement()
    {
        // Obtiene la dirección de movimiento relativa a la rotación del personaje
        Vector3 forward = transform.forward * move.y;
        Vector3 right = transform.right * move.x;
        Vector3 moveDirection = (forward + right).normalized;

        // Calcula la velocidad objetivo usando la dirección relativa
        Vector3 targetVelocity = moveDirection * moveSpeed;

        // Calcula el cambio de velocidad
        Vector3 velocityChange = (targetVelocity - p_Rigidbody.linearVelocity);
        velocityChange.y = 0f; // Mantiene la gravedad funcionando

        // Aplica la fuerza
        velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);
        p_Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
    } //FUNCTIONAL

    void PlayerRotation()
    {
        if (look.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(look.x, look.y) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }
    }

    void IsGrounded()
    {
        rayStart = transform.position + Vector3.down + rayOffset;
        floorRay = new Ray(rayStart, Vector3.down);
        rayHit = Physics.Raycast(floorRay, rayLength);
        bOnGround = rayHit;

        Debug.DrawRay(transform.position + Vector3.down + (rayOffset), new Vector3(0f, -rayLength, 0f), Color.yellow);
    }   //FUNCTIONAL
}
