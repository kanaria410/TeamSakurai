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
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            //レイヤー名をJumpに変更
            gameObject.layer = LayerMask.NameToLayer("Jump");
            //時間を動かす
            Behavior = true;
        }
        
        if (Behavior == true)
        {
            //時間計測
            BehaviorTime += Time.deltaTime;
            //動いているかの確認
            Debug.Log(BehaviorTime);
        }

        if (BehaviorTime >= 2)
        {
            //レイヤー名をNomalに変更
            gameObject.layer = LayerMask.NameToLayer("Nomal");
            //時間初期化
            BehaviorTime = 0;
            //時間を止める
            Behavior = false;
        }

        
        if (Input.GetKeyDown(KeyCode.S))
        {
            //レイヤー名をGetOffに変更
            gameObject.layer = LayerMask.NameToLayer("GetOff");
            //時間を動かす
            Behavior = true;
        }

        if (Behavior == true)
        {
            //時間計測
            BehaviorTime += Time.deltaTime;
            //動いているかの確認
            Debug.Log(BehaviorTime);
        }

        if (BehaviorTime >= 1)
        {
            //レイヤー名をNomalに変更
            gameObject.layer = LayerMask.NameToLayer("Nomal");
            //時間初期化
            BehaviorTime = 0;
            //時間を止める
            Behavior = false;
        }
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
    }
}