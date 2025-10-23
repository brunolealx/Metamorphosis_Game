using UnityEngine;
using UnityEngine.InputSystem; // Novo sistema de input

public class InventoryToggle : MonoBehaviour
{
    public GameObject inventoryPanel; // Arraste o painel do inventário aqui

    private void Update()
    {
        // Verifica se a tecla I foi pressionada
        if (Keyboard.current != null && Keyboard.current.iKey.wasPressedThisFrame)
        {
            if (inventoryPanel != null)
            {
                bool isActive = inventoryPanel.activeSelf;
                inventoryPanel.SetActive(!isActive);
                Debug.Log("Inventory toggled: " + !isActive);
            }
        }
    }
}
