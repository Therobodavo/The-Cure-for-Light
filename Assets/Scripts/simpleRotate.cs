using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleRotate : MonoBehaviour {
    float tiltAngle;
    public float increase;
	// Use this for initialization
	void Start () {
        tiltAngle = 5;
	}
	
	// Update is called once per frame
	void Update () {
        tiltAngle+= increase;
        Quaternion target = Quaternion.Euler(0, 0, tiltAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime *5);
    }
}
