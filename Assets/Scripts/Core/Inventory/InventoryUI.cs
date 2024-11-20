using UnityEngine;

public class InventoryUI : MonoBehaviour {
    public GameObject inventoryPanel; // 背包面板
    public Transform inventoryGrid;    // 直接在Inspector中引用InventoryGrid

    void Start() {
        // 初始时隐藏背包
        inventoryPanel.SetActive( false );
        UpdateUI();
    }

    void Update() {
        // 监听按键 `B` 来切换背包的显示状态
        if (Input.GetKeyDown( KeyCode.B )) {
            inventoryPanel.SetActive( !inventoryPanel.activeSelf );
        }
    }

    public void UpdateUI() {
        if (inventoryGrid == null) {
            Debug.LogError( "InventoryGrid reference is missing in InventoryUI." );
            return;
        }

        // 清空现有格子
        foreach (Transform child in inventoryGrid) {
            Destroy( child.gameObject );
        }

        // 生成格子并填充物品
        for (int i = 0 ; i < Inventory.instance.items.Count ; i++) {
            Item item = Inventory.instance.items[ i ];
            GameObject slot = Instantiate( Inventory.instance.inventorySlotPrefab , inventoryGrid );
            InventorySlot slotComponent = slot.GetComponent<InventorySlot>();
            if (slotComponent != null) {
                slotComponent.Setup( item , i );
            } else {
                Debug.LogError( "InventorySlot component missing on InventorySlot prefab." );
            }
        }
    }
}
