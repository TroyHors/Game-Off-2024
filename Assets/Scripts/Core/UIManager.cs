using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject PlayerUI;
    public GameObject Teleport;
    public GameObject inventory;

    void Start()
    {
        PlayerUI.SetActive(true);
        Teleport.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (StoryManager.Instance.isStoryActive) {
            Teleport.SetActive(false);
            inventory.SetActive(false);

        }
        if (inventory.activeSelf) {
            Teleport.SetActive( false );
            
        }


    }
}
