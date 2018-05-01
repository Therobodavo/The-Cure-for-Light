using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script for displaying a tutorial message to the player if they walk over a sign
public class Signs : MonoBehaviour
{
    public GameObject canv;
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
        canv.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canv.SetActive(false);
    }

}
