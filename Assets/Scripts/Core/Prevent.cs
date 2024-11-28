using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prevent : MonoBehaviour
{


    private void Awake() {
            DontDestroyOnLoad( gameObject ); // 保持跨场景不销毁

    }
}
