using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform; // A c�mera que o fundo vai seguir

    private Vector3 offset;

    void Start()
    {
        // Calcula a diferen�a inicial entre o fundo e a c�mera
        offset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        // Atualiza a posi��o do fundo de acordo com a c�mera
        transform.position = cameraTransform.position + offset;
    }
}
