using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Crafting Data" , menuName = "Inventory/Crafting Data" )]
public class CraftingData_SO : ScriptableObject {
    public List<InventoryItem> craftingItems = new List<InventoryItem>();

    public void AddItem( ItemData_SO newItemData , int amount ) {
        bool found = false;

        if (newItemData.stackable) {
            foreach (var item in craftingItems) {
                if (item.itemData == newItemData) {
                    item.amount += amount;
                    found = true;
                    break;
                }
            }
        }

        if (!found) {
            foreach (var item in craftingItems) {
                if (item.itemData == null) {
                    item.itemData = newItemData;
                    item.amount = amount;
                    break;
                }
            }
        }
    }

    public void RemoveItem( ItemData_SO itemData , int amount ) {
        for (int i = 0 ; i < craftingItems.Count ; i++) {
            if (craftingItems[ i ].itemData == itemData) {
                craftingItems[ i ].amount -= amount;
                if (craftingItems[ i ].amount <= 0) {
                    craftingItems[ i ].itemData = null;
                    craftingItems[ i ].amount = 0;
                }
                break;
            }
        }
    }
}
