using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalBehavior : MonoBehaviour
{
    [Space(10)]
    [Header("Intervals between sounds")]
    [Tooltip("Lowest interval in milliseconds")]
    [SerializeField] private int MinTimer;
    [Tooltip("Highest interval in milliseconds")]
    [SerializeField] private int MaxTimer;

    private AudioSource _voicePlayer;
    private int _timeUntilNextSound;
    void Start()
    {
        _timeUntilNextSound = Random.Range(MinTimer, MaxTimer);
        _voicePlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _timeUntilNextSound -= Mathf.RoundToInt(Time.deltaTime * 1000);
        if(_timeUntilNextSound <= 0) 
        {
            _voicePlayer.Play();
            _timeUntilNextSound = Random.Range(MinTimer, MaxTimer);
        }
    }
}
