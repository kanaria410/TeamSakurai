using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    float alpha;
    float timer = 0.0f;
    [SerializeField, Header("フェードの速さ")]
    float fadeSpeed = 0.0f;
    [SerializeField, Header("プレイヤ")]
    GameObject player = null;
    [SerializeField, Header("敵")]
    GameObject enemy = null;
    [SerializeField, Header("リザルトのテキスト")]
    Text _resultText = null;
    [SerializeField, Header("カウントのテキスト")]
    Text _timerText = null;
    [SerializeField, Header("フェード用の画像")]
    Image _fadeImage = null;

    bool judgeFlag; //勝敗の判定を一度だけするためのフラグ

    void Count()
    {
        timer -= Time.deltaTime;

        if (timer > 0.5)
        {
            if (timer >= 4)
            {
                _timerText.text = "3";
            }
            _timerText.text = timer.ToString("0");
        }
        else if (timer > 0.0)
        {
            _timerText.text = "GO!";
            GlobalDate._STOP_FLAG = false;
        }
        else if (timer > -1.0)
        {
            if (alpha > 0)
            {
                alpha -= Time.deltaTime;
                _timerText.color = new Color(_timerText.color.r, _timerText.color.g, _timerText.color.b, alpha);
            }
        }

    }

    void Judge()
    {
        if (judgeFlag)
        {
            if (player.activeInHierarchy == false)
            {
                _resultText.text = "You Lose";
                _resultText.color = Color.blue;
                judgeFlag = false;
                timer = 0;
                _fadeImage.enabled = true;
                return;
            }
            else if (enemy.activeInHierarchy == false)
            {
                _resultText.text = "You Win";
                _resultText.color = Color.red;
                judgeFlag = false;
                timer = 0;
                _fadeImage.enabled = true;
            }
        }
    }

    void FadeIn()
    {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            alpha += Time.deltaTime * fadeSpeed;
            _fadeImage.color = new Color(0, 0, 0, alpha);
            if (alpha >= 1)
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    private void Awake()
    {
        GlobalDate._STOP_FLAG = true;
        //Time.timeScale = 0.0f;
    }

    // Use this for initialization
    void Start()
    {
        alpha = 1.0f;
        timer = 3.5f;
        judgeFlag = true;
        _resultText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //Count();
        Judge();
        if (judgeFlag)
            Count();
        if (!judgeFlag)
            FadeIn();
    }
}
