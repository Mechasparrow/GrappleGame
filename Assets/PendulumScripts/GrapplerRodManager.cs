using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class GrapplerRodManager : MonoBehaviour
{
    private RodLengthGen rlg;
    private Rigidbody rb;

    private HingeJoint anchorJoint;
    private FixedJoint fixedJoint;

    // Start is called before the first frame update
    void Start()
    {
        InitFields();     
    }

    void InitFields()
    {
        rlg = GetComponent<RodLengthGen>();
        rb = GetComponent<Rigidbody>();
        fixedJoint = GetComponent<FixedJoint>();
        anchorJoint = null;
    }
    
    public void AttachAnchor(Rigidbody anchorPoint)
    {
        GameObject g = new GameObject();
        g.transform.parent = anchorPoint.transform;

        anchorJoint = g.AddComponent<HingeJoint>();
        g.GetComponent<Rigidbody>().isKinematic = true;
        g.transform.position = anchorPoint.transform.position;
        
        anchorJoint.anchor = Vector3.zero;
        
        
        
    }

    public void DestroyAnchor()
    {
        if (anchorJoint)
        {
            Destroy(anchorJoint.gameObject);
            anchorJoint = null;
        }
    }
    
    //Notes
    /*
     * Still thinks that old original anchor is its best friend
     */
    
    public void AnchorUp(Rigidbody payloadBody, Rigidbody anchorPoint)
    {
        if (fixedJoint == null)
        {
            InitFields();
        }
        
        fixedJoint.connectedBody = null;

        
        //Set new fields
        AttachAnchor(anchorPoint);

        
        rb.isKinematic = true;
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        
        //Apply the scaling
        Vector3 axisVector = rlg.ApplyScaling(payloadBody.gameObject.transform, anchorPoint.gameObject.transform,null);

     
        //Fix the rod to the payload
        fixedJoint.connectedBody = payloadBody;
        
        //Fix the rod to the anchor
        anchorJoint.connectedBody = rb;
        anchorJoint.axis = axisVector;
        
        rb.isKinematic = false;

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
