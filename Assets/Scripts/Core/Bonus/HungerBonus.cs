using UnityEngine;

[CreateAssetMenu( fileName = "Number+" , menuName = "Item Effects/Hunger/Nunber+" )]
public class HungerBonus : ItemBonusEffect {
    public int maxBonus = 20;
    public int addBonus = 5;

    public override void ApplyEffect() {
        PlayerController.Instance.addHBonus = addBonus;
        PlayerController.Instance.maxHBonus = maxBonus;
    }

    public override void RemoveEffect() {
        PlayerController.Instance.addHBonus = 0;
        PlayerController.Instance.maxHBonus = 0;
    }
}