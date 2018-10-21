using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform player;
    public Vector3 offset;
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;

    public bool bounds;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        transform.position = new Vector3(player.position.x + offset.x, 2, -10);

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }

	}
}
