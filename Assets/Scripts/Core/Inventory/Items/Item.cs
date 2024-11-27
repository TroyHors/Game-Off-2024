using Unity.VisualScripting;
using UnityEngine;

public enum ItemType { Fish, SP, Bait, Eq }

[CreateAssetMenu( fileName = "New Item" , menuName = "Inventory/Item Data" )]
public class ItemData_SO : ScriptableObject {
    public ItemType itemType;
    public string itemName;
    public Sprite itemIcon;
    public int itemAmount;
    public int maxStack;
    public bool stackable;
    [TextArea]
    public string description = "";

    [Header("Baits")]
    [ Range( 1 , 3 )]
    public int baitsLevel;
    public ItemData_SO targetFish;

    [Header("Bonus")]
    public ItemBonusEffect bonusEffect;

    [Header("Foods")]
    public FoodsData_SO foodsData;


    // 应用特殊效果
    public void ApplyBonusEffect() {
        if (bonusEffect != null) {
            bonusEffect.ApplyEffect();

        }
    }

    // 移除特殊效果
    public void RemoveBonusEffect() {
        if (bonusEffect != null) {
            bonusEffect.RemoveEffect();

        }
    }
}

    


