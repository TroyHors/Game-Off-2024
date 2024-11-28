using UnityEngine;
using UnityEngine.UI;

public class TeleportButton : MonoBehaviour {
    public string teleportID;   // 对应的传送点 ID

    private Button button;

    private void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener( () => {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null) {
                playerController.TeleportTo( teleportID );
                playerController.panel.SetActive( false );
                playerController.Teleports.SetActive( false );
            }
        } );
    }
}
