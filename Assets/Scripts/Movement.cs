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
        
    }

    private void FixedUpdate()
    {
        float dis = .1f;
        int layer_mask = LayerMask.GetMask("Structure", "Creature");
        RaycastHit2D topRight = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x / 2) +.01f, transform.position.y - (transform.localScale.y / 2)), Vector2.right,dis,layer_mask);
        RaycastHit2D middleRight = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x / 2) +.01f, transform.position.y), Vector2.right,dis,layer_mask);
        RaycastHit2D bottomRight = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x / 2) +.01f, transform.position.y + (transform.localScale.y / 2)), Vector2.right,dis,layer_mask);

        RaycastHit2D topLeft = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x / 2) -.01f, transform.position.y - (transform.localScale.y / 2)), -Vector2.right,dis,layer_mask);
        RaycastHit2D middleLeft = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x / 2) -.01f, transform.position.y), -Vector2.right,dis,layer_mask);
        RaycastHit2D bottomLeft = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x / 2) -.01f, transform.position.y + (transform.localScale.y / 2)), -Vector2.right,dis,layer_mask);
        
        Vector3 pos = gameObject.transform.position;

        if (Input.GetKey(KeyCode.D))
        {
            if(!topRight.collider && !middleRight.collider && !bottomRight.collider) 
            {
                gameObject.transform.position = new Vector3(pos.x + .1f, pos.y, pos.z);
            }
            else
            {
                if(topRight.collider) 
                {
                     gameObject.transform.position = new Vector2(topRight.point.x - .01f - (transform.localScale.x / 2), topRight.point.y + (transform.localScale.y / 2));
                }
                else if(middleRight.collider) 
                {
                     gameObject.transform.position = new Vector2(middleRight.point.x - .01f - (transform.localScale.x / 2), middleRight.point.y);
                }
                else
                {
                    gameObject.transform.position = new Vector2(bottomRight.point.x - .01f - (transform.localScale.x / 2), bottomRight.point.y - (transform.localScale.y / 2));
                }
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(topLeft.collider == null && middleLeft.collider == null && bottomLeft.collider == null) 
            {
                gameObject.transform.position = new Vector3(pos.x - .1f, pos.y, pos.z);
            }
            else 
            {
                if(topLeft.collider) 
                {
                     gameObject.transform.position = new Vector2(topLeft.point.x + .01f + (transform.localScale.x / 2), topLeft.point.y + (transform.localScale.y / 2));
                }
                else if(middleLeft.collider) 
                {
                     gameObject.transform.position = new Vector2(middleLeft.point.x + .01f + (transform.localScale.x / 2), middleLeft.point.y);
                }
                else
                {
                    gameObject.transform.position = new Vector2(bottomLeft.point.x + .01f + (transform.localScale.x / 2), bottomLeft.point.y - (transform.localScale.y / 2));
                }
            }
        }
        if (Input.GetKey(KeyCode.Space) && hasJumped == false)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);
            hasJumped = true;
        }
    }
}
