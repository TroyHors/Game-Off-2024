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
    public InventoryData_SO resultData_C;
    public InventoryData_SO resultData_B;
    public CraftingData_SO craftingData;
    public CraftingData_SO blendingData;
    [Header( "Containers" )]
    public ContainerUI inventoryUI;
    public ContainerUI craftingUI;
    public ContainerUI blendingUI;
    public ContainerUI resultUI_C;
    public ContainerUI resultUI_B;
    [Header( "Drag Canvas" )]
    public Canvas dragCanvas;
    public DragData currentDrag;
    void Start() {
        inventoryUI.RefreshUI();
        craftingUI.RefreshUI();
        blendingUI.RefreshUI();
        resultUI_C.RefreshUI();
        resultUI_B.RefreshUI();
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



}
