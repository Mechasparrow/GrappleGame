using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObject : GrappleTarget
{
    private Rigidbody rb;
    public bool grabbed = false;
    
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
    }

    public override void trigger(GameObject source)
    {
        if (!grabbed) base.trigger(source);
    }

    public override void untrigger(GameObject source)
    {
        if(!grabbed) base.untrigger(source);
    }


    public override void Interact(GameObject player)
    {
        // TODO make child of 
        
        GrapplePlayerBehavior gpb = player.GetComponent<GrapplePlayerBehavior>();
        Transform grabTransform = findGrabArea(gpb);
        
        if (grabTransform)
        {
            GrabTheObject(grabTransform, gpb);
        }
        
        Debug.Log("Alt behavior");
        
        
    }

    private void disablePhysics()
    {
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void enablePhysics()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }

    public void UngrabTheObject(GrapplePlayerBehavior gpb)
    {
        if (!grabbed) return;
        
        Transform t = gameObject.transform;
        t.parent = null;
        enablePhysics();

        grabbed = false;

        gpb.grabbedObject = null;
    }
    
    public void GrabTheObject(Transform grabArea, GrapplePlayerBehavior gpb)
    {
        if (grabbed) return;
        
        Transform t = gameObject.transform;
        Vector3 offset = new Vector3(0,0,3);
        
        disablePhysics();
        
        t.position = grabArea.position;
        t.parent = grabArea;

        t.localPosition = t.localPosition + offset;
        
        grabbed = true;


        gpb.grabbedObject = this;
    }

    private Transform findGrabArea(GrapplePlayerBehavior gpb)
    {
        Transform grabArea = null;
        
        
        if (gpb)
        {
            grabArea = gpb.grabTransform;
        }

        return grabArea;

    }
}
