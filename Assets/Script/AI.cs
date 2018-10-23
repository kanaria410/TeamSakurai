using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
    public int status = 1;
    GameObject player;
    public float distance;
    float moveValueX = 0.1f;
    float moveValueY = 0f;
    float moveValueZ = 0f;
    bool attackOne;
    bool battleOne;
    float time;
    float time2;
    int attackRandom;
    int battleRandom;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("player");
        attackOne = false;
        battleOne = false;
	}
	
    void Idle()
    {
        //待機
        status = 2;
    }
    void Move()
    {
        if (distance >= 4)
        {
            transform.position -= new Vector3(moveValueX, moveValueY, moveValueZ);
        }
        if(distance <= 4)
        {
            status = 3;
        }
    }
    void Battle()
    {
        if(battleOne == false)
        {
            battleRandom = Random.Range(1, 3);
            battleOne = true;
        }
        if(battleRandom == 1)
        {
            transform.position -= new Vector3(0.1f, 0, 0);
        }
        if(battleRandom == 2)
        {
            time2 += Time.deltaTime;
            if(time2 <= 0.3f)
            {
                transform.position += new Vector3(0.1f, 0, 0);
            }
            if(time2 >= 0.3f)
            {
                battleOne = false;
            }
        }
        attackOne = false;
        if(distance <= 2)
        {
            status = 4;
        }
    }
    void delay()
    {
        transform.position -= new Vector3(0.5f, 0, 0);
        player.GetComponent<Player>().Hit();
        attackRandom = Random.Range(1, 3);
    }
    void Attack()
    {
        if(attackOne == false)
        {
            Invoke("delay", 0.1f);
            attackOne = true;
        }
        if(attackRandom == 1)
        {
            status = 3;
        }
        if(attackRandom == 2)
        {
            status = 5;
        }
    }
    void Avoid()
    {
        time += Time.deltaTime;
        if(time <= 0.5f)
        {
            transform.position += new Vector3(0.1f, 0, 0);
        }
        if(time >= 0.5f)
        {
            status = 3;
        }
    }

	// Update is called once per frame
	void Update () {
        distance = (transform.position.x - player.transform.position.x);

        //if()

        switch (status)
        {
            case 1:
                Idle();
                break;
            case 2:
                Move();
                break;
            case 3:
                Battle();
                break;
            case 4:
                Attack();
                break;
            case 5:
                Avoid();
                break;
            default:
                break;
        }

    }
}
