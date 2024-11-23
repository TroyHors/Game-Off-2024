using UnityEngine;
using UnityEngine.UI;

public class FishingUI : MonoBehaviour {
    public Button fishingButton;
    public FishingData_SO fishingData;

    void Start() {
        fishingButton.onClick.AddListener( () => fishingData.Fishing() );
        InventoryManager.Instance.fishingUI.RefreshUI();
    }
}
