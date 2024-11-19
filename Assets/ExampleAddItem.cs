// ExampleAddItem.cs
using UnityEngine;

public class ExampleAddItem : MonoBehaviour {
    public Item itemToAdd1; // 要添加的物品
    public Item itemToAdd2;
    void Update() {
        if (Input.GetKeyDown( KeyCode.R )) {
            Inventory.instance.Add( itemToAdd1 );
            Inventory.instance.Add( itemToAdd2 );
        }
    }
}
