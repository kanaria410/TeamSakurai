using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorMove : MonoBehaviour
{
    bool hitFlag;

    BoxCollider boxCollider;

    public bool Get_HitFlag
    {
        get { return hitFlag; }
    }

	// Use this for initialization
	void Start ()
    {
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            hitFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            hitFlag = false;
        }
    }
}
