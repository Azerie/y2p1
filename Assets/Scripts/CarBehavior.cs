using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarBehavior : MonoBehaviour
{
    [Tooltip("Name of the scene that loads after picking up enough animals")]
    [SerializeField] string SceneToLoad = "Main Menu";
    [Tooltip("Number of animals required to load next scene")]
    [SerializeField] int AnimalsToProgress = 3;

    public void CheckAnimalsNumber()
    {
        if (transform.childCount >= AnimalsToProgress) {
            SceneManager.LoadScene(SceneToLoad);
        }
    }
}
