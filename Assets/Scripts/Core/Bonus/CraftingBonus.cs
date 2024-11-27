using UnityEngine;

[CreateAssetMenu( fileName = "Carft+" , menuName = "Item Effects/Pick/Craft+" )]
public class CraftingBonus : ItemBonusEffect {
    public int chance = 20;

    public override void ApplyEffect() {
        CraftingData_SO.Instance.noConsumptionChance = chance;

    }

    public override void RemoveEffect() {
        CraftingData_SO.Instance.noConsumptionChance = 0;

    }
}
