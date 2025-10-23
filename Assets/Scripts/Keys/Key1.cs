using UnityEngine;
using UnityEngine.UI;

public class Key1 : MonoBehaviour
{
    public Item keyItem;        // ScriptableObject da chave
    public Image keyIconUI;     // HUD

    private void Start()
    {
        if (keyIconUI != null)
            keyIconUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && keyItem != null && InventoryManager.instance != null)
        {
            InventoryManager.instance.AddItem(keyItem);
            Debug.Log("Added to inventory: " + keyItem.itemName);

            if (keyIconUI != null && keyItem.icon != null)
            {
                keyIconUI.sprite = keyItem.icon;
                keyIconUI.gameObject.SetActive(true);
            }

            Destroy(gameObject);
        }
    }
}
