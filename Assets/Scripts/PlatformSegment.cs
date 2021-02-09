using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlatSegmentType
{
    NORMAL, OBSTACLE, CLEAR
}

public class PlatformSegment : MonoBehaviour
{
    
    MeshCollider collider;
    Renderer renderer;

    PlatSegmentType sType;
    public PlatSegmentType SegmentType 
    { get { return sType; } 
      set { sType = value;
            if (sType.Equals(PlatSegmentType.CLEAR))
                collider.isTrigger = true;
            else
                collider.isTrigger = false;

            renderer.enabled = true;

            if (sType.Equals(PlatSegmentType.NORMAL))
                renderer.material.color = Color.white;
            else if (sType.Equals(PlatSegmentType.OBSTACLE))
                renderer.material.color = Color.red;
            else
                renderer.enabled = false;
        } 
    }

    public void Start()
    {
        collider = GetComponent<MeshCollider>();
        renderer = GetComponent<Renderer>();
    }
}
