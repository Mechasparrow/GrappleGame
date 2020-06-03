using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBehavior : MonoBehaviour
{

    public GameObject rod;

    //inputs
    private bool disableRod = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void processInputs()
    {
        disableRod = Input.GetKeyUp(KeyCode.Space);
    }

    private void gameLogic()
    {
        if (disableRod)
        {
            rod.SetActive(false);
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
