using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SlotType { BAG, CRAFT, RESULT_C, BLEND, RESULT_B, EQUIPMENT, BAITS, SHOWING }

public class SlotHolder : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public SlotType slotType;
    public ItemUI itemUI;
    public GameObject slots;

    public void OnPointerClick( PointerEventData eventData ) {
        if (slotType == SlotType.SHOWING) {
            ItemData_SO selectedItem = itemUI.GetItem();
            if (selectedItem != null) {
                Recipe matchingRecipe = FindRecipeByResult( selectedItem );
                if (matchingRecipe != null) {
                    FindObjectOfType<RecipeUI>().DisplayRecipeDetails( matchingRecipe );
                }
            }
        }
        if (eventData.clickCount >= 2) {
            UseItem();
        }
     }

    private Recipe FindRecipeByResult( ItemData_SO resultItem ) {
        foreach (var recipe in IShowRecipe.Instance.recipeList) {
            if (recipe.resultItem == resultItem) {
                return recipe;
            }
        }
        return null;
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
            case SlotType.SHOWING:
            itemUI.Bag = InventoryManager.Instance.recipeInventoryData;
            break;

        }

        var item = itemUI.Bag.items[ itemUI.Index ];
        itemUI.SetupItemUI( item.itemData , item.amount );
     }

    public void OnPointerEnter( PointerEventData eventData ) {
        if (itemUI.GetItem() != null) {
            InventoryManager.Instance.tooltip.SetUpTooltip(itemUI.GetItem());
            InventoryManager.Instance.tooltip.gameObject.SetActive(true);
            InventoryManager.Instance.tooltipTransform.position = new Vector3( slots.transform.position.x - 200 , slots.transform.position.y , slots.transform.position.z );          
        }
    }

    public void OnPointerExit( PointerEventData eventData ) {
        InventoryManager.Instance.tooltip.gameObject.SetActive( false );
    }
}

