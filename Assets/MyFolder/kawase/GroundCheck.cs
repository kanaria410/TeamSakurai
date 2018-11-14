using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour {

    //接地している場合
    public UnityEvent OnEnterGround;

    //接地していない場合
    public UnityEvent OnExitGround;
    
    //着地回数
    private int EnterNum = 0;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("接地している");
        EnterNum++;
        OnEnterGround.Invoke();
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("接地していない");
        EnterNum--;
        if (EnterNum <= 0)
        {
            OnExitGround.Invoke();
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
