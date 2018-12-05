using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {
    float speed = 2;
    Vector2 vec;
    float positionLotteryTime;
    float randomPosX;
    float randomPosY;
    float muscleTime;
    bool moveOn = false;
    public bool isThereAny = false;
    GameObject[] tagObjects;
    GameObject food;
    SpriteRenderer sp;
    public Sprite normal;
    public Sprite muscle;
    int lv = 1;
    // Use this for initialization
    void Start () {
        sp = GetComponent<SpriteRenderer>();
	}

    public void Eat()
    {
        lv++;
    }

    public void MuscleReset()
    {
        muscleTime = 0;
    }
    void Update()
    {
        tagObjects = GameObject.FindGameObjectsWithTag("food");
        if(tagObjects.Length >= 1)
        {
            isThereAny = true;
        }
        if (isThereAny == true && tagObjects.Length == 0)
        {
            sp.sprite = muscle;
            muscleTime += Time.deltaTime;
        }
        if(muscleTime >= 1)
        {
            sp.sprite = normal;
        }

        if (moveOn == false)
        {
            positionLotteryTime += Time.deltaTime;
        }

        if(positionLotteryTime >= 3)
        {
            randomPosX = Random.Range(-8, 8);
            randomPosY = Random.Range(-2, 2);
            vec = new Vector2(randomPosX, randomPosY);
            positionLotteryTime = 0;
            moveOn = true;
        }
        if (moveOn)
        {
            transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(vec.x, vec.y), speed * Time.deltaTime);
            if(transform.position.x == vec.x&&transform.position.y == vec.y)
            {
                moveOn = false;
            }
        }

        //クリックした位置に移動 
        /*if (Input.GetMouseButtonDown(0))
        {
            vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        transform.position = Vector2.MoveTowards
            (transform.position, new Vector2(vec.x, vec.y), speed * Time.deltaTime);
            */
    }
}
