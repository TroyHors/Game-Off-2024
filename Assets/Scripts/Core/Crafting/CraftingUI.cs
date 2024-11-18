// CraftingUI.cs
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour {
    public GameObject inventoryPanel; // 背包面板
    public Transform inventoryParent; // 背包物品父对象
    public GameObject inventorySlotPrefab; // 背包槽位预制体

    public Transform craftingArea; // 合成区域父对象

    public Button craftButton; // 合成按钮
    public Button closeButton; // 关闭按钮

    private Inventory inventory;
    private CraftingSystem craftingSystem;

    private List<Item> placedItems = new List<Item>();

    void Start() {
        inventory = Inventory.instance;
        craftingSystem = FindObjectOfType<CraftingSystem>();

        craftButton.onClick.AddListener( OnCraft );
        closeButton.onClick.AddListener( CloseUI );

        PopulateInventory();

        // 订阅背包变化
        inventory.onItemChangedCallback += PopulateInventory;
    }

    private void PopulateInventory() {
        // 清空当前显示的背包物品
        foreach (Transform child in inventoryParent) {
            Destroy( child.gameObject );
        }

        // 重新显示背包物品
        foreach (Item item in inventory.items) {
            GameObject slot = Instantiate( inventorySlotPrefab , inventoryParent );
            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
            inventorySlot.AddItem( item , 1 ); // 假设数量为1
            // 添加拖拽功能
            DragHandler dragHandler = slot.AddComponent<DragHandler>();
            dragHandler.craftingUI = this;
        }
    }

    public void PlaceItem( Item item ) {
        placedItems.Add( item );
        // 显示在合成区域（可以通过实例化图标等方式实现）
        // 这里简化为不显示
    }

    private void OnCraft() {
        if (craftingSystem.Craft( placedItems , out Item result )) {
            // 移除合成所需物品
            foreach (Item item in placedItems) {
                inventory.RemoveItem( item );
            }

            // 添加合成结果物品
            inventory.Add( result );

            // 清空合成区域
            placedItems.Clear();

            Debug.Log( "Crafted: " + result.itemName );
        } else {
            Debug.Log( "Crafting failed. Invalid recipe." );
        }
    }

    public void CloseUI() {
        gameObject.SetActive( false );
        Time.timeScale = 1f;
    }
}
