using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeDath : MonoBehaviour {

    Renderer rend;
    Color col;
    [SerializeField, Range(0, 1)]
    float alpha;

    bool deathFlag;

    public GameObject camera;


    public bool Get_DeathFlag
    {
        get { return deathFlag; }
    }

    // Use this for initialization
    void Start()
    {
        deathFlag = false;
        rend = GetComponent<Renderer>();
        col = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        rend.material.color = new Color(alpha, col.g, col.b,0);

        if (transform.position.y < -20)
        {
            deathFlag = true;
            //Destroy(gameObject);
            transform.position = new Vector3(9, 2, 0);

            camera.GetComponent<MultiPlayerCamera>().SetTargets(transform);
        }
    }
}
