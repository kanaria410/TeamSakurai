using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//必要なコンポ―ネットを定義
[RequireComponent(typeof(Rigidbody))]
public class CharacterMove : MonoBehaviour
{
    [SerializeField, Header("速さ")]
    float speed;
    [SerializeField, Header("ジャンプ力")]
    float jumpForce;
    float jumpCountr;       //何回ジャンプしたか
    float direction;        //向き
    float defaultDirection; //ゲーム開始時にどの方向を向いていたか
    bool attackFlag;        //攻撃していいかダメかのフラグ
    [SerializeField,Header("攻撃時にどの軸にどのくらいの力をかけるか")]
    Vector2 attackForce;
    Animator animator;      //アニメータ
    Rigidbody _rigidbody;   //リジッドボディ
    Rigidbody enemyRigidbody;   //敵のリジッドボディ

    void RayMove()
    {
        //Rayの原点
        Vector3 sensorPosition = new Vector3(transform.position.x, transform.position.y + 5.0f, transform.position.z);

        //Rayの作成
        //第一引数：Rayを飛ばす原点
        //第二引数：Rayを飛ばす方向
        Ray attackSensor = new Ray(sensorPosition, transform.forward);

        //Rayが当たったオブジェクトの情報を入れる
        RaycastHit attackSensorHit;

        //敵のマスクのみ検知するためのレイヤーマスク
        int layerMask = 1 << 10;

        //各Rayの長さ
        float attackSensorDis = 2.0f;

        //敵検知センサー
        if (Physics.Raycast(attackSensor, out attackSensorHit, attackSensorDis, layerMask))
        {
            attackFlag = true;
            enemyRigidbody = attackSensorHit.collider.GetComponent<Rigidbody>();
        }
        else
        {
            attackFlag = false;
            enemyRigidbody = null;
        }

        //Rayを可視化
        Debug.DrawRay(attackSensor.origin, attackSensor.direction * attackSensorDis, Color.white, 0, true);
    }

    //移動関数
    void Walk()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            animator.SetBool("Walk", true);
            transform.Translate(0, 0, speed * Time.deltaTime);
            direction = defaultDirection;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            animator.SetBool("Walk", true);
            transform.Translate(0, 0, speed * Time.deltaTime);
            direction = -defaultDirection;
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        //Debug.Log("移動");
    }

    //方向転換関数
    void Direction()
    {
        transform.localRotation = Quaternion.Euler(0, direction, 0);
    }

    //ジャンプ関数
    void Jump()
    {
        const int maxJumpValue = 2; //ジャンプの最大回数

        if (Input.GetButtonDown("Jump") && jumpCountr < maxJumpValue)
        {
            animator.SetTrigger("Jump");
            _rigidbody.AddForce(Vector3.up * jumpForce);
            jumpCountr++;
        }
    }

    //攻撃関数
    void Attack()
    {
        bool isReady = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Attack"));

        if (Input.GetButtonDown("Attack"))
        {
            animator.SetTrigger("_Attack");
        }

        if (attackFlag && isReady)
        {
            enemyRigidbody.AddForce(((transform.forward * attackForce.x) + (Vector3.up * attackForce.y)) * Time.deltaTime);
            attackFlag = false;
            enemyRigidbody = null;
        }
    }

    // Use this for initialization
    void Start()
    {
        //初期化
        jumpCountr = 0;
        attackFlag = false;
        animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        enemyRigidbody = null;
        direction = transform.localEulerAngles.y;
        defaultDirection = direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GlobalDate._STOP_FLAG)
        {
            RayMove();
            Attack();
            Walk();
            Direction();
            Jump();
            //Debug.Log("何で動くの");
        }
        //Debug.Log("(´･ω･｀)");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCountr = 0;
            animator.SetBool("Jump", false);
            //animator.SetBool("Walk", true);
        }
    }
}