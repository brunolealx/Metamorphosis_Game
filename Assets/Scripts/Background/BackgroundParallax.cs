using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxFactor = 0.5f; // 0 = fundo parado, 1 = acompanha a câmera normalmente

    private Vector3 lastCameraPosition;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(delta.x * parallaxFactor, delta.y * parallaxFactor, 0);
        lastCameraPosition = cameraTransform.position;
    }
}
