using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class RodLengthGen : MonoBehaviour
{

    public delegate void PipeNext();
    
    public Vector3 ApplyScaling(Transform fromTransform, Transform toTransform, PipeNext pipeNext)
    {
        Vector3 deltaPos = (fromTransform.position - toTransform.position);
        
        //Apply the scaling
        transform.position = toTransform.position + deltaPos / 2;
        
        //FIXME TODO apply rotation as well upon the zed axis
        Vector3 localScale = transform.localScale;
        localScale.x = computeDistance(fromTransform.position, toTransform.position);
        transform.localScale = localScale;
        
        //apply rotation
        transform.LookAt(toTransform);
        Vector3 eulerAnglesOld = transform.rotation.eulerAngles;
        eulerAnglesOld.x += 90;
        eulerAnglesOld.z += 90;
        transform.rotation = Quaternion.Euler(eulerAnglesOld);
        
        //Find the new axis
        Vector3 normal = Vector3.Cross(deltaPos, Vector3.down).normalized;
        
        Debug.DrawLine(toTransform.position, toTransform.position + normal * 5, Color.white, 10.0f);
        
        
        //Once complete
        pipeNext?.Invoke();
        return normal;
    }
    
    private float computeDistance(Vector3 from, Vector3 to)
    {
        return Vector3.Distance(from,to);
    }
    
}
