using UnityEngine;

[CreateAssetMenu( fileName = "Teleport+" , menuName = "Item Effects/Mobility/Teleport+" )]
public class TeleportBonus : ItemBonusEffect {
    public int maxCount;

    public override void ApplyEffect() {
        TeleportManager.Instance.available = true;
        TeleportManager.Instance.maxTeleportCount = maxCount;
    }

    public override void RemoveEffect() {
        TeleportManager.Instance.available = false;
        TeleportManager.Instance.maxTeleportCount = 5;
    }
}
