using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [Header("Player")]
    public Transform target;

    [Header("Suavização")]
    public float smoothSpeed = 5f;

    [Header("Offset")]
    public Vector3 offset;

    void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;

      
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

      
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}


