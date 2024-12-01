using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class GameData {
    public static GameData Instance;
    public Vector3 playerPosition; // 玩家位置
    public int dayCount; // 当前天数
    public List<InventoryItem> inventoryItems; // 背包物品
    public Dictionary<string , bool> storyStatus; // 剧情状态
}

public class SaveManager : MonoBehaviour {
    private static string saveFilePath => Application.persistentDataPath + "/savefile.json";

    public static void SaveGame( GameData data ) {
        string jsonData = JsonUtility.ToJson( data , true );
        File.WriteAllText( saveFilePath , jsonData );
        Debug.Log( "Game saved to " + saveFilePath );
    }

    public static GameData LoadGame() {
        if (File.Exists( saveFilePath )) {
            string jsonData = File.ReadAllText( saveFilePath );
            return JsonUtility.FromJson<GameData>( jsonData );
        } else {
            Debug.LogWarning( "No save file found." );
            return null;
        }
    }

    public static void ResetSave() {
        if (File.Exists( saveFilePath )) {
            File.Delete( saveFilePath );
            Debug.Log( "Save file reset." );
        }
    }
}
