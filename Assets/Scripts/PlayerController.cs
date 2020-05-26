using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private IControls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        this.playerControls = new PlayerControls();
    }

    private Direction GetDirection(IControls controls)
    {
        //null check
        if (controls == null)
        {
            return Direction.NONE;
        }

        if (controls.goLeft())
        {
            return Direction.LEFT;
        }else if (controls.goRight())
        {
            return Direction.RIGHT;
        }
        else if (controls.goUp())
        {
            return Direction.UP;
        }
        else if (controls.goDown())
        {
            return Direction.DOWN;
        }else
        {
            return Direction.NONE;
        }


    }

    void processInputs()
    {
        Direction direction = GetDirection(this.playerControls);

        Vector2 offsetTranslate = Vector2.zero;
        
        //TODO make this customizable
        

        switch (direction)
        {
            case Direction.LEFT:
                offsetTranslate = Vector2.left;
                break;
            case Direction.UP:
                offsetTranslate = Vector2.up;
                break;
            case Direction.DOWN:
                offsetTranslate = Vector2.down;
                break;
            case Direction.RIGHT:
                offsetTranslate = Vector2.right;
                break;
            default:
                offsetTranslate = Vector2.zero;
                break;
        }


        float mag = 1.0f;
        movePlayer(offsetTranslate, mag);
        
    }

    void movePlayer(Vector2 offset, float mag)
    {
        Vector3 newPlayerPos = transform.position + (new Vector3(x: -offset.x, y:0, z:-offset.y) * mag); ;
        transform.position = newPlayerPos;
    }


    // Update is called once per frame
    void Update()
    {
        processInputs();
    }
}
