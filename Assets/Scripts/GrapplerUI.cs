using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplerUI : MonoBehaviour
{
    public Renderer grappleUI;

    private Material disabledGrapplerMat;
    public Material enabledGrapplerMat;

    

    // Start is called before the first frame update
    void Start()
    {
        disabledGrapplerMat = grappleUI.material;
    }

    public void enableGrapplerUI()
    {
        grappleUI.material = enabledGrapplerMat;
    }

    public void disableGrapplerUI()
    {
        grappleUI.material = disabledGrapplerMat;
    }
}
