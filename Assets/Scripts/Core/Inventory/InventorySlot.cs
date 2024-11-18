// InventorySlot.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {
    public Image icon;
    public TextMeshProUGUI quantityText;

    private Item currentItem;
    private int quantity;

    public void AddItem( Item newItem , int qty ) {
        currentItem = newItem;
        quantity = qty;
        icon.sprite = currentItem.icon;
        icon.enabled = true;
        quantityText.text = quantity > 1 ? quantity.ToString() : "";
    }

    public void ClearSlot() {
        currentItem = null;
        quantity = 0;
        icon.sprite = null;
        icon.enabled = false;
        quantityText.text = "";
    }

    public Item GetItem() {
        return currentItem;
    }
}
