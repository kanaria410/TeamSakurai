using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed;
    Rigidbody rigid;

    void Move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        { 
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
    }

    void Jump()
    {
        float jumpPower = 10.0f;
        rigid.AddForce(new Vector3(0, jumpPower * 45.0f, 0));
        //jumpcount++;
    }

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(gameObject);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        Move();
    }

    private void OnTriggerStay(Collider other)
    {
        //超仮の攻撃
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.Return))
        {
            //衝突してきたオブジェクトのリジッドボディを取得
            Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();

            //自分と衝突してきたオブジェクトの距離の差を計算
            Vector3 dis = other.gameObject.transform.position - transform.position;

            Vector3 force;

            //自分の右にいれば右に、左にいれば左に飛ばす
            if (dis.x > 0)
            {
                force = new Vector3(10.0f, 10.0f, 0);
            }
            else
            {
                force = new Vector3(-10.0f, 10.0f, 0);
            }

            //飛ばす
            rigid.AddForce(force, ForceMode.Impulse);
        }
    }
}
