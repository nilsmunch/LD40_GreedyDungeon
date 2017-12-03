using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixellatedCameraFollow : MonoBehaviour {
    public GameObject trackThis;

    const float limiter = 16;
	
	// Update is called once per frame
	void Update () {
        Vector2 pos = trackThis.transform.position;

        Vector3 adjusted = new Vector3(pos.x, pos.y, -10);

        transform.position = adjusted;
    }
}
