using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public Vector2 checkpointPos;
    GameObject player;
    Checkpoints thisCheckP;
	// Use this for initialization
	void Start ()
    {
        checkpointPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
        player = GameObject.Find("Player");
        thisCheckP = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.GetComponent<Player>().currentCheckpoint = thisCheckP;
    }
}
