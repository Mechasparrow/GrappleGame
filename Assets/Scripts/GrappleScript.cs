using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class GrappleScript : MonoBehaviour
{

    private LineRenderer rope;

    public Transform baseTransform;


    public Transform targetTransform = null;
    public Transform clawTransform;

    //Launch Claw
    public Vector3 initialClawPos;
    public bool launchGrapple = false;
    private bool retractGrapple = false;
    private GameTimer grappleTimer;

    //Based off distance
    private float defaultTargetDistance = 5.0f;
    private float? newTargetDistance = null;
    private float distanceTraveled = 0.0f;
    private float grappleSpeed = 10.0f;

    //GrappleTarget
    public RaycastTarget raycastTarget;

    private GrappleTarget currentGrappleTarget = null;

    //Visual Cue
    public GrapplerUI grappleUI;

    // Start is called before the first frame update
    void Start()
    {
        //Init timer
        grappleTimer = new GameTimer(timeThreshold: 1.0f);

        //Grab initial claw pos
        initialClawPos = clawTransform.localPosition;

        //Init Rope
        rope = GetComponentInChildren<LineRenderer>();
        rope.enabled = true;
        updateRope();

        //Grab grapple target
        updateGrappleTarget();
    }

    public void resetGrappleClaw()
    {

        clawTransform.localPosition = initialClawPos;
        clawTransform.LookAt(baseTransform);
        clawTransform.Rotate(new Vector3(0, -90, 0));
        distanceTraveled = 0.0f;
        launchGrapple = false;
        retractGrapple = false;
        currentGrappleTarget = null;
        newTargetDistance = null;
        clawTransform.localScale = Vector3.one;
    }

    private void updateGrappleTarget()
    {
        currentGrappleTarget = raycastTarget.getGrappleTarget();
    }

    void updateRope()
    {
        rope.SetPosition(0, baseTransform.position);
        rope.SetPosition(1, clawTransform.position);
    }

    public void anchorClaw(Transform targetTransform, Vector3 offsetVector)
    {
        clawTransform.position = targetTransform.position + offsetVector;
        clawTransform.localScale = 0.5f * Vector3.one;
        updateRope();
    }

    Vector3 grabLaunchAngle(Transform targetTransform)
    {
        return (targetTransform.position - baseTransform.position).normalized; 
    }

    void grappleBehavior(Transform grappleTargetTransform)
    {

        bool startLaunch = Input.GetKeyUp(KeyCode.Mouse0);
        bool retractGrappleBtn = Input.GetKeyUp(KeyCode.Mouse1);

        if (newTargetDistance == null && (startLaunch || retractGrappleBtn))
        {
            if (grappleTargetTransform == null)
            {
                targetTransform = clawTransform;
                newTargetDistance = defaultTargetDistance;

                clawTransform.LookAt(baseTransform);
                clawTransform.Rotate(new Vector3(0, -90, 0));
            }
            else
            {
                targetTransform = grappleTargetTransform;
                newTargetDistance = Vector3.Distance(clawTransform.position, grappleTargetTransform.position);
                
                if (!retractGrappleBtn)
                {
                    clawTransform.LookAt(targetTransform);
                    clawTransform.Rotate(new Vector3(0, 90, 0));
                }
            }
        }

        if (!launchGrapple && startLaunch)
        {
            launchGrapple = true;
        }

        if (!retractGrapple && retractGrappleBtn)
        {
            retractGrapple = true;
        }


        bool launchComplete = (distanceTraveled >= newTargetDistance) && launchGrapple;
        bool retractComplete = (distanceTraveled <= 0) && retractGrapple;

        if (launchComplete || retractComplete)
        {
            launchGrapple = false;
            retractGrapple = false;
            newTargetDistance = null;


        }

        if (retractComplete)
        {
            clawTransform.localPosition = initialClawPos;
        }

        if (launchGrapple || retractGrapple)
        {

            Vector3 deltaGrapplePosition = grabLaunchAngle(targetTransform) * grappleSpeed * Time.deltaTime;



            if (launchGrapple)
            {
                clawTransform.position += deltaGrapplePosition;
                distanceTraveled += deltaGrapplePosition.magnitude;
            }
            else
            {
                clawTransform.position -= deltaGrapplePosition;
                distanceTraveled -= deltaGrapplePosition.magnitude;
            }




        }
    }

    // Update is called once per frame
    void Update()
    {

        updateGrappleTarget();


        Transform grappleTargetTransform = null;
        
        if (currentGrappleTarget != null)
        {
            grappleTargetTransform = currentGrappleTarget.transform;
            grappleUI.enableGrapplerUI();
        }else
        {
            grappleUI.disableGrapplerUI();
        }

        grappleBehavior(grappleTargetTransform);

        updateRope();   
    }
}
