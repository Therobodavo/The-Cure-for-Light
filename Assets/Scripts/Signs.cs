using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for displaying a tutorial message to the player if they walk over a sign
public class Signs : MonoBehaviour
{
    public string info;
    bool isOn = false;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isOn = false;
    }

    private void OnGUI()
    {
        if(isOn)
        {
            GUI.Box(new Rect(0,0, 1000, 100), info);
        }
    }
}
