using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogsData_SO : ScriptableObject
{
    public List<DialogPiece> dialogPieces = new List<DialogPiece>();
    public Dictionary<string, DialogPiece> dialogIndex = new Dictionary<string, DialogPiece>();

#if UNITY_EDITOR
    private void OnValidate() {
        dialogIndex.Clear();
        foreach(var piece in dialogPieces) {
            if (!dialogIndex.ContainsKey( piece.ID )) {
                dialogIndex.Add( piece.ID, piece );
            }
        }
    }
}
#endif