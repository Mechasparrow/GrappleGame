using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleTarget : MonoBehaviour, ITriggerable, IInteractable
{
    public Material triggerMat,untriggerMat;

    private new Renderer renderer;

    public Transform cubePosTeleport;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject potentialClaw = collision.gameObject;
        if (potentialClaw.CompareTag("claw"))
        {
            GrappleScript gs = potentialClaw.GetComponent<GrappleScript>();

            if (gs.launchGrapple)
            {
                gs.resetGrappleClaw();
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                Interact(player);
            }
        }
    }

    public virtual void trigger(GameObject source)
    {
        
        renderer.material = triggerMat;
    }

    public virtual void Interact(GameObject teleportVictim)
    {
        //Reset velocity
        Rigidbody teleportRB = teleportVictim.GetComponent<Rigidbody>();
        teleportRB.velocity = Vector3.zero;

        GrapplePlayerBehavior gpb = teleportVictim.GetComponent<GrapplePlayerBehavior>();
        if (gpb)
        {
            gpb.grappleTo(cubePosTeleport);
        }
        else
        {
            //Change Position (teleport)
            teleportVictim.transform.position = cubePosTeleport.position;
        }

    }



    public virtual void untrigger(GameObject source)
    {
        //TODO
        renderer.material = untriggerMat;
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        renderer = GetComponent<Renderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
