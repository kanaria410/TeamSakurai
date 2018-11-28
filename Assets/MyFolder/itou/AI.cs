using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    const int
        IDLE = 1,       //アイドル状態
        WALK = 2,       //歩く状態
        ATTACK = 3,     //戦闘状態
        END = 100;      //セミコロンを書くのがめんどくさかったので適当に大きな値を用意しました
    const int STATE_NUMBER = 10;    //本来はステート(状態)の数だけでいい　今は適当に１０
    int state;          //状態
    int key;            //方向転換にKey
    int[] frameCount = new int[STATE_NUMBER];   //各ステート内で使う　フレームをカウントする
    [SerializeField]
    float speed;        //スピード（現在は加速度）
    [SerializeField]
    GameObject target;  //ターゲット
    Rigidbody rigid;    //リジッドボディ

    //戦闘状態内で使っている変数
    bool attackFlag;

    //アイドル
    void Idle()
    {
        //このフレーム数経過するとステートを移行
        int frame = 0;

        if (frameCount[IDLE] >= frame)
        {
            state = WALK;

            frameCount[IDLE] = 0;
        }

        //フレームをカウント
        frameCount[IDLE]++;
    }

    //歩く
    void Walk()
    {
        Distance();

        //これ以上の速度はでない
        float maxVeloctiy = 4;

        //加速度がmaxVeloctiyを超えることはない
        if (Mathf.Abs(rigid.velocity.x) < maxVeloctiy)
        {
            //移動
            if (Distance().x > 0)
            {
                rigid.AddForce(Vector3.left * speed, ForceMode.Acceleration);
                key = -1;
            }
            else
            {
                rigid.AddForce(Vector3.right * speed, ForceMode.Acceleration);
                key = 1;
            }
        }
    }

    //戦闘
    void Attack()
    {
        //敵が攻撃範囲に入ってきてから何フレーム攻撃しないか
        int frame = 30;

        //指定したフレーム数を超えたら攻撃
        if (attackFlag && frameCount[ATTACK] > frame)
        {
            Rigidbody rigid = target.GetComponent<Rigidbody>();

            Vector3 force;

            if (Distance().x < 0)
            {
                force = new Vector3(10.0f, 10.0f, 0);
            }
            else
            {
                force = new Vector3(-10.0f, 10.0f, 0);
            }

            //飛ばす
            rigid.AddForce(force, ForceMode.Impulse);

            frameCount[ATTACK] = 0;
        }

        if (attackFlag)
        {
            frameCount[ATTACK]++;
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
            case IDLE:
                Idle();
                break;
            case WALK:
                Walk();
                break;
            case ATTACK:
                Attack();
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
        state = IDLE;
        attackFlag = false;
	}

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.LogError("現在の状態：" + state);
        }

        //動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //ステートマシーン関数
        StateMachine();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == target.tag)
        {
            attackFlag = true;
            state = ATTACK;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == target.tag)
        {
            attackFlag = false;
            state = WALK;
            frameCount[ATTACK] = 0;
        }
    }
}
