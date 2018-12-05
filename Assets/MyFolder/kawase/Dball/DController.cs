using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DController : MonoBehaviour {

    public Rigidbody rb;            //body獲得
    public float jumpPower;         //ジャンプ力
    public float speed;             //移動スピード
    private int jumpcount;          //ジャンブした回数をカウント
    private Animator animetor;      //アニメーションを使うよ

    float jumpTime;                 //ジャンプする時間
    bool jumpOn;                    //ジャンプボタンが押されたか確認
    Vector2 x1;                     //十字
    Vector2 x2;                     //スティック
    public bool ExitGround { get; set; }

    void Start()
    {
        animetor = GetComponent<Animator>();
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpcount < 2)
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
        //十字キー
        x1 = new Vector3(Input.GetAxis("Horizontal"), Vector3.zero.y);
        rb.AddForce(x1 * speed, ForceMode.Force);

        //スティック
        x2 = new Vector3(Input.GetAxis("Horizontal2"), Vector3.zero.y);
        rb.AddForce(x2 * speed, ForceMode.Force);

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


        if (x2.magnitude > 0.1f)
        {
            animetor.SetFloat("speed", x2.magnitude);
            transform.rotation = Quaternion.LookRotation(x2);
            Debug.Log("aa");
        }
        else
        {
            animetor.SetFloat("speed", 0f);
        }

        //ここら辺に回避
        if (Input.GetButton("Avoidance"))
        {
            Debug.Log("(ﾟ∀ﾟ)");
        }

        //ガード
        if (Input.GetButton("Guard"))
        {
            animetor.SetTrigger("GuardTrigger");
            Debug.Log("( *´艸｀)");
        }
        else
        {
            if (Input.GetButtonUp("Guard"))
            {
                animetor.SetBool("isGuard", false);
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
                Debug.Log("(´･ω･`)");
            }

            if (Input.GetButtonDown("Punch2"))//△強攻撃
            {
                animetor.SetTrigger("StrongTrigger");
                Debug.Log("(*´ω｀*)");
            }

            if (Input.GetButtonDown("Smash"))//〇スマッシュ
            {
                animetor.SetTrigger("SmashTrigger");
                Debug.Log("(#ﾟДﾟ)ｺﾞﾗｧ!!!!");
            }
        }
        //空中派生攻撃
        if (ExitGround == true)
        {
            Debug.Log("あああああああああああ");
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
            Debug.Log("下派生");
        }

        //覚醒
        if (Input.GetButtonDown("Awakening"))
        {
            Debug.Log("(｀・ω・´)");
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
