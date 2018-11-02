using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YagamiController : MonoBehaviour
{
    //八神唯
    public Rigidbody rb;            //body獲得
    public float jumpPower;         //ジャンプ力
    public float speed;             //移動スピード
    private int jumpcount;          //ジャンブした回数をカウント

    int key;                        //方向転換に使う
    int OffensivePower;             //プレイヤーの攻撃力？
    int DefensePower;               //プレイヤーの防御力？

    float jumpTime;                 //ジャンプする時間
    bool jumpOn;                    //ジャンプボタンが押されたか確認
    bool Awakening;                 //覚醒スイッチ

    void Start()
    {
    }

    void Jump()
    {
        if (Input.GetKeyDown("x") && jumpcount < 2)
        {
            rb.AddForce(new Vector3(0, jumpPower * 45.0f, 0));
            gameObject.layer = LayerMask.NameToLayer("Jump");
            jumpcount++;
            jumpOn = true;
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
        //キャラ移動
        if (Input.GetKey("right"))
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Acceleration);
            key = 1;
        }

        if (Input.GetKey("left"))
        {
            rb.AddForce(Vector3.left * speed, ForceMode.Acceleration);
            key = -1;

        }

        //動く方向に応じて反転
        if (key != 0)
         {
            transform.localScale = new Vector3(key, 1, 1);
         }
        
        //ここら辺に回避
        if (Input.GetKeyDown("e"))
        {
            Debug.Log("(ﾟ∀ﾟ)");
        }
    }


    void CharacterAttack()
    {
        //以下通常攻撃
        if (Input.GetKeyDown("a"))//□弱攻撃
        {
            Debug.Log("(´･ω･`)");
        }

        if (Input.GetKeyDown("w"))//△強攻撃
        {
            Debug.Log("(*´ω｀*)");
        }

        if (Input.GetKeyDown("d"))//〇スマッシュ
        {
            Debug.Log("(#ﾟДﾟ)ｺﾞﾗｧ!!!!");
        }
        //以下派生攻撃
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("Neutral");
        }

        if (Input.GetKey("q") && Input.GetKeyDown("right")
         || Input.GetKey("q") && Input.GetKeyDown("left"))
        {
            Debug.Log("左右派生");
        }


        if (Input.GetKey("q") && Input.GetKeyDown("up"))
        {
            Debug.Log("上派生");

        }

        if (Input.GetKey("q") && Input.GetKeyDown("down"))
        {
            Debug.Log("下派生");
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
        }
    }
}
