using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;  // Nome do item
    public Sprite icon;      // Ícone do item
    public int id;           // ID único (opcional)
}
