// DayChangeInteractable.cs
using UnityEngine;

public class IDayChange : MonoBehaviour, IInteractable {
    [Header( "Day Change Settings" )]
    public static IDayChange Instance;
    public int dayIncrement = 1; // 每次交互增加的天数，默认为1
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
    }
}
