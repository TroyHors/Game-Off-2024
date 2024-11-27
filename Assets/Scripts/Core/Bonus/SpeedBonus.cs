using UnityEngine;

[CreateAssetMenu( fileName = "Speed+" , menuName = "Item Effects/Mobility/Speed+" )]
public class SpeedBonus: ItemBonusEffect {
    public float fast = 0.1f;
    public float before = 0.2f;

    public override void ApplyEffect() {
        before = PlayerController.Instance.moveDelay;
        PlayerController.Instance.moveDelay = fast;

    }

    public override void RemoveEffect() {
        PlayerController.Instance.moveDelay = before;

    }
}
