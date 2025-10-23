using UnityEngine;

public class Door1 : MonoBehaviour
{
    public Item requiredItem; // Arraste o ScriptableObject da chave aqui

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Door trigger detected by: " + other.name);

        if (other.CompareTag("Player"))
        {
            if (InventoryManager.instance != null && requiredItem != null)
            {
                if (InventoryManager.instance.items.Contains(requiredItem))
                {
                    Debug.Log("Door opened!");
                    Destroy(gameObject); // Remove a porta
                }
                else
                {
                    Debug.Log("You need the key!");
                }
            }
            else
            {
                Debug.LogWarning("InventoryManager ou requiredItem não configurado na porta!");
            }
        }
    }
}
