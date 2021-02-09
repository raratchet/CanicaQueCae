using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic
{
    bool hasRecentBounce;
    bool isTraspassing;

    Ball ball;

    public BallLogic(Ball ballToMaster)
    {
        ball = ballToMaster;
    }

    public void NotifyCollisionEnter(Collision collision)
    {
        if (!hasRecentBounce && !isTraspassing)
            DoBounceBehaviour();

        BallBounceEvent bEvent = new BallBounceEvent();
        bEvent.segment = collision.gameObject.GetComponent<PlatformSegment>();
        bEvent.platform = collision.gameObject.GetComponentInParent<Platform>();
        EventObserver.instance.BallBounce(bEvent);
    }

    public void NotifyTriggerEnter(Collider other)
    {
        if (isTraspassing)
            return;

        BallTraspassEvent bEvent = new BallTraspassEvent();
        bEvent.segment = other.gameObject.GetComponent<PlatformSegment>();
        bEvent.platform = other.gameObject.GetComponentInParent<Platform>();
        EventObserver.instance.BallTraspass(bEvent);
    }

    public void NotifyCollisionExit(Collision collision)
    {
        hasRecentBounce = false;
    }

    public void NotifyTriggerExit(Collider other)
    {
        isTraspassing = false;
    }

    public void NotifyTriggerStay(Collider other)
    {
        isTraspassing = true;
    }

    void DoBounceBehaviour()
    {
        hasRecentBounce = true;
        ball.PlayAnimation("Bounce");
        ball.AddBounceForce();
    }

}
