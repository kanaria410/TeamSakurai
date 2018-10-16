using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//川瀬
public class characterController : MonoBehaviour {

    private Vector3 Player_pos;      //プレイヤーのポジション
    private int jumpcount;           //ジャンブした回数をカウント
    public Rigidbody rb;             //body獲得      
    public float jumpPower;          //ジャンプ力
    public float speed;              //移動スピード
    private float x;         

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

    void Update ()
    {
        //if (Input.GetKey("right"))
        //{
        //    transform.position += transform.right * speed * Time.deltaTime;
        //    Debug.Log("右に動く");
        //}

        //if (Input.GetKey("left"))
        //{
        //    transform.position -= transform.right * speed * Time.deltaTime;
        //    Debug.Log("左に動く");
        //
        x = Input.GetAxis("Horizontral");

        rb.velocity = new Vector3(z * speed, 0);
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

