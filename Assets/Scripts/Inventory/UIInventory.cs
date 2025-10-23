using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public static UIInventory instance;

    public Transform itemsParent;        // Onde os slots estão no Canvas
    public GameObject inventoryUI;       // Painel do inventário
    public GameObject slotPrefab;        // Prefab de slot (imagem)

    private void Awake()
    {
        instance = this;
    }

    public void UpdateUI()
    {
        // Limpa slots antigos
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        // Cria slots para cada item
        foreach (var item in InventoryManager.instance.items) // <- Atualizado
        {
            GameObject slot = Instantiate(slotPrefab, itemsParent);
            slot.GetComponent<Image>().sprite = item.icon;
        }
    }

    // Mostrar/ocultar inventário
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI != null)
                inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
