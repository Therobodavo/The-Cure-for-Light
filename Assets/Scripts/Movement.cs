using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    bool hasJumped = false;
    public Collider2D mainCol;
    //List<Collider2D> jumpCols = new List<Collider2D>();
    GameObject[] jumpColObjs;
    List<Collider2D> jumpCols = new List<Collider2D>();
	// Use this for initialization
	void Start ()
    {
        //Finding the platforms to check for jumping
        jumpColObjs = GameObject.FindGameObjectsWithTag("JumpCheck");
        for (int i = 0; i < jumpColObjs.Length; i++)
        {
            jumpCols.Add(jumpColObjs[i].GetComponent<Collider2D>());
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        //gameObject.GetComponent<Rigidbody2D>().IsTouching()
        for (int i = 0; i < jumpColObjs.Length; i++)
        {
            if (gameObject.GetComponent<Rigidbody2D>().IsTouching(jumpCols[i]))
            {
                hasJumped = false;
            }
        }
        /*if(gameObject.GetComponent<Rigidbody2D>().IsTouching(mainCol))
        {
            hasJumped = false;
        }
        */
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(.5f,0),ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-.5f, 0), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.Space) && hasJumped == false)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3.5f), ForceMode2D.Impulse);
            hasJumped = true;
        }
    }
}
