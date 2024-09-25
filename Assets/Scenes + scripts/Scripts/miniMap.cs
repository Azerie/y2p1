using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class miniMap : MonoBehaviour
{
    public GameObject MiniMap;
    public GameObject HUD;

    private bool isMapOpen = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMinimap();
        }
    }

    void ToggleMinimap()
    {
        isMapOpen = !isMapOpen;
        MiniMap.SetActive(isMapOpen);

        if (isMapOpen)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            HUD.SetActive(false);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            HUD.SetActive(true);
        }
    }
}
