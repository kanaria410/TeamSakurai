using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    //自身をリスポーンさせるための関数
    public void Respawn()
    {
        if (transform.position.x < -100 || transform.position.x > 100 || transform.position.y < -20) 
        {
            gameObject.SetActive(false);
        }
        
        //}
        //残機をデクリメント
        //life--;
        ////加速している状態を強制的に静止させる
        //_rigidbody.velocity = Vector3.zero;
        ////リスポーンする位置
        //transform.position = defaultPosition;
        //生きているからカメラに登録し直す
        //multiPlayerCamera.SetTargets(transform);
    }

    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Respawn();
    }
}
