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
}
