using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimalInteraction : MonoBehaviour
{
    private GameObject pickupableAnimal;
    private GameObject car;
    private bool _isHoldingAnimal = false;
    private bool _canDropoff = false;
    // [SerializeField] private float _animalHoldingDistance;
    // [SerializeField] private float _animalInCarPlacingHeight;

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
                // Debug.DrawRay(pickupableAnimal.transform.position, Vector3.down, Color.blue);
                
                if(Physics.Raycast(pickupableAnimal.transform.position, Vector3.down, out RaycastHit hit)) {
                    Vector3 newPos = pickupableAnimal.transform.position + Vector3.down * hit.distance;
                    pickupableAnimal.transform.position = newPos;
                }
                pickupableAnimal.GetComponent<Collider>().enabled = true;
                _isHoldingAnimal = false;
            }
        }
        else if(pickupableAnimal != null) 
        {
            AnimalBehavior animalInfo = pickupableAnimal.GetComponent<AnimalBehavior>();
            pickupableAnimal.transform.parent = transform;
            pickupableAnimal.transform.localPosition = Vector3.forward * animalInfo.GetHoldingDistance() + Vector3.up * animalInfo.GetHoldingHeight();
            pickupableAnimal.GetComponent<Collider>().enabled = false;

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
