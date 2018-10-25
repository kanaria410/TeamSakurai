using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    //キャラクターのデータを保存しておく変数
    GameObject[] character;
	// Use this for initialization
	void Start ()
    {
        //リソースフォルダー内のObjectsの中身をすべて取得
        character = Resources.LoadAll<GameObject>("Objects");

        //選択されたプレイヤーを召喚
        Instantiate(character[GameDate.Property_CharacterNumber], transform.position,
            Quaternion.identity);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SceneManager.LoadScene("Test_SelectScene");
        }	
	}
}
