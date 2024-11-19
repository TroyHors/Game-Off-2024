// Inventory.cs
using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public static Inventory instance;

    public int space = 20;
    public List<Item> items = new List<Item>();

    public Action onItemChangedCallback;

    void Awake() {
        if (instance != null) {
            Debug.LogWarning( "More than one instance of Inventory found!" );
            return;
        }
        instance = this;
    }

    public bool Add( Item item ) {
        // 检查是否已有该物品且未超过最大堆叠
        foreach (Item invItem in items) {
            if (invItem == item && item.maxStack > 1) {
                // 实现堆叠逻辑
                // 此处假设每次添加一个物品
                onItemChangedCallback?.Invoke();
                return true;
            }
        }

        if (items.Count >= space) {
            Debug.Log( "Not enough room." );
            return false;
        }

        items.Add( item );
        onItemChangedCallback?.Invoke();
        return true;
    }

    public void RemoveItem( Item item ) {
        if (items.Contains( item )) {
            items.Remove( item );
            onItemChangedCallback?.Invoke();
        }
    }

    public int GetItemCount( Item item ) {
        int count = 0;
        foreach (Item invItem in items) {
            if (invItem == item) {
                count++;
            }
        }
        return count;
    }
}
