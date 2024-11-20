using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adder : MonoBehaviour {
    public ItemData_SO itemData;

    void Start() {
        if (InventoryManager.Instance == null) {
            Debug.LogError( "InventoryManager.Instance is null in Start!" );
        }
    }

    void Update() {
        if (Input.GetKeyDown( KeyCode.R )) {
            if (itemData != null) {
                if (InventoryManager.Instance != null) {
                    InventoryManager.Instance.craftingData.AddItem( itemData , 1 );
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
}
