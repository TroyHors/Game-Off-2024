using UnityEngine;

[CreateAssetMenu( fileName = "HChance+" , menuName = "Item Effects/Hunger/HChance+" )]
public class HChanceBonus : ItemBonusEffect {
    public int chance = 20;

    public override void ApplyEffect() {
        PlayerController.Instance.chance = chance;

    }

    public override void RemoveEffect() {
        PlayerController.Instance.chance = 0;
    }
}
