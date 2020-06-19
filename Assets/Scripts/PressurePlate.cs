using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    public TriggerableObject triggerableObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();

        if (otherRb){
            triggerableObject.trigger(gameObject);
        }
    }

    private void OnCollisionExit(Collision other) {
        Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody>();

        if (otherRb){
            triggerableObject.untrigger(gameObject);
        }    
    }

}
