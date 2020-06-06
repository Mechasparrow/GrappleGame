using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerSwing : MonoBehaviour
{
    private RigidbodyFirstPersonController fpsController;
    private Camera mainCamera;
    private Rigidbody rb;
    
    public SwingBehavior grappleReference;
    public TemporaryCameraFollower tcf;

    public bool grappleInProgress = false;
    
    // Start is called before the first frame update
    void Start()
    {
        fpsController = GetComponent<RigidbodyFirstPersonController>();
        mainCamera = fpsController.cam;
        rb = GetComponent<Rigidbody>();
    }

    void processInputs()
    {
        bool beginGrapple = Input.GetKeyUp(KeyCode.Mouse0);

        if (beginGrapple)
        {
            
            grappleReference.FlipRodManual();

            if (!grappleInProgress)
            {
                fixUp();
                grappleInProgress = true;
            }
            else
            {
                tearDown();
                grappleInProgress = false;
            }
            
        }
    }

    void fixUp()
    {
        mainCamera.enabled = false;
        tcf.cam.enabled = true;

        tcf.mouseLook = fpsController.mouseLook;
        tcf.transform.rotation = fpsController.transform.rotation;
        tcf.cam.transform.rotation = fpsController.cam.transform.rotation;
        
        fpsController.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
        
    }

    void tearDown()
    {
        mainCamera.enabled = true;
        tcf.cam.enabled = false;


        fpsController.transform.rotation = tcf.transform.rotation;
        fpsController.cam.transform.rotation = tcf.cam.transform.rotation;
        
        fpsController.enabled = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        processInputs();

        
    }
}
