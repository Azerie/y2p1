using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.InputSystem;

public class DriveControls : MonoBehaviour
{
    [Header("Camera")]
    [Tooltip("Rotation speed of the character")]
    [SerializeField] private float RotationSpeed = 40f;
    [Tooltip("Lower camera constraint")]
    [SerializeField] private int MinCameraAngle = -50;
    [Tooltip("Higher camera constraint")]
    [SerializeField] private int MaxCameraAngle = 50;

    [Header("Car")]
    [SerializeField] private bool _isPossibleToLeave = true;
    [Tooltip("Base motor force")]
    [SerializeField] private float _motorForce = 4000f;
    [Tooltip("Idle break force")]
    [SerializeField] private float _brakeForce = 1000f;
    [SerializeField] private float MaxSteeringAngle = 30;
    [Tooltip("How far a wheel can steer in a second in degrees")]
    [SerializeField] private float SteerChangeRate = 60;
    [SerializeField] private WheelCollider FwdLeftWheel;
    [SerializeField] private WheelCollider FwdRightWheel;
    [SerializeField] private WheelCollider BackLeftWheel;
    [SerializeField] private WheelCollider BackRightWheel;

    [Header("Sound")]
    [SerializeField] private AudioClip carDoor;
    [SerializeField] private AudioClip carDrive;


    private float _rotationVelocity;
    private float _cameraYrotation = 0f;
    private float _currentSteeringAngle = 0;

    private Vector2 moveInput = Vector2.zero;
    private Vector2 lookInput = Vector2.zero;

    private Camera cam;
    private AudioSource _audioSource;

    public GameObject Sign;

    private bool hasInteracted = false;

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Awake() {
        Cursor.visible = false;
    }

    void Update()
    {
        Move();
    }

    private void Move() 
    {
        float torque = Math.Sign(moveInput.y) * _motorForce;
        float breakTorque = 0;
        if (moveInput.y < 0.1f)
        {
            torque = 0;
            breakTorque = _brakeForce;
        }
        else if(!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(carDrive);
        }
        FwdLeftWheel.motorTorque = torque;
        FwdRightWheel.motorTorque = torque;
        FwdLeftWheel.brakeTorque = breakTorque;
        FwdRightWheel.brakeTorque = breakTorque;
        BackLeftWheel.brakeTorque = breakTorque;
        BackRightWheel.brakeTorque = breakTorque;

        float targetSteeringAngle = Math.Sign(moveInput.x) * MaxSteeringAngle;
        if (_currentSteeringAngle < targetSteeringAngle - 0.1f || _currentSteeringAngle > targetSteeringAngle + 0.1f)
        {
            _currentSteeringAngle = Mathf.Lerp(_currentSteeringAngle, targetSteeringAngle, Time.deltaTime * SteerChangeRate);
            _currentSteeringAngle = Mathf.Round(_currentSteeringAngle * 100000f) / 100000f;
        }
        FwdLeftWheel.steerAngle = _currentSteeringAngle;
        FwdRightWheel.steerAngle = _currentSteeringAngle;
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }


    private void OnLook(InputValue value) 
    {
        cam = GetComponentInChildren<Camera>();
        _rotationVelocity = value.Get<Vector2>().x * RotationSpeed * Time.deltaTime;

        // rotate the camera left and right
        cam.transform.parent.Rotate(Vector3.up * _rotationVelocity);

        _cameraYrotation -= value.Get<Vector2>().y * RotationSpeed * Time.deltaTime;
        _cameraYrotation = Math.Clamp(_cameraYrotation, MinCameraAngle, MaxCameraAngle);

        Quaternion newRotation = Quaternion.Euler(_cameraYrotation, 0, 0);

        cam.transform.localRotation = newRotation;
    }

    private void OnPause() {
        Time.timeScale = 0;
        GameObject pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu.transform.GetComponent<Canvas>().enabled = true;
        Cursor.visible = true;
    }

    private void OnInteract()
    {
        if (_isPossibleToLeave)
        {
            GetComponentInChildren<PlayerInput>().enabled = false;
            Transform player = transform.Find("Player");
            player.SetParent(transform.parent);
            player.GetComponent<PlayerInput>().enabled = true;
            player.GetComponentInChildren<CapsuleCollider>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.GetComponentInChildren<Camera>().transform.parent.localRotation = Quaternion.Euler(0, 0, 0);
            _audioSource.PlayOneShot(carDoor);
        }
    }

    public void SetCameraSensitivity(int sensitivity)
    {
        RotationSpeed = sensitivity;
    }

    void Interactwithcar()
    {

        if (!hasInteracted)
        {

            Sign.SetActive(false);


            hasInteracted = true;
        }
    }

}
