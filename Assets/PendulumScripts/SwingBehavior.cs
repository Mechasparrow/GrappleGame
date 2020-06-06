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
    
    // Start is called before the first frame update
    void Start()
    {
        //Grab the grappler rod manager
        grm = rod.GetComponent<GrapplerRodManager>();

        //Grab the closest anchor
        GameObject closestAnchor = scoutOutClosestAnchorPoint();
        
        grm.AnchorUp(payload.GetComponent<Rigidbody>(), closestAnchor.GetComponent<Rigidbody>());

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

    private void gameLogic()
    {
        GrapplerRodManager grm = rod.GetComponent<GrapplerRodManager>();
        
        if (disableRod && rod.activeInHierarchy)
        {
            grm.DestroyAnchor();
            rod.SetActive(false);
        }
        else if (disableRod && !rod.activeInHierarchy)
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
    
    void resetInputs()
    {
        disableRod = false;
    }

    // Update is called once per frame
    void Update()
    {
        processInputs();

        gameLogic();
        
        resetInputs();
    }
}
