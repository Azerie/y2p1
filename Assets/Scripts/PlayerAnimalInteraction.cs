using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimalInteraction : MonoBehaviour
{
    private GameObject pickupableAnimal;
    private GameObject car;
    private bool _isHoldingAnimal = false;
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
            // drop animal on the ground
            pickupableAnimal.transform.parent = transform.parent;
            // Debug.DrawRay(pickupableAnimal.transform.position, Vector3.down, Color.blue);
            
            if(Physics.Raycast(pickupableAnimal.transform.position, Vector3.down, out RaycastHit hit)) {
                Vector3 newPos = pickupableAnimal.transform.position + Vector3.down * hit.distance;
                pickupableAnimal.transform.position = newPos;
            }
            pickupableAnimal.GetComponent<Rigidbody>().useGravity = true;
            pickupableAnimal.GetComponent<Rigidbody>().isKinematic = false;
            pickupableAnimal.GetComponent<Collider>().enabled = true;
            pickupableAnimal.GetComponent<AnimalBehavior>().UnSave();

            pickupableAnimal.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
            _isHoldingAnimal = false;
        }
        else if(pickupableAnimal != null) 
        {
            AnimalBehavior animalInfo = pickupableAnimal.GetComponent<AnimalBehavior>();
            pickupableAnimal.transform.parent = transform;
            pickupableAnimal.transform.localPosition = Vector3.forward * animalInfo.GetHoldingDistance() + Vector3.up * animalInfo.GetHoldingHeight();
            pickupableAnimal.transform.rotation = Quaternion.Euler(Vector3.zero);
            animalInfo.PlayPickup();
            pickupableAnimal.GetComponent<Rigidbody>().useGravity = false;
            pickupableAnimal.GetComponent<Rigidbody>().isKinematic = true;
            pickupableAnimal.GetComponent<Collider>().enabled = false;
            animalInfo.Save();

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
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!_isHoldingAnimal && collision.gameObject.tag == "Animal")
        {
            pickupableAnimal = null;
        }
    }
}
