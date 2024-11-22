using UnityEngine;

public abstract class ItemBonusEffect : ScriptableObject {
    // 当物品被放置到指定 Inventory 时触发
    public abstract void ApplyEffect();

    // 当物品被移出指定 Inventory 时触发
    public abstract void RemoveEffect();
}
