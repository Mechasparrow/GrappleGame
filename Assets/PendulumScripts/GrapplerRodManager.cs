using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplerRodManager : MonoBehaviour
{
    private RodLengthGen rlg;
    private Rigidbody rb;

    public HingeJoint anchorJoint;
    private FixedJoint fixedJoint;

    //Payload
    public Rigidbody payload;

    // Start is called before the first frame update
    void Start()
    {
        rlg = GetComponent<RodLengthGen>();
        rb = GetComponent<Rigidbody>();
        fixedJoint = GetComponent<FixedJoint>();
        
        rlg.ApplyScaling(linkUpRod);
    }

    private void linkUpRod()
    {
        //TODO
        //Disable Rigidbody
        rb.isKinematic = true;
        
        //TODO fixed joint to payload
        fixedJoint.connectedBody = payload;

        //TODO hinge joint to main anchor
        anchorJoint.connectedBody = rb;

        //TODO
        //Enable Rigidbody
        rb.isKinematic = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
