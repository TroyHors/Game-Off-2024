using UnityEngine;

public enum ItemType { Fish, SP, Bait, Eq }

[CreateAssetMenu( fileName = "New Item" , menuName = "Inventory/Item Data" )]
public class ItemData_SO : ScriptableObject {
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;
    public int maxStack;
    public bool stackable;
    [TextArea]
    public string description = "";


}
