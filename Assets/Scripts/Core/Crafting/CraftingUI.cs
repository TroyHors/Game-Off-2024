using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour {
    public SlotHolder[] craftingSlots;

    public void RefreshUI() {
        for (int i = 0 ; i < craftingSlots.Length ; i++) {
            craftingSlots[ i ].itemUI.Index = i;
            craftingSlots[ i ].UpdateItem();
        }
    }
}
