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
        // Configura la posici�n y rotaci�n inicial de cada c�mara
        foreach (var cam in playerCameras)
        {
            // Rotaci�n inicial de la c�mara (estilo Fall Guys)
            cam.transform.rotation = Quaternion.Euler(cameraPitch, 0, 0);
        }
    }

    private void LateUpdate()
    {
        // Actualiza la posici�n de cada c�mara
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
        // Calcula la posici�n objetivo de la c�mara
        Vector3 desiredPosition = target.position - (Vector3.forward * cameraDistance) + (Vector3.up * cameraHeight) + offset;

        // Suaviza el movimiento de la c�mara
        camera.transform.position = Vector3.Lerp(
            camera.transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Asegura que la c�mara mantiene su �ngulo
        camera.transform.rotation = Quaternion.Euler(cameraPitch, 0, 0);
    }

    // M�todos p�blicos para ajustar la c�mara en tiempo de ejecuci�n
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