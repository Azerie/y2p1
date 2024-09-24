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
    [Tooltip("Array of sound clips the animal makes")]
    [SerializeField] private AudioClip[] _soundArray;
    [Tooltip("Sound clip played on animal pickup")]
    [SerializeField] private AudioClip _pickupSound;

    [Space(10)]
    [Header("Picking up")]
    [Tooltip("Distance at which player holds the animal")]
    [SerializeField] private float _animalHoldingDistance = 1;
    [Tooltip("Height at which player holds the animal")]
    [SerializeField] private float _animalHoldingHeight = 0;

    [Space(10)]
    [Tooltip("Place of the animal relative to the car after placement")]
    [SerializeField] private Vector3 _placementInCar = Vector3.up;

    private AudioSource _voicePlayer;
    private int _timeUntilNextSound;

    private bool _isSafe = false;

    public AnimalsCollect animalsCollect;
    void Start()
    {
        _timeUntilNextSound = Random.Range(MinTimer, MaxTimer);
        _voicePlayer = GetComponent<AudioSource>();

        if (animalsCollect == null)
        {
            animalsCollect = GameObject.FindObjectOfType<AnimalsCollect>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isSafe) {
            _timeUntilNextSound -= Mathf.RoundToInt(Time.deltaTime * 1000);
            if(_timeUntilNextSound <= 0) 
            {
                PlayVoiceline();
                // uncomment after adding sounds
                _timeUntilNextSound = Random.Range(MinTimer, MaxTimer);
            }
        }
    }

    private void PlayVoiceline()
    {
        _voicePlayer.PlayOneShot(_soundArray[Random.Range(0, _soundArray.Length - 1)]);
    }

    public void PlayPickup()
    {
        _voicePlayer.PlayOneShot(_pickupSound);
        _timeUntilNextSound = Random.Range(MinTimer, MaxTimer);
    }

    public float GetHoldingDistance() {
        return _animalHoldingDistance;
    }

    public float GetHoldingHeight() {
        return _animalHoldingHeight;
    }

    public Vector3 GetPlacementInCar() {
        return _placementInCar;
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Car") {
            MoveToCar();
        }
    }

    public void MoveToCar()
    {
        GameObject car = GameObject.FindWithTag("Car");
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Collider>().enabled = false;
        transform.parent = car.transform;
        car.GetComponent<CarBehavior>().CheckAnimalsNumber();
        transform.localPosition = GetComponent<AnimalBehavior>().GetPlacementInCar();

        

        Save();
        if (animalsCollect != null)
        {
            animalsCollect.AnimalSaved();
        }
    }

    public void Save() { _isSafe = true; }

    public void UnSave() { _isSafe = false; }

    public bool IsSafe() { return _isSafe; }
}
