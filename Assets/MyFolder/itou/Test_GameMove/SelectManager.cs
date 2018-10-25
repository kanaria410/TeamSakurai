using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public void Set_A()
    {
        GameDate.Property_CharacterNumber = 0;
    }

    public void Set_B()
    {
        GameDate.Property_CharacterNumber = 1;
    }

    public void Set_C()
    {
        GameDate.Property_CharacterNumber = 2;
    }

    public void Set_D()
    {
        GameDate.Property_CharacterNumber = 3;
    }

    public void NextScene()
    {
        //何も選択されていないならゲームシーンには移動しない
        if(GameDate.Property_CharacterNumber != 100)
        SceneManager.LoadScene("Test_GameScene");
    }

    // Use this for initialization
    void Start ()
    {
        //適当に初期化
        GameDate.Property_CharacterNumber = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
