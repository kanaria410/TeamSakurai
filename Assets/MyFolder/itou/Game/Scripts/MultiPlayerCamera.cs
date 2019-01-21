using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerCamera : MonoBehaviour
{
    public Camera cam;
    public List<Transform> targets;

    [SerializeField, Header("キャラクターたち")]
    private GameObject[] character;

    private PlayerDeath[] playerDeath = new PlayerDeath[2];

    public Vector3 offset;
    public float smoothTime = 0.5f;

    public float minZoom = 50;
    public float maxZoom = 10;
    public float zoomLimiter = 50;

    bool flag = true;   //一度だけ使う

    private Vector3 velocity;

    public void SetTargets(Transform trans)
    {
        targets.Add(trans);
    }

    private void Reset()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0) return;

        Move();
        Zoom();
    }

    private void Zoom()
    {
        var newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        var centerPoint = GetCenterPoint();
        var newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }

    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1) return targets[0].position;
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

    private void Start()
    {
        //キャラクターについているクラスを取得
        for (int i = 0; i < character.Length - 1; i++)
        {
            playerDeath[i] = character[i].GetComponent<PlayerDeath>();
        } 
    }

    private void Update()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (character[i].activeInHierarchy == false)
            {
                if (flag)
                    targets.Remove(targets[i]);
                flag = false;
            }
        }
    }
}