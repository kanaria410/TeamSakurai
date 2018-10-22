using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    const int sensorNumbar = 2; //自身についているセンサーの数
    int state;                  //状態
    int[] frameCount = new int[10]; //何フレーム経ったか　要素数は適当に多く取っただけです
    int jumpCount;              //何回ジャンプしたか
    [SerializeField, Header("スピ―ド")]
    float speed;                //スピード
    [SerializeField, Header("どれくらい距離をとるか")]
    float targetOfDistance;
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
        END = 100;  //いちいちセミコロン書くのがめんどくさかったのでENDという謎めいたものを作成

    //待機
    void Idle()
    {
        //30フレーム経過したら歩き始める
        if (frameCount[IDLE] > 30)
        {
            state = WALK;
            frameCount[IDLE] = 0;
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
                state = IDLE;
            }
        }

        //移動
        if (dis.x > 0)
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }

        //フレームカウンターをインクリメント
        frameCount[WALK]++;
    }

    //後ずさる
    void Back()
    {
        //プレイヤーと自分の距離の差を計算
        dis = transform.position - target.transform.position;

        //後ずさる
        if (dis.x > 0)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= transform.right * speed * Time.deltaTime;
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (target != null)
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
        if (other.gameObject.tag == "Player" && Time.frameCount % 30 == 0)
        {
            Rigidbody rigid = other.gameObject.GetComponent<Rigidbody>();

            Vector3 dis = other.gameObject.transform.position - transform.position;

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
        }
    }
}
