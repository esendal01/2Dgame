using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Kameranın takip edeceği hedef (örneğin, karakter)

    public float smoothSpeed = 0.125f; // Takip etme hızı

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}