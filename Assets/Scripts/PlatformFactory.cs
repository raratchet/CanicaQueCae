using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformFactory 
{

    Queue<Platform> platformPool;
    Queue<Platform> inUsePlatforms;

    [SerializeField]
    GameObject platformPrefab;
    [SerializeField]
    int poolSize;
    [SerializeField]
    Vector3 platInstancePoint;
    [SerializeField]
    List<Vector3> initialPlatsPos;


    public void Initialize()
    {
        platformPool = new Queue<Platform>();
        inUsePlatforms = new Queue<Platform>();
        PopulatePool();
        CreateStartPlatforms();
    }

    public Queue<Platform> ActivePlatforms { get { return inUsePlatforms; } }

    void PopulatePool()
    {
        for(int i = 0; i < poolSize; i++)
        {
            Platform plat = GameObject.Instantiate(platformPrefab).GetComponent<Platform>();
            platformPool.Enqueue(plat);
            plat.gameObject.SetActive(false);
        }
    }

    void CreateStartPlatforms()
    {
        foreach(Vector3 pos in initialPlatsPos)
        {
            CreatePlatformAt(pos);
        }

        //This code so teh first platform doesn't have obstacles
        Platform firstPlat = inUsePlatforms.Peek();
        firstPlat.ResetSegments();
        firstPlat.segments[3].SegmentType = PlatSegmentType.CLEAR;

    }

    public void CreatePlatform() 
    {
        CreatePlatformAt(platInstancePoint);
    }

    void CreatePlatformAt(Vector3 position)
    {
        Platform plat = platformPool.Dequeue();
        inUsePlatforms.Enqueue(plat);
        plat.transform.position = position;
        plat.gameObject.SetActive(true);

        if(inUsePlatforms.Count >= poolSize -  1)
        {
            DestroyTopPlatform();
        }
    }

    public void DestroyTopPlatform()
    {
        Platform plat = inUsePlatforms.Dequeue();
        platformPool.Enqueue(plat);
        plat.gameObject.SetActive(false);
    }


}
