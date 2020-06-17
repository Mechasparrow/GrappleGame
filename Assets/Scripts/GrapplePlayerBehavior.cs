using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GrapplePlayerBehavior : MonoBehaviour
{

    public delegate void onComplete();


    //grapple variables
    public bool startGrapple = false;
    public float speed = 5.0f;
    public Transform grappleTarget;
    private Vector3 targetPos;
    private float targetDistance = 0.0f;
    private float distanceTraveled = 0.0f;

    //Grabbing variables
    public Transform grabTransform;
    public GrabbableObject grabbedObject;
    
    //physics variable
    private Rigidbody rb;
    private GrappleScript gs;

    
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        gs = GetComponentInChildren<GrappleScript>();
    }

    // Returns delta pos normalized to mag 1
    public Vector3 getDeltaPos()
    {
        Vector3 currentPos = transform.position;
        Vector3 deltaPos = (targetPos - currentPos);
        return deltaPos.normalized;
    }

    public float computeTargetDistance()
    {
        float distance = Vector3.Distance(transform.position, targetPos);
        return distance;
    }

    public void grappleTo(Transform grappleTarget)
    {
        this.grappleTarget = grappleTarget;
        targetPos = grappleTarget.position;
        targetDistance = computeTargetDistance();
        startGrapple = true;
        rb.isKinematic = true;
    }

    
    
    void grapplingBehavior()
    {
        if (startGrapple == true)
        {
            if (distanceTraveled >= targetDistance)
            {
                distanceTraveled = 0.0f;
                startGrapple = false;
                rb.isKinematic = false;
                gs.resetGrappleClaw();
            }
            else
            {
                Vector3 deltaPos = getDeltaPos() * speed * Time.deltaTime;
                transform.position = transform.position + deltaPos;
                distanceTraveled += speed * Time.deltaTime;
                gs.anchorClaw(grappleTarget, new Vector3(0,-1,0));
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        bool ungrab = Input.GetKeyUp(KeyCode.Mouse1);

        if (ungrab && grabbedObject)
        {
            grabbedObject.UngrabTheObject(this);
        }
        
        grapplingBehavior();
    }
}
