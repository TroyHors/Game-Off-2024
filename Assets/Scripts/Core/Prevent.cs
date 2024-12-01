using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prevent : MonoBehaviour {
    private static HashSet<string> existingObjects = new HashSet<string>(); // 用于存储已存在的对象名称

    private void Awake() {
        string uniqueID = gameObject.name; // 使用对象的名字作为唯一标识

        if (existingObjects.Contains( uniqueID )) {
            Destroy( gameObject ); // 如果该对象已经存在，则销毁当前重复的对象
        } else {
            existingObjects.Add( uniqueID ); // 如果对象不存在，则将其加入集合
            DontDestroyOnLoad( gameObject ); // 保持跨场景不销毁
        }
    }
}
