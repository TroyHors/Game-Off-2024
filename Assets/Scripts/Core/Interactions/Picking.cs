using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour, IInteractable {
    public ItemData_SO itemData;
    public int amount = 5;
    public bool isPicking = false;
    public void Interact() {
        if (isPicking) return;
        isPicking = true;
        if (itemData != null) {
            if (InventoryManager.Instance != null) {
                InventoryManager.Instance.inventoryData.AddItem( itemData , amount );
                InventoryManager.Instance.inventoryUI.RefreshUI();
                InventoryManager.Instance.craftingUI.RefreshUI();
            } else {
                Debug.LogError( "InventoryManager.Instance is null in Update!" );
            }
        } else {
            Debug.Log( "itemData is null!" );
        }
    }

}
