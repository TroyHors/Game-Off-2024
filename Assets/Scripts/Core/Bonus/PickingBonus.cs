using UnityEngine;

[CreateAssetMenu( fileName = "Pick+" , menuName = "Item Effects/Pick/Pick+" )]
public class PickingBonus : ItemBonusEffect {
    public int amountBonus = 10;

    public override void ApplyEffect() {
        IPicking.instance.amount = amountBonus;

    }

    public override void RemoveEffect() {
        IPicking.instance.amount = 0;

    }
}
