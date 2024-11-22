using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SlotType { BAG, CRAFT, RESULT_C, BLEND, RESULT_B, EQUIPMENT }

public class SlotHolder : MonoBehaviour {
    public SlotType slotType;
    public ItemUI itemUI;

    public void UpdateItem() {
        switch (slotType) {
            case SlotType.BAG:
            itemUI.Bag = InventoryManager.Instance.inventoryData;
            break;
            case SlotType.CRAFT:
            itemUI.Bag = InventoryManager.Instance.craftingData;
            break;
            case SlotType.RESULT_C:
            itemUI.Bag = InventoryManager.Instance.resultData_C;
            break;
            case SlotType.BLEND:
            itemUI.Bag = InventoryManager.Instance.blendingData;
            break;
            case SlotType.RESULT_B:
            itemUI.Bag = InventoryManager.Instance.resultData_B;
            break;
            case SlotType.EQUIPMENT:
            itemUI.Bag = InventoryManager.Instance.equipmentData;
            break;

        }

        var item = itemUI.Bag.items[ itemUI.Index ];
        itemUI.SetupItemUI( item.itemData , item.amount );
     }
}

