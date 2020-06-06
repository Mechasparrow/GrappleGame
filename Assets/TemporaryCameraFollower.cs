using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class TemporaryCameraFollower : MonoBehaviour
{

    public Transform objectTransform;
    public bool following;

    public Camera cam;

    public MouseLook mouseLook = new MouseLook();

    void Start()
    {
        mouseLook.Init (transform, cam.transform);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        mouseLook.LookRotation (transform, cam.transform);
        if (following)
        {
            transform.position = objectTransform.position;
        }
    }
}
