using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeDath : MonoBehaviour {
    bool deathFlag;

    public bool Get_DeathFlag
    {
        get { return deathFlag; }
    }

    // Use this for initialization
    void Start()
    {
        deathFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -10)
        {
            deathFlag = true;
            //Destroy(gameObject);
            transform.position = new Vector3(9, 2, 0);
        }
    }
}
