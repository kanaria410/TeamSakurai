using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPenetration : MonoBehaviour
{
    float BehaviorTime;
    bool Behavior;

    int frame;
    int timer;

    // Use this for initialization
    void Start()
    {
        frame = 0;
        timer = 60;
        //gameObject.layer = LayerMask.NameToLayer("Nomal");
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    gameObject.layer = LayerMask.NameToLayer("Jump");
        //    bool Behavior;
        //}

        //if (Behavior==true)
        //{
        //    frame++;
        //    Debug.Log(frame);
        //}

        //if (frame > timer)
        //{
        //    gameObject.layer = LayerMask.NameToLayer("Nomal");
        //    frame = 0;
        //    Behavior = false;
        //}
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameObject.layer = LayerMask.NameToLayer("Jump");
            Behavior = true;
        }

        if (Behavior == true)
        {
            BehaviorTime += Time.deltaTime;
            Debug.Log(BehaviorTime);
        }

        if (BehaviorTime >= 2)
        {
            gameObject.layer = LayerMask.NameToLayer("Nomal");
            BehaviorTime = 0;
            Behavior = false;
        }

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Behavior = true;
        //    if (Behavior && timer > 0.0f)
        //    {
        //        frame++;
        //        Debug.Log(frame);
        //    }
        //    //frame++;
        //    //Debug.Log(frame);
        //    gameObject.layer = LayerMask.NameToLayer("Jump");
        //    //bool Behavior;
        //    if (frame > timer)
        //    {
        //        gameObject.layer = LayerMask.NameToLayer("Nomal");
        //        //frame = 0;
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.layer = LayerMask.NameToLayer("GetOff");
            Behavior = true;
        }
        if (Behavior == true)
        {
            BehaviorTime += Time.deltaTime;
            Debug.Log(BehaviorTime);
        }
        if (BehaviorTime >= 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Nomal");
            BehaviorTime = 0;
            Behavior = false;
        }
    }
}