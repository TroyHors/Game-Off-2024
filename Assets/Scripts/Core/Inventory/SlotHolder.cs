using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { BAG, CRAFT, RESULT_C, BLEND, RESULT_B, EQUIPMENT, BAITS }

public class SlotHolder : MonoBehaviour, IPointerClickHandler {
    public SlotType slotType;
    public ItemUI itemUI;

    public void OnPointerClick( PointerEventData eventData ) {
        if (eventData.clickCount >= 2) {
            UseItem();
        }
     }

    public void UseItem() {
        if (itemUI != null) {
            if (itemUI.GetItem().itemType == ItemType.Fish || itemUI.GetItem().itemType == ItemType.Bait) {
                if (itemUI.Bag.items[itemUI.Index].amount > 0 && PlayerController.Instance.currentHunger < PlayerController.Instance.maxHunger) {
                    PlayerController.Instance.UpdateHunger( itemUI.GetItem().foodsData.hunger );
                    PlayerController.Instance.UpdateMutation( itemUI.GetItem().foodsData.mutation );
                    itemUI.Bag.items[ itemUI.Index ].amount -= 1;
                    if(itemUI.Bag.items[ itemUI.Index ].amount <= 0) {
                        itemUI.Bag.items[ itemUI.Index ].itemData = null;
                    }
                }
            }
        }
        UpdateItem();
    }

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
            case SlotType.BAITS:
            itemUI.Bag = InventoryManager.Instance.fishingData;
            break;

        }

        var item = itemUI.Bag.items[ itemUI.Index ];
        itemUI.SetupItemUI( item.itemData , item.amount );
     }
}

