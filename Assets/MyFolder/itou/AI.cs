using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    const int
        IDLE = 1,       //アイドル状態
        WALK = 2,       //歩く状態
        END = 100;      //セミコロンを書くのがめんどくさかったので適当に大きな値を用意しました
    int state;          //状態
    [SerializeField]
    float speed;        //スピード（現在は加速度）
    float distance;     //自分とターゲットの距離
    GameObject target;  //ターゲット
    Rigidbody rigid;    //リジッドボディ

    //アイドル
    void Idle()
    {
        //歩く状態に移行
        state = WALK;
    }

    //歩く
    void Walk()
    {
        //これ以上の速度はでない
        float maxVeloctiy = 4;

        //加速度がmaxVeloctiyを超えることはない
        if (Mathf.Abs(rigid.velocity.x) < maxVeloctiy)
        {
            //移動
            if (Distance().x > 0)
            {
                rigid.AddForce(Vector3.left * speed, ForceMode.Acceleration);
            }
            else
            {
                rigid.AddForce(Vector3.right * speed, ForceMode.Acceleration);
            }
        }
    }

    //自分とプレイヤーの距離
    Vector3 Distance()
    {
        //自分とターゲットの距離を計算
        Vector3 distance = transform.position - target.transform.position;
        return distance;
    }

    //ステートマシーン
    void StateMachine()
    {
        switch (state)
        {
            case 1:
                Idle();
                break;
            case 2:
                Walk();
                break;
            default:
                break;
        }
    }

    void Start ()
    {
        //リジッドボディを取得
        rigid = GetComponent<Rigidbody>();

        //プレイヤーをターゲットに指定
        target = GameObject.FindGameObjectWithTag("Player");

        //初期化
        distance = 0;
        state = IDLE;
	}
	
	void Update ()
    {
        //ステートマシーン関数
        StateMachine();
	}
}
