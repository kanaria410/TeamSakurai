using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//川瀬
public class characterController : MonoBehaviour {

    private int jumpcount;           //ジャンブした回数をカウント
    public Rigidbody rb;             //body獲得      
    public float jumpPower;          //ジャンプ力
    public float speed;              //移動スピード
    int key;                         //方向転換に使う
    float jumpTime;                  //ジャンプする時間
    bool jumpOn;                     //ジャンプボタンが押されたか確認

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
    }

    void CharacterAttack()
    {
        //以下通常攻撃
        if (Input.GetKeyDown("w"))//□弱攻撃
        {
            Debug.Log("(´･ω･`)");
        }

        if (Input.GetKeyDown("d"))//△強攻撃
        {
            Debug.Log("(*´ω｀*)");
        }

        if (Input.GetKeyDown("a"))//〇スマッシュ
        {
            Debug.Log("(#ﾟДﾟ)ｺﾞﾗｧ!!!!");
        }
        //以下派生攻撃
        if (Input.GetKey("q") && Input.GetKeyDown("left"))
        {
            Debug.Log("俺の最弱はちっとばっか響くぞ!!!!");
        }

        if (Input.GetKey("q") && Input.GetKeyDown("right"))
        {
            Debug.Log("死んでもいいゲームなんて、ヌル過ぎるぜ");
        }

        if (Input.GetKey("q") && Input.GetKeyDown("up"))
        {
            Debug.Log("青春を楽しむ愚か者ども…砕け散れ");
        }

        if (Input.GetKey("q") && Input.GetKeyDown("down"))
        {
            Debug.Log("やめてよね、本気で喧嘩したらサイが僕にかなうはずないだろ");
        }
    }

    void Update ()
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

