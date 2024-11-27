using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour {
    public TextMeshProUGUI optionText;
    private Button thisButton;
    private string nextPieceID;

    private void Awake() {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener( OnOptionClicked );
    }

    public void UpdateOption( DialogPiece piece , DialogOptions option ) {
        optionText.text = option.text;
        nextPieceID = option.targetID;
    }

    private void OnOptionClicked() {
        if (string.IsNullOrEmpty( nextPieceID )) {
            DialogUI.Instance.EndDialog();
        } else if (DialogUI.Instance.currentData.dialogIndex.ContainsKey( nextPieceID )) {
            DialogUI.Instance.UpdateMainDialog( DialogUI.Instance.currentData.dialogIndex[ nextPieceID ] );
            DialogUI.Instance.currentPieceID = nextPieceID;
        } else {
            DialogUI.Instance.EndDialog();
        }
    }
}
