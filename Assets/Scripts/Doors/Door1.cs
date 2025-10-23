using UnityEngine;

public class Door1 : MonoBehaviour
{
    public Item requiredItem; // Arraste o ScriptableObject da chave aqui
    public PlayerController player; // Arraste o Player aqui, pra acessar o inventário HUD

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (InventoryManager.instance != null && requiredItem != null)
            {
                if (InventoryManager.instance.items.Contains(requiredItem))
                {
                    Debug.Log("Door opened!");

                    // Remove a chave do inventário
                    InventoryManager.instance.RemoveItem(requiredItem);

                    // Esconde o ícone da chave no HUD
                    if (player != null)
                        player.inventoryIcon.SetActive(false);

                    // Remove a porta
                    Destroy(gameObject);
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
