using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GameObject player;
    GameObject director;
    Rigidbody rb;
    public float power;
    float time;
    float time2;
    float hitLifeTime = 0.1f;
    float detectionLifeTime = 0.1f;
    float reset = 0;
    float powerWayX = 4f;
    float powerWayY = 4f;
    float powerWayZ = 0f;
    public float distance = 0;
    bool hit;
    bool detection;
    public bool win;
    bool succes;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("player");
        rb = GetComponent<Rigidbody>();
        hit = false;
        win = false;
        succes = false;
        detection = false;
	}
    public void Hit()
    {
        hit = true;
    }
    public void Win()
    {
        win = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            detection = true;
        }
    }
    // Update is called once per frame
    void Update () {
        power = player.GetComponent<Player>().AttackPowerReturn();

        distance = (transform.position.x - player.transform.position.x);

        if (hit == true&&detection == true)
        {
            succes = true;
        }
        if (succes)
        {
            rb.AddForce(new Vector3(powerWayX, powerWayY, powerWayZ), ForceMode.Impulse);
        }
        /*if(win == true)
        {
            powerWayX = 0f;
            powerWayY = 0f;
        }*/

        if(hit == true)
        {
            time += Time.deltaTime;
        }
        if (time >= hitLifeTime)
        {
            hit = false;
            time = reset;
        }

        if (detection == true)
        {
            time2 += Time.deltaTime;
        }
        if (time2 >= detectionLifeTime)
        {
            detection = false;
            time2 = reset;
        }

    }
}
