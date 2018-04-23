using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public  Material doorcol;
    public Player play;
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
        if(play.hasItem)
        {
            doorcol.color = Color.green;
            Debug.Log("Good End");
        }
        else
        {
            Debug.Log("Bad End");
            doorcol.color = Color.red;
        }
    }
}
