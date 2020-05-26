using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GoalBehavior : MonoBehaviour
{
    public TriggerableObject triggerObject;

    public GameObject colliderObject;

    public void OnTriggerEnter(Collider other)
    {
        //TODO make public
        string otherTag = colliderObject.tag;
    
        if (other.CompareTag(otherTag))
        {
            triggerObject.trigger(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
