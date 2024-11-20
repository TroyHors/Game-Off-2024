//// CraftingUI.cs
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CraftingUI : MonoBehaviour, IDropHandlerCustom {
//    public Transform inventoryParent; // 背包物品父对象
//    public GameObject inventorySlotPrefab; // 背包槽位预制体

//    public Transform craftingArea; // 合成区域父对象
//    public Button craftButton; // 合成按钮

//    private Inventory inventory;
//    private CraftingSystem craftingSystem;

//    private List<Item> placedItems = new List<Item>();
//    private List<InventorySlot> craftingPlacedSlots = new List<InventorySlot>(); // 存储合成区域中的物品槽位

//    void Start() {
//        inventory = Inventory.instance;
//        craftingSystem = FindObjectOfType<CraftingSystem>();

//        craftButton.onClick.AddListener( OnCraft );

//        PopulateInventory();

//        // 订阅背包变化
//        inventory.onItemChangedCallback += PopulateInventory;

//        // 设置合成区域为拖放目标
//        CanvasGroup craftingCanvasGroup = craftingArea.GetComponent<CanvasGroup>();
//        if (craftingCanvasGroup == null) {
//            craftingCanvasGroup = craftingArea.gameObject.AddComponent<CanvasGroup>();
//        }
//        craftingCanvasGroup.blocksRaycasts = true;
//    }

//    private void PopulateInventory() {
//        // 清空当前显示的背包物品
//        foreach (Transform child in inventoryParent) {
//            Destroy( child.gameObject );
//        }

//        // 重新显示背包物品
//        foreach (Item item in inventory.items) {
//            GameObject slot = Instantiate( inventorySlotPrefab , inventoryParent );
//            InventorySlot inventorySlot = slot.GetComponent<InventorySlot>();
//            int itemCount = inventory.GetItemCount( item );
//            inventorySlot.AddItem( item , itemCount );
//        }
//    }

//    public void PlaceItem( InventorySlot droppedSlot ) {
//        if (droppedSlot.GetItem() == null)
//            return;

//        Item item = droppedSlot.GetItem();
//        placedItems.Add( item );

//        // 在合成区域中实例化一个物品槽位
//        GameObject placedObject = Instantiate( inventorySlotPrefab , craftingArea );
//        InventorySlot placedSlot = placedObject.GetComponent<InventorySlot>();
//        placedSlot.AddItem( item , 1 ); // 假设数量为1
//        craftingPlacedSlots.Add( placedSlot );

//        // 从背包中移除物品
//        inventory.RemoveItem( item );
//    }

//    public void OnDropItem( InventorySlot droppedSlot ) {
//        // 将物品放入合成区域
//        PlaceItem( droppedSlot );
//    }

//    private void OnCraft() {
//        if (craftingSystem.Craft( placedItems , out Item result )) {
//            // 清空合成区域中的物品显示
//            foreach (InventorySlot slot in craftingPlacedSlots) {
//                Destroy( slot.gameObject );
//            }
//            craftingPlacedSlots.Clear();
//            placedItems.Clear();

//            // 将合成结果物品放入合成区域
//            if (result != null) {
//                // 创建一个新的物品槽位显示合成结果
//                GameObject resultObject = Instantiate( inventorySlotPrefab , craftingArea );
//                InventorySlot resultSlot = resultObject.GetComponent<InventorySlot>();
//                resultSlot.AddItem( result , 1 ); // 假设数量为1
//                craftingPlacedSlots.Add( resultSlot );
//            }

//            Debug.Log( "Crafted: " + result.itemName );
//        } else {
//            Debug.Log( "Crafting failed. Invalid recipe." );
//        }
//    }
//}
