using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Personagem que a câmera vai seguir
    public float smoothSpeed = 0.125f; // Suavidade do movimento
    public Vector3 offset; // Deslocamento da câmera em relação ao personagem

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
