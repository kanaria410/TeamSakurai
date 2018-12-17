﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YagamiController : MonoBehaviour
{
    //八神唯
    public Rigidbody rb;            //body獲得
    public float jumpPower;         //ジャンプ力
    public float speed;             //移動スピード
    private int jumpcount;          //ジャンブした回数をカウント
    private Animator animetor;      //アニメーションを使うよ

    float jumpTime;                 //ジャンプする時間
    bool jumpOn;                    //ジャンプボタンが押されたか確認
    bool GuardOn;                   //Guard中かどうか
    Vector3 x1;                     //十字x
    Vector2 x2;                     //スティック
    public bool ExitGround { get; set; }
    
    //地面から離れたとする距離
    public float distanceToTheGround;
    //着地アニメーションへ変わる地面からの距離
    public float distanceToLanding;
    //地面との距離を計るためにこの位置からレイを飛ばす
    public Transform shoes;

    void Start()
    {
        animetor = GetComponent<Animator>();
        shoes = GetComponent<Transform>();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpcount < 1)
        {
            animetor.SetTrigger("JumpTrigger");
            rb.AddForce(new Vector3(0, jumpPower * 45f, 0));
            gameObject.layer = LayerMask.NameToLayer("Jump");
            jumpcount++;
            jumpOn = true;
            ExitGround = true;
        }

        if (jumpOn == true)
        {
            jumpTime += Time.deltaTime;
        }
        if (jumpTime >= 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            jumpTime = 0f;//タイムの初期化
        }
    }

    
    void CharacterMove()
    {
        if (ExitGround == false)
        {
            animetor.SetBool("isFall", false);
        }
        //　アニメーションパラメータFallがfalseの時で地面との距離が遠かったらFallをtrueにする
        else if (!animetor.GetBool("isFall"))
        {
            Debug.DrawLine(shoes.position, shoes.position + Vector3.down * distanceToTheGround, Color.red);
            if (!Physics.SphereCast(new Ray(shoes.position, Vector3.down), distanceToTheGround, LayerMask.GetMask("Field")))
            {
                animetor.SetBool("isFall", true);
            }
        }
        //　落下アニメーションの時はレイを飛ばし地面との距離が近かったら着地アニメーションにする
        else if (animetor.GetBool("isFall"))
        {
            Debug.DrawLine(shoes.position, shoes.position + Vector3.down * distanceToLanding, Color.green);
            if (Physics.Linecast(shoes.position, shoes.position + Vector3.down * distanceToLanding, LayerMask.GetMask("Field")))
            {
                animetor.SetBool("isLanding", true);
            }
        }

        //十字キー
        x1 = new Vector3(Input.GetAxis("Horizontal"), Vector3.zero.y);
        rb.AddForce(x1 * speed, ForceMode.Force);

        ////スティック
        //z1 = new Vector3(Input.GetAxis("Vertical"), Vector3.zero.y);
        //rb.AddForce(x2 * speed, ForceMode.Force);

        ////十字操作
        if (x1.magnitude > 0.1f)
        {
            animetor.SetFloat("speed", x1.magnitude);
            transform.rotation = Quaternion.LookRotation(x1);
            Debug.Log("aa");
        }
        else
        {
            animetor.SetFloat("speed", 0f);
        }
        ////十字操作
        //if (z1 > 0.1f)
        //{
        //    animetor.SetFloat("speed", z);
        //    transform.rotation = Quaternion.LookRotation(z1);
        //    Debug.Log("aa");
        //}
        //else
        //{
        //    animetor.SetFloat("speed", 0f);
        //}



        ////スティック操作
        //if (x2.magnitude > 0.1f)
        //{
        //   animetor.SetFloat("speed", x2.magnitude);
        //   transform.rotation = Quaternion.LookRotation(x2);
        //   Debug.Log("aa");
        // }
        // else
        // {
        //  animetor.SetFloat("speed", 0f);
        // }

        //回避
        if (Input.GetButton("Avoidance"))
        {
        }

        //ガード
        if (Input.GetButtonDown("Guard"))
        {
            animetor.SetBool("isGuard", true);
            GuardOn = true;
        }
        else
        {
            if (Input.GetButtonUp("Guard"))
            {
                animetor.SetBool("isGuard", false);
                GuardOn = false;
            }
        }

    }


    void CharacterAttack()
    {
        if (ExitGround == false)
        {
            //以下通常攻撃
            if (Input.GetButtonDown("Punch1"))//□弱攻撃
            {
                animetor.SetTrigger("WeakAttackTrigger");
            }

            if (Input.GetButtonDown("Punch2"))//△強攻撃
            {
                animetor.SetTrigger("StrongTrigger");
            }

            if (Input.GetButtonDown("Smash"))//〇スマッシュ
            {
                animetor.SetTrigger("SmashTrigger");
            }
        }
        //空中派生攻撃
        if (ExitGround == true)
        {
            //十字キー専用
            //空中左攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Horizontal") > 0)
            {
                animetor.SetTrigger("LeftAttackTrigger");
            }
            
            //空中右攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Horizontal") < 0)
            {
                animetor.SetTrigger("RightAttackTrigger");
            }
            
            //空中上攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Vertical") > 0)
            {
                animetor.SetTrigger("UpAttackTrigger");
            }

            //空中下攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Vertical") < 0)
            {
                animetor.SetTrigger("DownAttackTrigger");
            }

            //スティック専用
            //空中左攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Horizontal2") > 0)
            {
                animetor.SetTrigger("LeftAttackTrigger");
            }

            //空中右攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Horizontal2") < 0)
            {
                animetor.SetTrigger("RightAttackTrigger");
            }

            //空中上攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Vertical2") < 0)
            {
                animetor.SetTrigger("UpAttackTrigger");
            }

            //空中下攻撃
            if (Input.GetButton("Punch1") && Input.GetAxis("Vertical2") > 0)
            {
                animetor.SetTrigger("DownAttackTrigger");
            }
        }
        //必殺技
        if (Input.GetButtonDown("Deathblow"))
        {
            Debug.Log("Neutral");
        }

        //十字キー専用
        //派生左右
        if ((Input.GetButton("Deathblow") && Input.GetAxis("Horizontal") > 0)
         || Input.GetButton("Deathblow") && Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("左右派生");
        }

        //派生上
        if (Input.GetButton("Deathblow") && Input.GetAxis("Vertical") > 0)
        {
            Debug.Log("上派生");

        }

        //派生下
        if (Input.GetButton("Deathblow") && Input.GetAxis("Vertical") < 0)
        {
            Debug.Log("下派生");
        }
        
        //スティック専用
        //派生左右
        if ((Input.GetButton("Deathblow") && Input.GetAxis("Horizontal2") > 0)
         || Input.GetButton("Deathblow") && Input.GetAxis("Horizontal2") < 0)
        {
            Debug.Log("左右派生");
        }

        //派生上
        if (Input.GetButton("Deathblow") && Input.GetAxis("Vertical2") < 0)
        {
            Debug.Log("上派生");

        }

        //派生下
        if (Input.GetButton("Deathblow") && Input.GetAxis("Vertical2") > 0)
        {
            animetor.SetBool("isDownDB", true);
            Debug.Log("下派生");
        }

        //覚醒
        if (Input.GetButtonDown("Awakening"))
        {
            animetor.SetBool("isAwakening", true);
        }
    }

    void Update()
    {
        CharacterMove();
        Jump();
        CharacterAttack();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jumpcount = 0;
            Debug.Log("初期化済み");
            ExitGround = false;
        }
    }

}
