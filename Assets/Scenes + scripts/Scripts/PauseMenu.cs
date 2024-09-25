using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject HUD;




    private bool isPaused = false;
    private bool isMapOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }


    }
    public void Resume()
    {
        Time.timeScale = 1;
        GetComponent<Canvas>().enabled = false;
        Cursor.visible = false;
        HUD.SetActive(true);
    }
    public void Pause()
    {
        Time.timeScale = 0;
        GetComponent<Canvas>().enabled = true;
        Cursor.visible = true;
        HUD.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Options()
    {
        transform.GetChild(1).GetComponent<Canvas>().enabled = true;
        transform.GetChild(0).GetComponent<Canvas>().enabled = false;
    }

    public void OptionsBack()
    {
        transform.GetChild(0).GetComponent<Canvas>().enabled = true;
        transform.GetChild(1).GetComponent<Canvas>().enabled = false;
    }

    public void SensChange()
    {
        float value = transform.GetChild(1).GetComponentInChildren<Slider>().value;
        Debug.Log(value);
        PlayerControls walkControls = FindObjectOfType<PlayerControls>();
        if (walkControls != null)
        {
            walkControls.SetCameraSensitivity((int)Mathf.Round(value * 100));
        } 
        else
        {
            FindObjectOfType<DriveControls>().SetCameraSensitivity((int)Mathf.Round(value * 100));
        }
    }


    public void Quit()
    {
        Application.Quit();
    }
}
