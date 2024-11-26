using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {
    public static ItemUI Instance;
    public Image icon = null;
    public TextMeshProUGUI amount = null;

    public InventoryData_SO Bag { get; set; }
    public int Index { get; set; } = -1;

    private void Awake() {
        Instance = this;
    }
    public void SetupItemUI( ItemData_SO item , int itemAmount ) {
        if (item != null) {
            icon.sprite = item.itemIcon;
            amount.text = itemAmount.ToString();
            icon.gameObject.SetActive( true );
        } else {
            icon.gameObject.SetActive( false );
        }
    }

    public ItemData_SO GetItem() {
        if(Bag.items[ Index ].itemData != null) {
            return Bag.items[ Index ].itemData;
        }
        return null;
    }
}
