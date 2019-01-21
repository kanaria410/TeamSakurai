using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    const int
        IDEL = 0,   //アイドル状態
        WALK = 1,   //歩行状態
        ATTACK = 2, //攻撃状態
        AVOID = 3,  //後ずさり
        END = 100;  //意味なし
    int state;      //状態
    int random;     //ランダムで値を決める変数
    [SerializeField, Header("速さ")]
    float speed;
    [SerializeField, Header("ジャンプ力")]
    float jumpForce;
    float jumpCountr;       //何回ジャンプしたか
    float time;             //何かの時間
    float avoidTime;        //何秒後ずさるか
    float attackTime;       //何秒後に攻撃するか
    bool directionFlag;     //方向転換を一度だけしたいからboolで管理
    bool avoidFlag;         //Avoid関数内で使用する
    bool attackFlag;
    [SerializeField, Header("攻撃時にどの軸にどのくらいの力をかけるか")]
    Vector2 attackForce;
    Animator animator;      //アニメータ
    Rigidbody _rigidbody;   //リジッドボデ
    Rigidbody enemyRigidbody;   //敵のリジッドボディ
    GameObject target;      //何を追うか

    void RayMove()
    {
        //Rayの原点
        Vector3 sensorPosition = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);

        //Rayの作成
        //第一引数：Rayを飛ばす原点
        //第二引数：Rayを飛ばす方向
        Ray rayRight = new Ray(sensorPosition, new Vector3(90, -180, 0));
        Ray rayLeft = new Ray(sensorPosition, new Vector3(-90, -180, 0));
        Ray attackSensor = new Ray(sensorPosition, transform.forward);

        //Rayが当たったオブジェクトの情報を入れる箱
        RaycastHit rightHit;
        RaycastHit leftHit;
        RaycastHit attackSensorHit;

        //Playerのマスクのみ検知するためのレイヤーマスク
        int layerMask = 1 << 9;

        //自分のレイヤーとだけ衝突しない
        int _layerMask = ~(1 << 10);

        //各Rayの長さ
        float distance = 10.0f;
        float attackSensorDis = 2.0f;

        //右のRay
        if (!Physics.Raycast(rayRight, out rightHit, distance, _layerMask))
        {
            state = WALK;
            time = 0;
            directionFlag = true;
            avoidFlag = true;
            //Debug.Log("危険を回避");
        }

        //左のRay
        if (!Physics.Raycast(rayLeft, out leftHit, distance, _layerMask))
        {
            state = WALK;
            time = 0;
            directionFlag = true;
            avoidFlag = true;
            //Debug.Log("危険を回避");
        }

        //攻撃範囲に敵がいるかセンサーのRay
        if (Physics.Raycast(attackSensor, out attackSensorHit, attackSensorDis, layerMask))
        {
            enemyRigidbody = attackSensorHit.collider.GetComponent<Rigidbody>();
            state = ATTACK;
            //Debug.Log("敵を検知");
        }
        else
        {
            //このRayに敵が当たってなければ敵のリジッドボディ情報を破棄
            enemyRigidbody = null;
            attackFlag = true;
        }

        //Rayを可視化
        //Debug.DrawRay(rayRight.origin, rayRight.direction * distance, Color.red, 0, true);
        //Debug.DrawRay(rayLeft.origin, rayLeft.direction * distance, Color.blue, 0, true);
        //Debug.DrawRay(attackSensor.origin, attackSensor.direction * attackSensorDis, Color.green, 0, true);
    }

    //アイドル状態
    void Idel()
    {
        state = WALK;
        time = 0;
    }

    //移動関数
    void Walk()
    {
        time += Time.deltaTime;

        if (time >= 1)
        {
            //後ずさり抽選会
            random = Random.Range(1, 6);

            if (random == 1)
            {
                time = 0;
                state = AVOID;
            }
            else
            {
                time = 0;
            }
        }

        //プレイヤーが一定以上上にいるなら待機
        if (target.transform.position.y < 2)
        {
            animator.SetBool("Walk", true);
            transform.Translate(0, 0, speed * Time.deltaTime);
            Direction();
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    //方向転換関数
    void Direction()
    {
        Vector3 targetPos = target.transform.position;
        // ターゲットのY座標を自分と同じにすることで2次元に制限する。
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
    }

    //ジャンプ関数
    void Jump()
    {
        const int maxJumpValue = 2; //ジャンプの最大回数

        if (jumpCountr < maxJumpValue)
        {
            animator.SetTrigger("Jump");
            _rigidbody.AddForce(Vector3.up * jumpForce * Time.deltaTime);
            jumpCountr++;
        }
    }

    //攻撃関数
    void Attack()
    {
        //if (attackFlag)
        //{
        //    attackTime = Random.Range(0.1f, 0.5f);
        //    //attackTime = Random.Range(1.0f, 1.5f);
        //    //attackTime = Random.Range(10.0f, 10.5f);
        //    animator.SetBool("Walk", false);
        //    attackFlag = false;
        //}

        //if (time > attackTime)
        //{
        animator.SetTrigger("_Attack");
        bool isReady = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Attack"));
        if (isReady && !animator.GetBool("Jump") && enemyRigidbody)
        {
            attackFlag = true;
            enemyRigidbody.AddForce(((transform.forward * attackForce.x) + (Vector3.up * attackForce.y)) * Time.deltaTime);
            time = 0;
            state = IDEL;
        }
        state = IDEL;
        //}

        //time += Time.deltaTime;
    }

    //後ずさり関数
    void Avoid()
    {
        time += Time.deltaTime;

        if (avoidFlag)
        {
            avoidTime = Random.Range(0.5f, 1.0f);
            //デバッグ用の10.0f　10秒間後ずさる
            //avoidTime = 10.0f;
            avoidFlag = false;
        }

        if (target.transform.position.y > 2)
        {
            state = WALK;
            avoidFlag = true;
            return;
        }

        if (time <= avoidTime)
        {
            if (directionFlag)
            {
                transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y * -1.0f, 0);
                //Debug.LogError("方向転換");
                directionFlag = false;
            }
            animator.SetBool("Walk", true);
            transform.Translate(0, 0, speed * Time.deltaTime);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y * -1.0f, 0);
            time = 0;
            directionFlag = true;
            avoidFlag = true;
            state = WALK;
        }
    }

    // Use this for initialization
    void Start()
    {
        //初期化
        state = IDEL;
        jumpCountr = 0;
        time = 0;
        avoidTime = 0;
        directionFlag = true;
        avoidFlag = true;
        attackFlag = true;
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalDate._STOP_FLAG)
        {
            if (target.activeInHierarchy == false)
            {
                animator.SetBool("Walk", false);
                state = END;
                return;
            }

            RayMove();

            //ステートマシーン
            switch (state)
            {
                case IDEL:
                    Idel();
                    break;

                case WALK:
                    Walk();

                    break;

                case ATTACK:
                    Attack();
                    break;

                case AVOID:
                    Avoid();
                    break;

                default:
                    break;
            }
        }

        //Debug.Log("現在のステート" + state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCountr = 0;
            if (animator.GetBool("Jump"))
            {
                animator.SetBool("Walk", true);
            }
            animator.SetBool("Jump", false);
        }
    }
}