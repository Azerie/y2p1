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

    [Space(10)]
    [Header("Picking up")]
    [Tooltip("Distance at which player holds the animal")]
    [SerializeField] private float _animalHoldingDistance = 1;
    [Tooltip("Height at which player holds the animal")]
    [SerializeField] private float _animalHoldingHeight = 0;

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

    public float GetHoldingDistance() {
        return _animalHoldingDistance;
    }

    public float GetHoldingHeight() {
        return _animalHoldingHeight;
    }
}
