using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player movement")]
    public Rigidbody p_Rigidbody;
    public float moveSpeed = 3.0f;
    public float maxForce = 1.0f;
    public float movementThreshold = 0.1f;
    private Vector2 move;
    [HideInInspector]
    public Vector3 lastMoveDirection; // Nueva variable para recordar la última dirección

    [Header("Player look")]
    public float rotationSpeed = 10f;

    [Header("Player jump")]
    public float jumpForce = 3.0f;

    [Header("Player Identification")]
    [SerializeField] private int playerID;

    [Header("Raycast")]
    Vector3 rayStart;
    Ray floorRay;
    public Vector3 rayOffset = new Vector3(0, .5f, 0);
    public float rayLength = 0.2f;
    bool rayHit = false;
    public bool bOnGround;

    void Start()
    {
        // Inicializamos lastMoveDirection hacia adelante
        lastMoveDirection = transform.forward;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && bOnGround)
        {
            PlayerJump();
        }
    }

    void FixedUpdate()
    {
        IsGrounded();
        PlayerMovement();
    }

    void PlayerJump()
    {
        p_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void PlayerMovement()
    {
        // Obtiene la dirección de movimiento relativa a la cámara
        Camera playerCamera = Camera.allCameras[playerID];
        Vector3 cameraForward = Vector3.Scale(playerCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(playerCamera.transform.right, new Vector3(1, 0, 1)).normalized;

        // Verifica si hay input real del jugador
        if (move.magnitude >= movementThreshold)
        {
            // Calcula la dirección de movimiento
            Vector3 moveDirection = (cameraForward * move.y + cameraRight * move.x).normalized;

            // Actualiza la última dirección de movimiento
            lastMoveDirection = moveDirection;

            // Calcula la velocidad objetivo usando la dirección relativa a la cámara
            Vector3 targetVelocity = moveDirection * moveSpeed;

            // Calcula el cambio de velocidad
            Vector3 velocityChange = (targetVelocity - p_Rigidbody.linearVelocity);
            velocityChange.y = 0f; // Mantiene la gravedad funcionando

            // Aplica la fuerza con límite
            velocityChange = Vector3.ClampMagnitude(velocityChange, maxForce);
            p_Rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);

            // Rota el personaje hacia la dirección del movimiento
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lastMoveDirection),
                Time.fixedDeltaTime * rotationSpeed
            );
        }
        else
        {
            // Mantiene la rotación hacia la última dirección conocida
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lastMoveDirection),
                Time.fixedDeltaTime * (rotationSpeed * 0.5f) // Reducimos la velocidad de rotación cuando está quieto
            );

            // Desaceleración suave
            Vector3 currentVelocity = p_Rigidbody.linearVelocity;
            currentVelocity.x = Mathf.Lerp(currentVelocity.x, 0, Time.fixedDeltaTime * moveSpeed);
            currentVelocity.z = Mathf.Lerp(currentVelocity.z, 0, Time.fixedDeltaTime * moveSpeed);
            p_Rigidbody.linearVelocity = currentVelocity;
        }
    }

    void IsGrounded()
    {
        rayStart = transform.position + Vector3.down + rayOffset;
        floorRay = new Ray(rayStart, Vector3.down);
        rayHit = Physics.Raycast(floorRay, rayLength);
        bOnGround = rayHit;

        Debug.DrawRay(transform.position + Vector3.down + (rayOffset), new Vector3(0f, -rayLength, 0f), Color.yellow);
    }
}