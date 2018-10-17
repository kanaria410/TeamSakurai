using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//川瀬
public class characterController : MonoBehaviour {

    private int jumpcount;           //ジャンブした回数をカウント
    public Rigidbody rb;             //body獲得      
    public float jumpPower;          //ジャンプ力
    public float speed;              //移動スピード
    int key;                          //方向転換に使う

    void Start()
    {
    }



    void Jump()
    {
        if (Input.GetKeyDown("up") && jumpcount < 2)
        {
            rb.AddForce(new Vector3(0, jumpPower * 45.0f, 0));
            jumpcount++;
        }
    }

   void CharacterMove()
    {
        if (Input.GetKey("right"))
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Acceleration);
            key = 1;
            Debug.Log("右に動く");
        }

        if (Input.GetKey("left"))
        {
            rb.AddForce(Vector3.left * speed, ForceMode.Acceleration);
            key = -1;
            Debug.Log("左に動く");
        }
        //動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }
    }

    void Update ()
    {
        CharacterMove();
        Jump();
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

