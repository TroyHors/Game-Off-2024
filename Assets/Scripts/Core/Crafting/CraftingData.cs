using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New CraftingInventory" , menuName = "Inventory/Crafting Data" )]
public class CraftingData_SO : ScriptableObject
{
    public List<CraftingItem> items = new List<CraftingItem>();

    [System.Serializable]
    public class CraftingItem {
        public ItemData_SO itemData;
        public int amount;
    }
}
