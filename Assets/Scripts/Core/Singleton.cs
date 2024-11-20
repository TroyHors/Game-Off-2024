using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour {
    private static T _instance;

    public static T Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<T>();
                if (_instance == null) {
                    GameObject singletonObject = new GameObject( typeof( T ).Name );
                    _instance = singletonObject.AddComponent<T>();
                    DontDestroyOnLoad( singletonObject ); // 保证对象在场景切换时不会被销毁
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake() {
        if (_instance == null) {
            _instance = this as T;
            DontDestroyOnLoad( gameObject ); // 保证单例在场景切换时不会被销毁
        } else {
            Destroy( gameObject ); // 如果已有实例，销毁多余的实例
        }
    }
}
