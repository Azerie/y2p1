using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimalInteraction : MonoBehaviour
{
    private GameObject pickupableAnimal;
    private GameObject car;
    private bool _isHoldingAnimal = false;
    private bool _canDropoff = false;
    [SerializeField] private float _animalHoldingDistance;
    [SerializeField] private float _animalInCarPlacingHeight;

    private void Awake()
    {
        car = GameObject.FindGameObjectWithTag("Car");
    }

    public void Interact()
    {
        if(_isHoldingAnimal) 
        {
            // drop animal in the car
            if(_canDropoff)
            {
                pickupableAnimal.transform.parent = car.transform;
                pickupableAnimal.transform.localPosition = Vector3.up * car.transform.childCount;
                pickupableAnimal = null;
                _isHoldingAnimal = false;
            }
            // drop animal on the ground
            else
            {
                pickupableAnimal.transform.parent = transform.parent;
                pickupableAnimal.GetComponent<Collider>().isTrigger = false;

                _isHoldingAnimal = false;
            }
        }
        else if(pickupableAnimal != null) 
        {
            pickupableAnimal.transform.parent = transform;
            pickupableAnimal.transform.localPosition = Vector3.forward * _animalHoldingDistance;
            pickupableAnimal.GetComponent<Collider>().isTrigger = true;

            _isHoldingAnimal = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // can only pick up animal if not holding anything
        if(!_isHoldingAnimal && collision.gameObject.tag == "Animal")
        {
            pickupableAnimal = collision.gameObject;
        }
        else if(_isHoldingAnimal && collision.gameObject.tag == "Car")
        {
            _canDropoff = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!_isHoldingAnimal && collision.gameObject.tag == "Animal")
        {
            pickupableAnimal = null;
        }
        else if (_isHoldingAnimal && collision.gameObject.tag == "Car")
        {
            _canDropoff = false;
        }
    }
}
