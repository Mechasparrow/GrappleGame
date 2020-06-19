using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurePlateVisualTrigger : TriggerableObject
{

    public Material untriggeredMat;
    public Material triggeredMat;

    private Renderer renderer;

    private void Start() {
        renderer = GetComponent<Renderer>();
        renderer.material = untriggeredMat;            
    
        ExtraBehavior();
    }

    virtual public void ExtraBehavior(){

    }

    override public void trigger(GameObject source){
        renderer.material = triggeredMat;
    }

    override public void untrigger(GameObject source){
        renderer.material = untriggeredMat;
    }
}
