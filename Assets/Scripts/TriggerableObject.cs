using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableObject : MonoBehaviour, ITriggerable
{
    virtual public void trigger(GameObject source)
    {
        throw new System.NotImplementedException();
    }

    virtual public void untrigger(GameObject source)
    {
        throw new System.NotImplementedException();
    }

}
