using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneBehavior : MonoBehaviour
{
    [SerializeField] private float _dps;
    private PlayerControls _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerControls>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _player.TakeDamage(_dps * Time.deltaTime);
        }
    }
}
