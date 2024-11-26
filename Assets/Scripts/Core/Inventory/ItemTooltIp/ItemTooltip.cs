using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemInfoText;

    public void SetUpTooltip(ItemData_SO item) {
        itemNameText.text = item.name;
        itemInfoText.text = item.description;
    }

}
