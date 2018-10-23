using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgmentWin : MonoBehaviour {
    public GameObject win;
    GameObject player;
    bool ahead;
    // Use this for initialization
    public void Win()
    {
        win.transform.position = new Vector3(260, 260, 0);
    }
    public void Later()
    {
        ahead = false;
    }
    private void Start()
    {
        player = GameObject.Find("player");
        ahead = true;
    }
    void OnBecameInvisible()
    {
        if(ahead)
        {
            player.GetComponent<JudgmentLose>().Fail();
            Win();
        }
    }
}
