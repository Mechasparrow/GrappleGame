using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableObject : MonoBehaviour, ITriggerable
{
    virtual public void trigger(GameObject source)
    {
        throw new System.NotImplementedException();
    }

    public void untrigger(GameObject source)
    {
        throw new System.NotImplementedException();
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
