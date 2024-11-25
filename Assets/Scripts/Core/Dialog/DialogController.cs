using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour, IInteractable
{
    public DialogsData_SO currentData;

    public void Interact() {
        if (currentData != null) {
            OpenDialog();
        } else {
        }
    }

    public void OpenDialog() {
        DialogUI.Instance.UpdateDialogData(currentData);
        DialogUI.Instance.UpdateMainDialog( currentData.dialogPieces[ 0 ] );
    }
}
