using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityTrigger : pressurePlateVisualTrigger
{

    public GameObject objectToActivate;

    override public void ExtraBehavior(){
        objectToActivate.SetActive(false);
    }

    override public void trigger(GameObject source){
        base.trigger(source);
        objectToActivate.SetActive(true);
    }

    override public void untrigger(GameObject source){
        base.untrigger(source);
        objectToActivate.SetActive(false);
    }
}
