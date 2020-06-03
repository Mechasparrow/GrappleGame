using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodLengthGen : MonoBehaviour
{

    public delegate void PipeNext();
    
    public Transform fromTransform;
    public Transform toTransform;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public void ApplyScaling(PipeNext pipeNext)
    {
        //Apply the scaling
        transform.position = toTransform.position + (fromTransform.position - toTransform.position) / 2;
        
        //FIXME TODO apply rotation as well upon the zed axis
        Vector3 localScale = transform.localScale;
        localScale.x = computeDistance();
        transform.localScale = localScale;
        
        
        
        //Once complete
        pipeNext();
    }
    
    private float computeDistance()
    {
        return Vector3.Distance(fromTransform.position, toTransform.position);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
