using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;  // Nome do item
    public Sprite icon;      // �cone do item
    public int id;           // ID �nico (opcional)
}
