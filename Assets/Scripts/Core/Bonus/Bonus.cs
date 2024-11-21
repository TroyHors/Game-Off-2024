using UnityEngine;

public abstract class Bonus : ScriptableObject {
    // 抽象方法，子类必须实现
    public abstract void ApplyBonus( ItemData_SO item , InventoryData_SO inventory );
}
