using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimalInteraction : MonoBehaviour
{
    private GameObject pickupableAnimal;
    private bool _isHoldingAnimal;
    [SerializeField] private float _animalHoldingDistance;

    public void Interact()
    {
        if(_isHoldingAnimal) 
        {
            // drop animal here
            pickupableAnimal.transform.parent = transform.parent;
            pickupableAnimal.GetComponent<Collider>().isTrigger = false;

            _isHoldingAnimal = false;
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
        if(!_isHoldingAnimal && collision.gameObject.tag == "Animal")
        {
            pickupableAnimal = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!_isHoldingAnimal && collision.gameObject.tag == "Animal")
        {
            pickupableAnimal = null;
        }
    }
}
