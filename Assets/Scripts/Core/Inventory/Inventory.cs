using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory instance; // 单例模式

    public List<Item> items = new List<Item>();
    public int space = 20; // 背包容量

    public InventoryUI inventoryUI; // 引用InventoryUI
    public GameObject inventorySlotPrefab; // 引用InventorySlot Prefab

    void Awake() {
        if (instance != null) {
            Debug.LogWarning( "More than one instance of Inventory found!" );
            return;
        }
        instance = this;
    }

    public bool Add( Item item ) {
        if (items.Count >= space) {
            Debug.Log( "Not enough room." );
            return false;
        }
        items.Add( item );
        inventoryUI.UpdateUI();
        return true;
    }

    public void Remove( Item item ) {
        items.Remove( item );
        inventoryUI.UpdateUI();
    }

    // 交换两个物品的位置
    public void SwapItems( int index1 , int index2 ) {
        if (index1 < 0 || index1 >= items.Count || index2 < 0 || index2 >= items.Count) {
            Debug.LogError( "Invalid item indices for swapping." );
            return;
        }

        Item temp = items[ index1 ];
        items[ index1 ] = items[ index2 ];
        items[ index2 ] = temp;

        inventoryUI.UpdateUI();
    }
}
