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
                CarBehavior carScript = car.GetComponent<CarBehavior>();
                carScript.CheckAnimalsNumber();
                // change localposition to position to be able to find values in editor
                pickupableAnimal.transform.localPosition = pickupableAnimal.GetComponent<AnimalBehavior>().GetPlacementInCar();
                pickupableAnimal = null;
                _isHoldingAnimal = false;
                _canDropoff = false;
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

    private void OnTriggerEnter(Collider collision)
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

    private void OnTriggerExit(Collider collision)
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
