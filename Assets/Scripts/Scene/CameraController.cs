using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float smoothing = 5; 

    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 targetPos = player.transform.position + offset;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, smoothing * Time.deltaTime);
    }
}
