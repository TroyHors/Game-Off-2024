using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public TextMeshProUGUI optionText;
    Button thisButton;
    DialogPiece currentPiece;

    private string nextPieceID;

    private void Awake() {
        thisButton = GetComponent<Button>();
        thisButton.onClick.AddListener( OnOptionClicked );
    }

    public  void UpdateOption(DialogPiece piece, DialogOptions option) {
        currentPiece = piece;
        optionText.text = option.text;
        nextPieceID = option.targetID;
    }

    public void OnOptionClicked() { 
        if(nextPieceID == "") {
            DialogUI.Instance.dialogPanel.SetActive(false);
            DialogUI.Instance.PlayerUI.SetActive(true);
            return;
        } else {
            DialogUI.Instance.UpdateMainDialog( DialogUI.Instance.currentData.dialogIndex[ nextPieceID ] );
            DialogUI.Instance.currentIndex++;
        }
    }
}
