using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventObserver: MonoBehaviour // This is monobehaviour so we don´t get weird behaviour when launching events
{

    #region Singleton

    public static EventObserver instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    #endregion  

    public event EventHandler<BallBounceEvent> OnBallBounce;
    public event EventHandler<BallTraspassEvent> OnBallTraspass;

    public void BallBounce(BallBounceEvent ballBounceEvent)
    {
        OnBallBounce?.Invoke(this, ballBounceEvent); 
    }

    public void BallTraspass(BallTraspassEvent ballTrasPassEvent)
    {
        OnBallTraspass?.Invoke(this, ballTrasPassEvent);
    }

}

public class Event
{

}

public class BallBounceEvent : Event
{
    public Platform platform;
    public PlatformSegment segment;
}

public class BallTraspassEvent :Event
{
    public Platform platform;
    public PlatformSegment segment;
}
