using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "New Dialogue", menuName = "Dialogue/Dialogue Data")]
public class DialogsData_SO : ScriptableObject
{
    public List<DialogPiece> dialogPieces = new List<DialogPiece>();
}
