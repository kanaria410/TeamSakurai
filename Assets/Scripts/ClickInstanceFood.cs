using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInstanceFood : MonoBehaviour {

    public GameObject food;
    GameObject obj;
    GameObject player;

    private void Start()
    {
        player = GameObject.Find("Tacha");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (hit2d)
            {
                if (hit2d.collider.gameObject.name == "goha")
                {
                    obj = Instantiate(food) as GameObject;
                    player.GetComponent<AutoMove>().MuscleReset();
                }
            }
        }
    }
}
