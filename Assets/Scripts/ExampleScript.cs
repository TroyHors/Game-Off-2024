using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleScript : MonoBehaviour {
    // Both private and public variables should be named in PascalCase and named by its funtion.
    [SerializeField]  // Use SerializeField instead of public variables
    private int exampleValue = 0;
    // Public variables will be exposedpublicly to other scripts.
    public float timer = 0.0f;

    /// Start is called before the first frame update.
    /// Initialize variables or setup references here.
    private void Start() {
        Debug.Log( "ExampleScript initialized." );
    }

    /// Update is called once per frame.
    /// Main logic for frame-by-frame updates should go here.
    private void Update() {
        timer += Time.deltaTime;
        if (timer > 1.0f) {
            PerformAction();
            timer = 0.0f;
        }
    }

    /// Example of a private method.
    /// This method performs a specific action and should include
    /// a brief description of its purpose and functionality.
    private void PerformAction() {
        // Example action logic
        Debug.Log( "Action performed." );
    }
}
