using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource audioSource;    //音を鳴らすためのコンポーネント
    AudioClip[] audioClipArray; //音データ

	// Use this for initialization
	void Start ()
    {
        //オーディオソースコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
        //音データを取得
        audioClipArray = Resources.LoadAll<AudioClip>("Audios");
        //ランダムな数値を取得
        int random = Random.Range(0, audioClipArray.Length);
        //ランダムで決まった値を使いランダムにBGMを決める
        audioSource.clip = audioClipArray[random];
        //BGMを再生
        audioSource.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
