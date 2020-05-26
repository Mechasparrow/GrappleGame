

using UnityEngine;

public class PlayerControls : IControls
{

    public bool goDown()
    {
        return Input.GetKeyDown(KeyCode.DownArrow);
    }

    public bool goLeft()
    {
        return Input.GetKeyDown(KeyCode.LeftArrow);
    }

    public bool goRight()
    {
        return Input.GetKeyDown(KeyCode.RightArrow);
    }

    public bool goUp()
    {
        return Input.GetKeyDown(KeyCode.UpArrow);
    }
}
