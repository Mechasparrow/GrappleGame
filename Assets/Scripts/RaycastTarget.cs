using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTarget : MonoBehaviour
{
    private Transform camTransform = null;
    private Vector3 origin;
    private Vector3 direction;

    private GrappleTarget currGrappleTarget;

    public float range;

    // Start is called before the first frame update
    void Start()

    {
        currGrappleTarget = null;
        Camera camera = GetComponentInChildren<Camera>();
        camTransform = camera.transform;
        origin = Vector3.zero;
        direction = Vector3.zero;
    }

    void renderRay(Vector3 origin, Vector3 dir, float distance)
    {
        Debug.DrawLine(origin, origin + (dir*distance), Color.red);
    }



    void raycastTarget(Vector3 origin, Vector3 dir, float distance)
    {
        RaycastHit hit;
        
        bool raycast = Physics.Raycast(origin, dir, out hit, distance);

        bool disableOld = true;

        if(raycast)
        {
            GameObject hitGameObject = hit.collider.gameObject;

            GrappleTarget grappleTarget = hitGameObject.GetComponent<GrappleTarget>();
            
            if (grappleTarget != null)
            {

                disableOld = !(grappleTarget == currGrappleTarget);

                grappleTarget.trigger(gameObject);
                currGrappleTarget = grappleTarget;
            }

        }

        if (disableOld && currGrappleTarget != null)
        {
            currGrappleTarget.untrigger(gameObject);
        }

        if (raycast == false)
        {
            currGrappleTarget = null;
        }
    }

    public GrappleTarget getGrappleTarget()
    {
        return this.currGrappleTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (camTransform != null)
        {
            origin = camTransform.position;
            direction = camTransform.forward;
        }
        raycastTarget(origin, direction, range);

        bool interactWithObject = false;
        IInteractable interactableObject = (IInteractable)currGrappleTarget;

        if (currGrappleTarget != null && interactWithObject)
        {
            interactableObject.Interact(gameObject);
        }
    
    }
}
