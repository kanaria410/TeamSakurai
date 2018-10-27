using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    const int sensorNumbar = 2; //自身についているセンサーの数
    int state;                  //状態
    int[] frameCount = new int[10]; //何フレーム経ったか　要素数は適当に多く取っただけです
    int jumpCount;              //何回ジャンプしたか
    int key;                    //方向転換に使う
    [SerializeField, Header("スピ―ド")]
    float speed;                //スピード
    [SerializeField, Header("どれくらい距離をとるか")]
    float targetOfDistance;
    [SerializeField, Header("Rayの長さ")]
    float distance;             //Rayの長さ
    bool attakFlag;             //攻撃をしていいかダメかを判断するフラグ
    Vector3 dis;                //差
    Rigidbody rigid;            //リジッドボディ
    GameObject target;          //攻撃対象

    GameObject[] sensor = new GameObject[sensorNumbar];     //0が右　1が左のセンサー
    bool[] sensorFlag = new bool[sensorNumbar];             //センサーが検知した情報を得る

    const int
        IDLE = 1,
        WALK = 2,
        BACK = 3,
        ATTACK = 4,
        END = 100;  //いちいちセミコロン書くのがめんどくさかったのでENDという謎めいたものを作成

    void RayMove()
    {
        Vector3 angle = new Vector3(90, -90, 0);
        Vector3 angle_ = new Vector3(-90, -90, 0);

        //Rayの作成　　　　　　　↓Rayを飛ばす原点　　　↓Rayを飛ばす方向
        Ray ray = new Ray(transform.position, angle);
        Ray ray_ = new Ray(transform.position, angle_);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit hit;

        //もしRayにオブジェクトが衝突したら
        //                  ↓Ray  ↓Rayが当たったオブジェクト ↓距離
        if (Physics.Raycast(ray, out hit, distance))
        {
            if(hit.collider.gameObject.tag != gameObject.tag 
                && hit.collider.gameObject.tag != "Player")
            {
                Debug.LogError(hit.collider.gameObject.name);
            }
        }

        Color red = Color.red;
        float time = 0.0f;
        Debug.DrawRay(transform.position, ray.direction * distance, red, time, true);
        Debug.DrawRay(transform.position, ray_.direction * distance, red, time, true);
    }


    //待機
    void Idle()
    {
        //何フレーム待機するか
        int frame = 10;

        //30フレーム経過したら歩き始める
        if (frameCount[IDLE] > frame)
        {
            state = WALK;
            frameCount[IDLE] = 0;   //リセット
        }

        frameCount[IDLE]++;
    }

    //歩く
    void Walk()
    {
        //プレイヤーと自分の距離の差を計算
        dis = transform.position - target.transform.position;

        //30フレームたったら待機モードに入るための抽選をする
        if (frameCount[WALK] >= 30)
        {
            //カウンターをリセット
            frameCount[WALK] = 0;

            //抽選をする
            int random = Random.Range(0, 10);

            //この値以下なら待機モードに移行
            int randomMax = 3;

            //抽選をした値が３未満だったら待機モードに移行
            if (random < randomMax)
            {
                state = BACK;
            }
        }

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

        //フレームカウンターをインクリメント
        frameCount[WALK]++;
    }

    //後ずさる
    void Back()
    {
        //プレイヤーと自分の距離の差を計算
        dis = transform.position - target.transform.position;

        int frame = Random.Range(10, 20);

        if (frameCount[BACK] > frame)
        {
            state = IDLE;
            frameCount[BACK] = 0;
        }

        //後ずさる
        if (dis.x > 0)
        {
            rigid.AddForce(Vector3.left * speed, ForceMode.Acceleration);
        }
        else
        {
            rigid.AddForce(Vector3.right * speed, ForceMode.Acceleration);
        }

        frameCount[BACK]++;
    }

    void Attack()
    {
        int frame = 30;

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

    void Jump()
    {
        float jumpPower = 10.0f;
        rigid.AddForce(new Vector3(0, jumpPower * 45.0f, 0));
        jumpCount++;
    }

    // Use this for initialization
    void Start()
    {
        //Playerというタグが付いているオブジェクトを取得
        target = GameObject.FindWithTag("Player");

        //自身についているRigidbodyを取得
        rigid = GetComponent<Rigidbody>();

        ////自身についているセンサーを取得
        //for (int i = 0; i < sensorNumbar; i++)
        //{
        //    //子オブジェクトを上から順に取得
        //    sensor[i] = transform.GetChild(i).gameObject;

        //    sensorFlag[i] = false;  //fakseで初期化
        //}

        //初期化
        state = 0;
        jumpCount = 0;

        //初期化
        for (int i = 0; i < frameCount.Length; i++)
        {
            frameCount[i] = 0;
        }
    }

    void FixedUpdate()
    {
        RayMove();

        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Test_AI");
        }

        if (target != null)
        {
            //動く方向に応じて反転
            if (key != 0)
            {
                transform.localScale = new Vector3(key, 1, 1);
            }

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
                    Attack();
                    Debug.Log("攻撃");
                    break;
                default:
                    //待機状態
                    state = IDLE;
                    break;
            }
        }
        else
        {
            ////ターゲットを再取得
            //target = GameObject.FindWithTag("Player");

            ////安定の無限ループ
            //while(target.name == gameObject.name)
            //{
            //    target = GameObject.FindWithTag("Player");
            //}

            //if (target == null)
            //{
            //    Debug.Log("(´･ω･｀)");
            //}
        }

        ////センサーが地面に触れてるかどうかを調べる
        //for (int i = 0; i < sensorNumbar; i++)
        //{
        //    sensorFlag[i] = sensor[i].GetComponent<SensorMove>().Get_HitFlag;
        //}
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = ATTACK;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            state = IDLE;
            frameCount[ATTACK] = 0;
        }
    }
}
