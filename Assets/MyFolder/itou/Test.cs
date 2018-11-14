using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField, Header("何秒で削除するか")]
    float destroyTime;

	// Use this for initialization
	void Start ()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, destroyTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
