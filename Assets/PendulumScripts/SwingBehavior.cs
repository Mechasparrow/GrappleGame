using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehavior : MonoBehaviour
{

    public GameObject payload;
    public GameObject rod;

    private GrapplerRodManager grm;
    
    //inputs
    private bool disableRod = false;
    
    //config
    public bool defaultAnchor;
    public bool controllable;

    // Start is called before the first frame update
    void Start()
    {
        //Grab the grappler rod manager
        grm = rod.GetComponent<GrapplerRodManager>();
        rod.SetActive(false);
        
        if (defaultAnchor){
            rod.SetActive(true);
            //Grab the closest anchor
            GameObject closestAnchor = scoutOutClosestAnchorPoint();
        
            grm.AnchorUp(payload.GetComponent<Rigidbody>(), closestAnchor.GetComponent<Rigidbody>());
        }
    }

    public GameObject scoutOutClosestAnchorPoint()
    {
        GameObject[] anchorPoints = grabAnchorPoints();

        float? minDistance = null;

        GameObject selectedAnchorPoint = null;
        
        foreach (GameObject anchorPoint in anchorPoints)
        {
            float distance = Vector3.Distance(payload.transform.position, anchorPoint.transform.position);

            if (minDistance == null || distance <= minDistance)
            {
                selectedAnchorPoint = anchorPoint;
                minDistance = distance;
            }

        }

        return selectedAnchorPoint;
    }
    
    private GameObject[] grabAnchorPoints()
    {
        string anchorTag = "anchorPoint";
        return GameObject.FindGameObjectsWithTag(anchorTag);
    }
    
    void processInputs()
    {
        disableRod = Input.GetKeyUp(KeyCode.Space);
    }

    public void FlipRodManual()
    {
        disableRod = true;
    }
    
    void ResetRodFlag()
    {
        disableRod = false;
    }

    public bool isGrappled()
    {
        return rod.activeInHierarchy;
    }
    
    private void GameLogic(bool _disableRod)
    {
        if (_disableRod && rod.activeInHierarchy)
        {
            grm.DestroyAnchor();
            rod.SetActive(false);
        }
        else if (_disableRod && !rod.activeInHierarchy)
        {
            rod.SetActive(true);
         
            if (grm)
            {
                GameObject closestAnchorPoint = scoutOutClosestAnchorPoint();
                Debug.Log("grm");
                
                if (closestAnchorPoint)
                {
                    Debug.Log("Closest anchor");
                    
                    grm.AnchorUp(payload.GetComponent<Rigidbody>(), closestAnchorPoint.GetComponent<Rigidbody>());
                    
                }
            }
        }
    }
    
    

    // Update is called once per frame
    void Update()
    {
        if (controllable)
        {
            processInputs();
        }
        GameLogic(disableRod);
        
        ResetRodFlag();
    }
}
