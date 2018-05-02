using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool hasItem = false;
    public Animator AnimPlayer;
    public Checkpoints currentCheckpoint;
    public Vector3 startPos;
    public bool dead;
   
	// Use this for initialization
	void Start () 
    {
        startPos = transform.position;
       
        dead = false;
        AnimPlayer = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
      
      
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(currentCheckpoint == null)
            {
                transform.position = startPos;
            }
            else
            {
                transform.position = currentCheckpoint.checkpointPos;
            }
         
            dead = false;
           AnimPlayer.SetBool("Dead", false);
        
        }
        if (dead)
        {

            AnimPlayer.SetBool("Dead", true);
        }
    }
}
