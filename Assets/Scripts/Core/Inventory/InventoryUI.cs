// InventoryUI.cs
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {
    public GameObject inventoryPanel;      // 背包面板
    public Transform itemsParent;          // 物品槽位的父对象
    public GameObject inventorySlotPrefab; // 物品槽位预制体

    Inventory inventory;                    // 背包管理器
    InventorySlot[] slots;                  // 物品槽位数组

    void Start() {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;

        // 初始化槽位
        slots = new InventorySlot[ inventory.space ];
        for (int i = 0 ; i < inventory.space ; i++) {
            GameObject slotObj = Instantiate( inventorySlotPrefab , itemsParent );
            slots[ i ] = slotObj.GetComponent<InventorySlot>();
        }

        UpdateUI();
    }

    void Update() {
        // 按下I键切换背包显示
        if (Input.GetKeyDown( KeyCode.B )) {
            inventoryPanel.SetActive( !inventoryPanel.activeSelf );
        }
    }

    // 更新背包UI
    void UpdateUI() {
        for (int i = 0 ; i < slots.Length ; i++) {
            if (i < inventory.items.Count) {
                slots[ i ].AddItem( inventory.items[ i ] , 1 ); // 假设每个物品数量为1
            } else {
                slots[ i ].ClearSlot();
            }
        }
    }
}
