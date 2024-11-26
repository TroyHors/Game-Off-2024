using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : Singleton<InventoryManager> {
    public class DragData {
        public SlotHolder originalHolder;
        public RectTransform originalParent;
    }
    // TODO: 最后添加模版用于保存数据
    [Header( "Inventory Data" )]
    public InventoryData_SO inventoryData;
    public InventoryData_SO equipmentData;
    public InventoryData_SO resultData_C;
    public InventoryData_SO resultData_B;
    public InventoryData_SO recipeInventoryData;
    public InventoryData_SO ingredientInventoryData;
    public InventoryData_SO resultInventoryData;
    public CraftingData_SO craftingData;
    public CraftingData_SO blendingData;
    public FishingData_SO fishingData;
    [Header( "Containers" )]
    public ContainerUI inventoryUI;
    public ContainerUI equipmentUI;
    public ContainerUI craftingUI;
    public ContainerUI blendingUI;
    public ContainerUI resultUI_C;
    public ContainerUI resultUI_B;
    public ContainerUI fishingUI;
    public ContainerUI recipeInventoryUI; // 配方物品显示的 Inventory
    public ContainerUI ingredientInventoryUI; // 配方详情显示的 Inventory
    public ContainerUI resultInventoryUI; // 配方详情显示的 Inventory
    [Header( "Drag Canvas" )]
    public Canvas dragCanvas;
    public DragData currentDrag;

    [Header("Tooltips")]
    public ItemTooltip tooltip;
    public Transform tooltipTransform;
    public RectTransform tooltipRectTransform;
    void Start() {
        inventoryUI.RefreshUI();
        craftingUI.RefreshUI();
        blendingUI.RefreshUI();
        resultUI_C.RefreshUI();
        resultUI_B.RefreshUI();
        equipmentUI.RefreshUI();    
        fishingUI.RefreshUI();
        recipeInventoryUI.RefreshUI();
        ingredientInventoryUI.RefreshUI(); 
        resultInventoryUI.RefreshUI();

    }
    public bool CheckInInventoryUI( Vector3 position ) {
        for (int i = 0 ; i < inventoryUI.slotHolders.Length ; i++) {
            RectTransform t = inventoryUI.slotHolders[ i ].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint( t , position )) {
                return true;
            }
        }

        return false;
    }

    public bool CheckInCraftingUI( Vector3 position ) {
        for (int i = 0 ; i < craftingUI.slotHolders.Length ; i++) {
            RectTransform t = craftingUI.slotHolders[ i ].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint( t , position )) {
                return true;
            }
        }

        return false;
    }
    public bool CheckInblendingUI( Vector3 position ) {
        for (int i = 0 ; i < blendingUI.slotHolders.Length ; i++) {
            RectTransform t = blendingUI.slotHolders[ i ].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint( t , position )) {
                return true;
            }
        }

        return false;
    }
    public bool CheckInequipmentUI( Vector3 position ) {
        for (int i = 0 ; i < equipmentUI.slotHolders.Length ; i++) {
            RectTransform t = equipmentUI.slotHolders[ i ].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint( t , position )) {
                return true;
            }
        }

        return false;
    }
    public bool CheckInfishingUI( Vector3 position ) {
        for (int i = 0 ; i < fishingUI.slotHolders.Length ; i++) {
            RectTransform t = fishingUI.slotHolders[ i ].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint( t , position )) {
                return true;
            }
        }

        return false;
    }



}
