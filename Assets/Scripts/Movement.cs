using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    bool hasJumped = false;
    public Collider2D mainCol;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //gameObject.GetComponent<Rigidbody2D>().IsTouching()
        if(gameObject.GetComponent<Rigidbody2D>().IsTouching(mainCol))
        {
            hasJumped = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(.21f,0),ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.A))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-.21f, 0), ForceMode2D.Impulse);
        }
        if (Input.GetKey(KeyCode.Space) && hasJumped == false)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 3f), ForceMode2D.Impulse);
            hasJumped = true;
        }
    }
}
