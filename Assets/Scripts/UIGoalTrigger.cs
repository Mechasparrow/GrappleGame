using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGoalTrigger : TriggerableObject
{
    public Text winText;

    override public void trigger(GameObject source)
    {
        Debug.Log("Triggered Goal");
        winText.enabled = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
