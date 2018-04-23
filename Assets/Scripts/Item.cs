using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    GameObject player;
    bool isObtained = false;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isObtained)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isObtained = true;
        player.GetComponent<Player>().hasItem = true;
    }
}
