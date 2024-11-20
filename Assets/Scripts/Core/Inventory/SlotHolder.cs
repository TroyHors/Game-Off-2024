using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType { BAG, CRAFT, ARMOR, ACTION }

public class SlotHolder : MonoBehaviour {
    public SlotType slotType;
    public ItemUI itemUI;

    public void UpdateItem() {
        switch (slotType) {
            case SlotType.BAG:
            itemUI.Bag = InventoryManager.Instance.inventoryData;
            break;
            case SlotType.CRAFT:
            itemUI.BagCrafting = InventoryManager.Instance.craftingData;
            break;
            case SlotType.ARMOR:
            break;
            case SlotType.ACTION:
            break;
        }

        var item = itemUI.Bag.items[ itemUI.Index ];
        itemUI.SetupItemUI( item.itemData , item.amount );
    }
}
