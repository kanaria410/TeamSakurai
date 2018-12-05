using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    float moveValueX = 0.1f;
    float moveValueY = 0f;
    float moveValueZ = 0f;
    public float attackPower = 0f;
    float attackPowerUp = 1f;
    GameObject enemy;
    bool hit;
    public bool damage;
    Rigidbody rb;
    float time;
    // Use this for initialization
    void Start () {
        enemy = GameObject.Find("enemy");
        hit = false;
        damage = false;
        rb = GetComponent<Rigidbody>();
	}
	public float AttackPowerReturn()
    {
        return attackPower;
    }
    public void Hit()
    {
        hit = true;
    }
    public void Damage()
    {
        rb.AddForce(new Vector3(-4, 4, 0), ForceMode.Impulse);
        enemy.GetComponent<AI>().enabled = false;
        enemy.GetComponent<Enemy>().Win();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy"&&hit == true)
        {
            Damage();
        }
    }
    // Update is called once per frame
    void Update () {

        if(hit == true)
        {
            time += Time.deltaTime;
        }
        if(time >= 0.1f)
        {
            hit = false;
            time = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(moveValueX, moveValueY, moveValueZ);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(moveValueX, moveValueY, moveValueZ);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 1, 0);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            attackPower += attackPowerUp;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            attackPower = 0;
            transform.position += new Vector3(0.5f, 0, 0);
            enemy.GetComponent<Enemy>().Hit();
        }
    }
}
