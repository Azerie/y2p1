using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBehaviour : MonoBehaviour
{
    [SerializeField] private bool _isStaminaBar;
    private PlayerControls player;
    private float maxBar;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerControls>();
    }

    private void Awake()
    {
        maxBar = transform.parent.GetComponent<RectTransform>().rect.width;
        transform.GetComponent<RectTransform>().offsetMax = new Vector2(0f, transform.GetComponent<RectTransform>().offsetMax.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isStaminaBar) 
        {
            transform.GetComponent<RectTransform>().offsetMax = new Vector2(-maxBar * (player.GetMaxStamina() - player.GetStamina()) / player.GetMaxStamina(), transform.GetComponent<RectTransform>().offsetMax.y);
        }
        else
        {
            transform.GetComponent<RectTransform>().offsetMax = new Vector2(-maxBar * (player.GetMaxHealth() - player.GetHealth()) / player.GetMaxHealth(), transform.GetComponent<RectTransform>().offsetMax.y);
        }
    }
}
