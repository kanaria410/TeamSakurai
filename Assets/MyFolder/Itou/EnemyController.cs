using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    const int arrayLength = 10; //配列の長さ　10は適当に少し多く取っただけです
    int state;                  //状態
    int[] frameCount = new int[arrayLength]; //何フレーム経ったか
    int jumpCount;              //何回ジャンプしたか
    int key;                    //方向転換に使う
    int rayFrameCount;
    int rayMoveFrameCount;
    [SerializeField, Header("Rayの長さ")]
    int distance;
    [SerializeField, Header("スピード")]
    float speed;                //スピード
    bool[] stateFlag = new bool[arrayLength];    //各ステート内で使うフラグ
    bool moveFlag;
    bool rayMoveFlag;
    bool turnaroundFlag;        //Turnaround：振り向く
    Vector3 dis;                //差
    Rigidbody rigid;            //リジッドボディ
    GameObject target;          //攻撃対象

    //状態を表している変数たち
    const int
        IDLE = 1,   //通常
        WALK = 2,   //歩く
        BACK = 3,   //後ずさる
        ATTACK = 4, //攻撃
        JUMP = 5,
        END = 100;  //いちいちセミコロン書くのがめんどくさかったのでENDという謎めいたものを作成

    void RayMove()
    {
        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray rayRight = new Ray(transform.position, new Vector3(90, -180, 0));
        Ray rayLeft = new Ray(transform.position, new Vector3(-90, -180, 0));

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit rightHit;
        RaycastHit leftHit;

        if (rayMoveFlag)
        {
            //右のRay
            if (!Physics.Raycast(rayRight, out rightHit, distance))
            {
                moveFlag = false;
                rayMoveFlag = false;
            }
           
            //左のRay
            if (!Physics.Raycast(rayLeft, out leftHit, distance))
            {
                moveFlag = false;
                rayMoveFlag = false;
            }
        }
        else
        {
            if (rayMoveFrameCount > 60)
            {
                moveFlag = true;
                rayMoveFlag = true;
                rayMoveFrameCount = 0;
            }         

            //一度だけ方向転換
            if (turnaroundFlag && key == 1)
            {
                key = -1;
                turnaroundFlag = false;
            }
            else if(turnaroundFlag && key ==- 1)
            {
                key = 1;
                turnaroundFlag = false;
            }

            //移動
            if (key == 1)
            {
                rigid.AddForce(Vector3.right * speed, ForceMode.Acceleration);
            }
            else
            {
                rigid.AddForce(Vector3.left * speed, ForceMode.Acceleration);
            }

            Debug.Log("ステートを無視して動く");

            //フレームをカウント
            rayMoveFrameCount++;
        }

        //Rayを可視化
        //Debug.DrawRay(rayRight.origin, rayRight.direction * distance, Color.red, 0, true);
        //Debug.DrawRay(rayLeft.origin, rayLeft.direction * distance, Color.red, 0, true);
    }

    //待機
    void Idle()
    {
        //何フレーム待機するか
        int frame = 20;
        
        //frameで設定したフレーム数経過したら歩き始める
        if (frameCount[IDLE] > frame)
        { 
            state = WALK;
            frameCount[IDLE] = 0;   //リセット
        }

        //カウンターをインクリメント
        frameCount[IDLE]++;
    }

    //歩く
    void Walk()
    {
        //プレイヤーと自分の距離の差を計算
        dis = transform.position - target.transform.position;

        //30フレームたったら待機モードに入るための抽選をする
        if (frameCount[WALK] >= 60)
        {
            //カウンターをリセット
            frameCount[WALK] = 0;

            //抽選をする
            int random = Random.Range(0, 10);

            //この値未満なら待機モードに移行
            int randomMax = 2;

            //抽選をした値がrandomMax未満だったら待機モードに移行
            if (random < randomMax)
            {
                state = IDLE;
                //state = BACK;
            }
        }

        //加速度が一定以上になったら止まる
        if (Mathf.Abs(rigid.velocity.x) < 4)
        {
            //移動
            if (dis.x > 0)
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

        //フレームカウンターをインクリメント
        frameCount[WALK]++;
    }

    //後ずさる
    void Back()
    {
        //プレイヤーと自分の距離の差を計算
        dis = transform.position - target.transform.position;

        //適当にランダムな値を取得
        int frame = Random.Range(20, 30);

        //ランダムに決まった値より経過したフレームが大きいならIDLEに移行
        if (frameCount[BACK] > frame)
        {
            state = IDLE;
            frameCount[BACK] = 0;   //フレームカウンターをリセット
        }

        //後ずさる
        //if (dis.x > 0)  
        //{
        //    rigid.AddForce(-Vector3.left * speed, ForceMode.Acceleration);
        //    key = 1;
        //}
        //else
        //{
        //    rigid.AddForce(-Vector3.right * speed, ForceMode.Acceleration);
        //    key = -1;
        //}

        //カウンターをインクリメント
        frameCount[BACK]++;
    }

    //攻撃
    void Attack()
    {
        //敵が攻撃範囲に入ってきてから何フレーム攻撃しないか
        int frame = 30;

        //指定したフレーム数を超えたら攻撃
        if (frameCount[ATTACK] > frame)
        {
            Rigidbody rigid = target.GetComponent<Rigidbody>();

            Vector3 dis = target.transform.position - transform.position;

            Vector3 force;

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

            frameCount[ATTACK] = 0;
        }

        frameCount[ATTACK]++;
    }

    //ジャンプ
    void Jump()
    {
        if(jumpCount < 2)
        rayMoveFlag = false;
        float jumpPower = 10.0f;
        rigid.AddForce(new Vector3(0, jumpPower * 45.0f, 0));
        jumpCount++;
        state = IDLE;
    }

    // Use this for initialization
    void Start()
    {
        //Playerというタグが付いているオブジェクトを取得
        target = GameObject.FindWithTag("Player");

        //自身についているRigidbodyを取得
        rigid = GetComponent<Rigidbody>();

        //初期化
        state = IDLE;
        jumpCount = 0;
        moveFlag = true;
        rayMoveFlag = true;
        turnaroundFlag = true;
        rayFrameCount = 0;
        rayMoveFrameCount = 0;

        //配列を初期化
        for (int i = 0; i < arrayLength; i++)
        {
            frameCount[i] = 0;
            stateFlag[i] = false;
        }
    }

    void　Update()
    {
        RayMove();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.LogError(state);
        }

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Test_AI");
        }

        //動く方向に応じて反転
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //ターゲットがいるとき
        if (target != null && moveFlag)
        {
            //ステートマシーン
            switch (state)
            {
                case 1:
                    Idle();
                    Debug.Log("待機中");
                    break;
                case 2:
                    Walk();
                    Debug.Log("歩いてます");
                    break;
                case 3:
                    Back();
                    Debug.Log("後ずさり中");
                    break;
                case 4:
                    //Attack();
                    Debug.Log("攻撃");
                    break;
                case 5:
                    Jump();
                    Debug.Log("線路へほっぴんじゃんぷ");
                    break;
                default:
                    //待機状態
                    state = IDLE;
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //攻撃範囲にプレイヤが入ってきたら攻撃状態に移行
        if (other.gameObject.tag == "Player")
        {
            state = ATTACK;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //プレイヤが攻撃範囲から出た場合、IDLE状態に移行
        if (other.gameObject.tag == "Player")
        {
            state = IDLE;
            frameCount[ATTACK] = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        jumpCount = 0;
        rayMoveFlag = true;
    }
}
