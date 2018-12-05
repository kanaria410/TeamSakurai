using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    GameObject player;
    float eatTime;
    SpriteRenderer sp;

	void Start ()
    {
        player = GameObject.Find("Tacha");
        sp = GetComponent<SpriteRenderer>();
	}
	
	void Update ()
    {
        eatTime += Time.deltaTime;
        transform.position =
            new Vector3(player.transform.position.x - 1, player.transform.position.y - 0.3f, player.transform.position.z);

        if(eatTime >= 1)
        {
            sp.material.color -= new Color(0, 0, 0, 0.01f);
        }
        if(eatTime >= 3)
        {
            player.GetComponent<AutoMove>().Eat();
            Destroy(gameObject);
        }
	}
}
