using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogUI : Singleton<DialogUI> {
    [Header( "Basic Elements" )]
    public Image icon;
    public TextMeshProUGUI mainText;
    public GameObject dialogPanel;
    public GameObject PlayerUI;
    public bool isTalking = false;

    [Header("Options")]
    public RectTransform optionPanel;
    public OptionUI optionPrefab;

    [Header( "Data" )]
    public DialogsData_SO currentData;
    public int currentIndex = 0;

    private void Update() {
        // 检测鼠标左键点击，用于推进对话
        if (isTalking && Input.GetMouseButtonDown( 0 )) {
            AdvanceDialog();
        }
    }

    public void UpdateDialogData( DialogsData_SO data ) {
        currentData = data;
        currentIndex = 0;
    }

    public void UpdateMainDialog( DialogPiece piece ) {
        isTalking = true;
        // 显示对话面板，隐藏玩家UI
        dialogPanel.SetActive( isTalking );
        PlayerUI.SetActive( !isTalking );
        // 显示头像（如果有）
        if (piece.image != null) {
            icon.enabled = true;
            icon.sprite = piece.image;
        } else {
            icon.enabled = false;
        }

        // 设置对话文本
        mainText.text = piece.text;
        CreateOptions( piece );
    }

    public void AdvanceDialog() {
        // 当前对话段
        DialogPiece currentPiece = currentData.dialogPieces[ currentIndex ];

        // 判断当前对话是否有选项
        if ( currentPiece.options.Count > 0) {
            return; // 存在选项时，不自动推进
        }

        // 判断是否还有下一段对话
        if (currentIndex + 1 < currentData.dialogPieces.Count) {
            DialogPiece nextPiece = currentData.dialogPieces[ currentIndex ];
            UpdateMainDialog( nextPiece );
            currentIndex++;
        } else {
            // 没有更多对话，结束对话
            isTalking = false; // 对话结束
            dialogPanel.SetActive( isTalking ); // 关闭对话UI
            PlayerUI.SetActive( !isTalking ); // 恢复玩家UI
        }
    }

    void CreateOptions( DialogPiece piece ) { 
        if( optionPanel.childCount > 0) {
            for (int i = 0 ; i < optionPanel.childCount ; i++) { 
                Destroy(optionPanel.GetChild(i).gameObject);
            }
        }

        for(int i = 0 ;i < piece.options.Count; i++) {
            var option = Instantiate( optionPrefab, optionPanel );
            option.UpdateOption(piece,piece.options[i]);   
        }
    }
}
