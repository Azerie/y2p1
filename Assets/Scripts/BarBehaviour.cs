using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBehaviour : MonoBehaviour
{
    private PlayerControls player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
    }

    // Update is called once per frame
    void Update()
    {
        float maxBar = transform.parent.GetComponent<RectTransform>().rect.width;
        // Debug.Log(maxBar.ToString() + ((player.GetMaxStamina() - player.GetStamina()) / player.GetMaxStamina()).ToString());
        transform.GetComponent<RectTransform>().offsetMax = new Vector2(-maxBar * (player.GetMaxStamina() - player.GetStamina()) / player.GetMaxStamina(), transform.GetComponent<RectTransform>().offsetMax.y);
    }
}
