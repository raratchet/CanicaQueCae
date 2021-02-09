using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformMover
{
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float platformMoveUnits;    
    [SerializeField]
    float platformMoveScale;
    float currentRotation;

    MonoBehaviour parent;

    public int FramesElapsedInTranslate { get { return (int)(platformMoveUnits / platformMoveScale); } }


    public void Initialize(MonoBehaviour courutineCreator)
    {
        parent = courutineCreator;
    }

    public void RotatePlatformsFromInput(float inputAxis, IEnumerable<Platform> platformsToRotate)
    {
        currentRotation = rotationSpeed * inputAxis;
        RotatePlatforms(currentRotation, platformsToRotate);
    }

    void RotatePlatforms(float angle, IEnumerable<Platform> platformsToRotate)
    {
        foreach(Platform plat in platformsToRotate)
        {
            plat.transform.Rotate(0, angle, 0);
        }
    }

    public void MovePlatformsUpwards(IEnumerable<Platform> elementsToMove)
    {
        parent.StartCoroutine(TranslatePlatformsUpwards(elementsToMove));
    }

    IEnumerator TranslatePlatformsUpwards(IEnumerable<Platform> elementsToMove)
    {
        for(float i = platformMoveScale; i < platformMoveUnits; i+= platformMoveScale)
        {
            foreach (Platform plat in elementsToMove)
            {
                plat.transform.Translate(Vector3.up * platformMoveScale);
            }
            yield return new WaitForEndOfFrame();
        }
    }

}
