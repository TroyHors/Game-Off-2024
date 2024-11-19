// InventoryUI.cs
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {
    public GameObject inventoryPanel; // 背包面板
    public Transform inventoryParent; // 背包物品父对象
    public GameObject inventorySlotPrefab; // 背包槽位预制体

    private Inventory inventory;
    private List<GameObject> instantiatedSlots = new List<GameObject>();

    void Start() {
        inventory = Inventory.instance;
        if (inventory == null) {
            Debug.LogError( "Inventory instance not found!" );
            return;
        }

        // 订阅背包变化事件
        inventory.onItemChangedCallback += UpdateUI;

        UpdateUI();
    }

    void Update() {
        // 切换背包面板显示
        if (Input.GetKeyDown( KeyCode.I )) {
            inventoryPanel.SetActive( !inventoryPanel.activeSelf );
        }
    }

    public void UpdateUI() {
        // 清空当前显示的物品槽位
        foreach (GameObject slot in instantiatedSlots) {
            Destroy( slot );
        }
        instantiatedSlots.Clear();

        // 遍历背包中的物品并实例化槽位
        foreach (Item item in inventory.items) {
            GameObject slotObj = Instantiate( inventorySlotPrefab , inventoryParent );
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            if (slot != null) {
                // 计算物品在背包中的数量
                int itemCount = inventory.GetItemCount( item );

                slot.AddItem( item , itemCount );

                instantiatedSlots.Add( slotObj );
            } else {
                Debug.LogError( "InventorySlot script not found on prefab!" );
            }
        }
    }
}
