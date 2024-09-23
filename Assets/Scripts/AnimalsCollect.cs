using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsCollect : MonoBehaviour
{
    public TextMeshProUGUI animalsSavedText; 
    private int animalsSavedCount = 0;  

  
    public void AnimalSaved()
    {
        animalsSavedCount += 1;  
        UpdateHUD(); 
    }

 
    private void UpdateHUD()
    {
        animalsSavedText.text = "Animals saved: " + animalsSavedCount;  
    }
}
