using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : Singleton<DialogUI> {
    [Header( "Basic Elements" )]
    public Image icon;
    public TextMeshProUGUI mainText;
    public GameObject dialogPanel;
    public GameObject PlayerUI;
    public bool isTalking = false;

    [Header( "Options" )]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;

    [Header( "Data" )]
    public DialogsData_SO currentData;
    public string currentPieceID;

    private void Update() {
        if (isTalking && Input.GetMouseButtonDown( 0 )) {
            AdvanceDialog();
        }
    }

    public void UpdateDialogData( DialogsData_SO data ) {
        currentData = data;
        if (data.dialogPieces.Count > 0) {
            currentPieceID = data.dialogPieces[ 0 ].ID; // 初始对话
        }
    }

    public void UpdateMainDialog( DialogPiece piece ) {
        isTalking = true;
        dialogPanel.SetActive( isTalking );
        PlayerUI.SetActive( !isTalking );

        if (piece.image != null) {
            icon.enabled = true;
            icon.sprite = piece.image;
        } else {
            icon.enabled = false;
        }

        mainText.text = piece.text;
        CreateOptions( piece );
    }

    public void AdvanceDialog() {
        if (currentData == null || string.IsNullOrEmpty( currentPieceID )) {
            EndDialog();
            return;
        }

        var currentPiece = currentData.dialogIndex[ currentPieceID ];

        if (currentPiece.options.Count > 0) {
            return; // 如果有选项，等待玩家选择
        }

        if (!string.IsNullOrEmpty( currentPiece.targetID ) && currentData.dialogIndex.ContainsKey( currentPiece.targetID )) {
            UpdateMainDialog( currentData.dialogIndex[ currentPiece.targetID ] );
            currentPieceID = currentPiece.targetID;
        } else {
            EndDialog();
        }
    }

    public  void EndDialog() {
        isTalking = false;
        dialogPanel.SetActive( isTalking );
        PlayerUI.SetActive( !isTalking );
    }

    private void CreateOptions( DialogPiece piece ) {
        if (optionPanel.childCount > 0) {
            for (int i = 0 ; i < optionPanel.childCount ; i++) {
                Destroy( optionPanel.GetChild( i ).gameObject );
            }
        }

        foreach (var option in piece.options) {
            var optionUI = Instantiate( optionPrefab , optionPanel );
            optionUI.UpdateOption( piece , option );
        }
    }
}
