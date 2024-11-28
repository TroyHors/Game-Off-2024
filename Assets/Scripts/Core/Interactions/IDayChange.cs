// DayChangeInteractable.cs
using UnityEngine;

public class IDayChange : MonoBehaviour, IInteractable {
    [Header( "Day Change Settings" )]
    public static IDayChange Instance;
    public bool isSlept = false;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        }
    }

public void Interact() {
        if (isSlept) return;
        isSlept = true;
        GameManager.Instance.dayCount ++;
        TeleportManager.Instance.currentTeleportCount = 0;
    }
}
