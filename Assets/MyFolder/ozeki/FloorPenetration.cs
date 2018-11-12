using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPenetration : MonoBehaviour
{
    float BehaviorTime;
    bool Behavior;


    // Use this for initialization
    void Start()
    {
        StartCoroutine("BehaviorTime");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            gameObject.layer = LayerMask.NameToLayer("Jump");
            bool Behavior;
        }
        if (Behavior == true)
        {
            BehaviorTime += Time.deltaTime;
        }
        if (BehaviorTime >= 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Nomal");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            gameObject.layer = LayerMask.NameToLayer("GetOff");
            bool Behavior;
        }
        if (Behavior == true)
        {
            BehaviorTime += Time.deltaTime;
        }
        if (BehaviorTime >= 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Nomal");
        }
    }
}