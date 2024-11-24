using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prevent : MonoBehaviour
{
    public static Prevent Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad( gameObject ); // 保持跨场景不销毁
        } else {
            Destroy( gameObject ); // 避免重复实例
        }
    }
}
