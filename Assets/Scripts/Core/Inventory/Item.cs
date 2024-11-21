using Unity.VisualScripting;
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
    public Bonus bonus;
    [TextArea]
    public string description = "";

    public void TriggerBonus( InventoryData_SO inventory ) {
        if (inventory == InventoryManager.Instance.equipmentData) {
            Debug.Log( inventory + "sdfa" );
        } else {
            Debug.Log( 2 );
        }
    }

}
