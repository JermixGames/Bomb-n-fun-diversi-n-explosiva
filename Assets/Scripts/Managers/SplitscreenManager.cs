using UnityEngine;

public class SplitScreenManager : MonoBehaviour
{
    [Header("Camera References")]
    [SerializeField] private Camera[] playerCameras;
    [SerializeField] private Transform[] players;

    [Header("Camera Settings")]
    [SerializeField] private float cameraHeight = 12f;
    [SerializeField] private float cameraDistance = 8f;
    [SerializeField] private float cameraPitch = 45f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private Vector3 offset;

    private void Start()
    {
        InitializeSplitScreen();
    }

    private void InitializeSplitScreen()
    {
        // Configura la posición y rotación inicial de cada cámara
        foreach (var cam in playerCameras)
        {
            // Rotación inicial de la cámara (estilo Fall Guys)
            cam.transform.rotation = Quaternion.Euler(cameraPitch, 0, 0);
        }
    }

    private void LateUpdate()
    {
        // Actualiza la posición de cada cámara
        for (int i = 0; i < playerCameras.Length; i++)
        {
            if (players[i] != null)
            {
                UpdateCameraPosition(playerCameras[i], players[i]);
            }
        }
    }

    private void UpdateCameraPosition(Camera camera, Transform target)
    {
        // Calcula la posición objetivo de la cámara
        Vector3 desiredPosition = target.position - (Vector3.forward * cameraDistance) + (Vector3.up * cameraHeight) + offset;

        // Suaviza el movimiento de la cámara
        camera.transform.position = Vector3.Lerp(
            camera.transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Asegura que la cámara mantiene su ángulo
        camera.transform.rotation = Quaternion.Euler(cameraPitch, 0, 0);
    }

    // Métodos públicos para ajustar la cámara en tiempo de ejecución
    public void SetCameraHeight(float height)
    {
        cameraHeight = height;
    }

    public void SetCameraDistance(float distance)
    {
        cameraDistance = distance;
    }

    public void SetCameraPitch(float angle)
    {
        cameraPitch = angle;
        foreach (var cam in playerCameras)
        {
            cam.transform.rotation = Quaternion.Euler(angle, 0, 0);
        }
    }
}