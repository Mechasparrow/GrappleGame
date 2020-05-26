using UnityEngine;

public class GameTimer
{
    private float timeThreshold;
    private float timeElapsed;


    public delegate void TimerDelegate();

    public GameTimer(float timeThreshold)
    {
        this.timeThreshold = timeThreshold;
        this.timeElapsed = 0.0f;
    }

    public void UpdateTimer()
    {
        this.timeElapsed += Time.deltaTime;
    }

    public void ResetTimer()
    {
        this.timeElapsed = 0.0f;
    }

    public bool CheckTimer(TimerDelegate callback)
    {
        bool passed = this.timeElapsed >= this.timeThreshold;
        if (passed)
        {
            callback();
            ResetTimer();
        }
        return passed;
    }
}