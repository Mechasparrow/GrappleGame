using UnityEngine;

public interface ITriggerable
{
    void trigger(GameObject source);
    void untrigger(GameObject source);
}

