using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentLose : MonoBehaviour {
    public GameObject lose;
    GameObject enemy;
    bool fail;

    public void Lose()
    {
        lose.transform.position = new Vector3(260, 260, 0);
    }
    public void Fail()
    {
        fail = true;
    }
    private void Start()
    {
        enemy = GameObject.Find("enemy");
        fail = false;
    }
    void OnBecameInvisible()
    {
        enemy.GetComponent<JudgmentWin>().Later();
        if(fail == false)
        {
            Lose();
        }
    }
}
