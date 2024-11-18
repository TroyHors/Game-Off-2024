// ExampleAddItem.cs
using UnityEngine;

public class ExampleAddItem : MonoBehaviour {
    public Item itemToAdd; // 要添加的物品

    void Update() {
        if (Input.GetKeyDown( KeyCode.E )) {
            Inventory.instance.Add( itemToAdd );
        }
    }
}
