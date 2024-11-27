using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemInfoText;

    public void SetUpTooltip(ItemData_SO item, InventoryData_SO inventory) {
        int count = 0;
        foreach (var inventoryItem in inventory.items) {
            if (inventoryItem.itemData == item) {
                count += inventoryItem.amount;
            }
        }
        itemNameText.text = item.name + $" * {count}";
        itemInfoText.text = item.description;
    }

}
