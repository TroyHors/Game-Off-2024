using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPicking : MonoBehaviour, IInteractable {
    public static IPicking instance;
    public ItemData_SO itemData;
    public int amount;
    public bool isPicking = false;

    private void Awake() {
        instance = this;
    }
    public void Interact() {
        if (isPicking) return;
        isPicking = true;
        if (itemData != null) {
            if (InventoryManager.Instance != null) {
                InventoryManager.Instance.inventoryData.AddItem( itemData , 5+amount );
                PlayerController.Instance.UpdateHunger( -3 );
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
