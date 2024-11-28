using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decliner : MonoBehaviour {
    private void Update() {
        if (Input.GetKeyDown( KeyCode.R ))
            PlayerController.Instance.UpdateHunger( -3 );
    }
}

