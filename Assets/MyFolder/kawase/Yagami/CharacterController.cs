using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private Animator animetor;      //アニメーションを使うよ
    public float speed = 10.0f;
    public Rigidbody rb;
    float jumpTime;
    private int jumpcount;
    public float jumpPower;
    private bool jumpOn;
    private bool ExitGround;

    //地面から離れたとする距離
    public float distanceToTheGround;
    //着地アニメーションへ変わる地面からの距離
    public float distanceToLanding;
    //地面との距離を計るためにこの位置からレイを飛ばす
    public Transform shoes;
    //プレイヤーの飛ばす力
    public float PlayerPower;

    Rigidbody enemyrb;

    public Animator rootAnimator;
    AnimatorStateInfo animatorStateInfo;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            jumpcount = 0;
            Debug.Log("初期化済み");
            ExitGround = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        rootAnimator.Update(0);
        animatorStateInfo = rootAnimator.GetCurrentAnimatorStateInfo(0);

        if (other.gameObject.tag == "enemy")
        {
            Debug.Log("あ");
           enemyrb  = other.gameObject.GetComponent<Rigidbody>();
            if (enemyrb)
            {
                if (animatorStateInfo.IsName("WeakAttack3"))
                {
                    enemyrb.AddForce(transform.root.forward * PlayerPower, ForceMode.Impulse);
                    Debug.Log("た");
                }
                else if (animatorStateInfo.IsName("Smash"))
                {
                    enemyrb.AddForce(transform.root.forward * PlayerPower, ForceMode.Impulse);
                    Debug.Log("る");
                }

                else if (animatorStateInfo.IsName("StrongAttack"))
                {
                    enemyrb.AddForce(transform.root.forward * PlayerPower, ForceMode.Impulse);
                }

            }
        }
    }
    



    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpcount < 1)
        {
            animetor.SetTrigger("JumpTrigger");
            rb.AddForce(new Vector3(0, jumpPower * 45f, 0));
            gameObject.layer = LayerMask.NameToLayer("Jump");
            jumpcount++;
            jumpOn = true;
            ExitGround = true;
        }

        if (jumpOn == true)
        {
            jumpTime += Time.deltaTime;
        }
        if (jumpTime >= 1)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            jumpTime = 0f;//タイムの初期化
        }
    }
    void PlayerMove()
    {
        //ここから一つ目のif文までがUnityちゃんが移動する方向に向く処理
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = cameraForward * Input.GetAxis("Vertical") +
                Camera.main.transform.right * Input.GetAxis("Horizontal");

        if (direction.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
        {
            animetor.SetFloat("speed", direction.magnitude);
            transform.rotation = Quaternion.LookRotation(direction);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
        }
        else
        {
            animetor.SetFloat("speed", 0f);
        }

        //if (ExitGround == false)
        //{
        //    animetor.SetBool("isFall", false);
        //}
        ////　アニメーションパラメータFallがfalseの時で地面との距離が遠かったらFallをtrueにする
        //else if (!animetor.GetBool("isFall"))
        //{
        //    Debug.DrawLine(shoes.position, shoes.position + Vector3.down * distanceToTheGround, Color.red);
        //    if (!Physics.SphereCast(new Ray(shoes.position, Vector3.down), distanceToTheGround, LayerMask.GetMask("Field")))
        //    {
        //        animetor.SetBool("isFall", true);
        //    }
        //}
        ////　落下アニメーションの時はレイを飛ばし地面との距離が近かったら着地アニメーションにする
        //else if (animetor.GetBool("isFall"))
        //{
        //    Debug.DrawLine(shoes.position, shoes.position + Vector3.down * distanceToLanding, Color.green);
        //    if (Physics.Linecast(shoes.position, shoes.position + Vector3.down * distanceToLanding, LayerMask.GetMask("Field")))
        //    {
        //        animetor.SetBool("isLanding", true);
        //    }
        //}

    }
    void PlayerAttack()
    {
        if (ExitGround == false)
        {
            //以下通常攻撃
            if (Input.GetButtonDown("Punch1"))//□弱攻撃
            {
                animetor.SetTrigger("WeakAttackTrigger");
            }

            if (Input.GetButtonDown("Punch2"))//△強攻撃
            {
                animetor.SetTrigger("StrongTrigger");
            }

            if (Input.GetButtonDown("Smash"))//〇スマッシュ
            {
                animetor.SetTrigger("SmashTrigger");
                //enemyrb.AddForce(transform.root.forward * PlayerPower, ForceMode.Impulse);
            }
        }

    }
    void Start()
    {
        animetor = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        shoes = GetComponent<Transform>();

    }


    void Update()
    {
        //Jump();
        PlayerMove();
        PlayerAttack();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;
        rb.AddForce(x, 0, z);
    }


}
