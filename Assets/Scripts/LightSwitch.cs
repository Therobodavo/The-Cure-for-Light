using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light mainLight;
    GameObject player;

    bool atSwitch = false;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Getting player input
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Making sure the player is at the correct position
            if (atSwitch == true)
            {
                if (mainLight.enabled == false)
                {
                    Debug.Log("Turned the light on");
                    mainLight.enabled = true;
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided");
        atSwitch = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        atSwitch = false;
    }
}
