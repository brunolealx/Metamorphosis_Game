using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform; // A câmera que o fundo vai seguir

    private Vector3 offset;

    void Start()
    {
        // Calcula a diferença inicial entre o fundo e a câmera
        offset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        // Atualiza a posição do fundo de acordo com a câmera
        transform.position = cameraTransform.position + offset;
    }
}
