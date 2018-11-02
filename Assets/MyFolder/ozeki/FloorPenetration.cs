using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPenetration : MonoBehaviour
{
    bool ThroughFlag;
    int frame;
    int timer;
    // Use this for initialization
    void Start()
    {
        frame = 0;

        timer = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (frame < timer)
        {
            ThroughFlag = false;

        }
    }
}