using UnityEngine;

public class RotateAndMovePlatform : MonoBehaviour
{
    public float rotationSpeed; 
    public Transform platform; 
    public Vector3 direction; 

    void Update()
    {
        
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);

        
        if (platform != null)
        {
            platform.GetComponent<Rigidbody>().MovePosition(platform.position + direction * Time.deltaTime);
        }
    }
}
