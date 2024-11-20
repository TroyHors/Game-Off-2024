using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    public Image icon = null;
    public TextMeshProUGUI amount = null;

    public InventoryData_SO Bag { get; set; }
    public CraftingData_SO BagCrafting { get; set; }
    public int Index { get; set; } = -1;

    public void SetupItemUI( ItemData_SO item , int itemAmount ) {
        if (item != null) {
            icon.sprite = item.itemIcon;
            amount.text = itemAmount.ToString();
            icon.gameObject.SetActive( true );
        } else {
            icon.gameObject.SetActive( false );
        }
    }
}
