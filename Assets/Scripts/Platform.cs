using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    public List<PlatformSegment> segments;

     void Awake()
    {
        foreach(PlatformSegment segment in transform.GetComponentsInChildren<PlatformSegment>(true))
        {
            segment.Start();
            segments.Add(segment);
        }
    }

    void OnEnable()
    {
        RandomizeSegments();
    }

    void OnDisable()
    {
        ResetSegments();
    }

    public void RandomizeSegments()
    {
        if (segments.Count == 0) return;

        int ran1 = Random.Range(0, segments.Count);
        int ran2 = Random.Range(0, segments.Count);
        int ran3 = Random.Range(0, segments.Count);

        segments[ran1].SegmentType = PlatSegmentType.OBSTACLE;
        segments[ran2].SegmentType = PlatSegmentType.OBSTACLE;
        segments[ran3].SegmentType = PlatSegmentType.CLEAR;
    }

    public void ResetSegments()
    {
        foreach(PlatformSegment segment in segments)
        {
            segment.SegmentType = PlatSegmentType.NORMAL;
        }
    }
}
