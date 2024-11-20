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
    public CraftingData_SO craftingData;
    [Header( "Containers" )]
    public ContainerUI inventoryUI;
    public CraftingUI craftingUI;
    [Header( "Drag Canvas" )]
    public Canvas dragCanvas;
    public DragData currentDrag;
    void Start() {
        inventoryUI.RefreshUI();
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

}
