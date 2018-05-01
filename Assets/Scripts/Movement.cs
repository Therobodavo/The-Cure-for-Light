using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    bool dead = false;
    bool hasJumped = false;
    public Collider2D mainCol;
    //List<Collider2D> jumpCols = new List<Collider2D>();
    GameObject[] jumpColObjs;
    List<Collider2D> jumpCols = new List<Collider2D>();
    Animator AnimPlayer;
    SpriteRenderer renderer;
   public Light pointLight;
    bool faceLeft = false;
    // Use this for initialization
    void Start ()
    {
        renderer = GetComponent<SpriteRenderer>();
        //Finding the platforms to check for jumping
        jumpColObjs = GameObject.FindGameObjectsWithTag("JumpCheck");
        AnimPlayer = GetComponent<Animator>();
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
        MoveLight();

 
        /*if(gameObject.GetComponent<Rigidbody2D>().IsTouching(mainCol))
        {
            hasJumped = false;
        }
        */
        
    }
    public void MoveLight()
    {
        Vector3 lightPos = pointLight.transform.position;
        if (!faceLeft)
        {
            lightPos.x = transform.position.x + 0.5f;
        }

        if (faceLeft)
        {
            lightPos.x = transform.position.x - 0.4f;
        }
       
        pointLight.transform.position = lightPos;

    

    }

    private void FixedUpdate()
    {
        float dis = .1f;
        int layer_mask = LayerMask.GetMask("Structure", "Creature");
        BoxCollider2D boxie = GetComponent<BoxCollider2D>();
        RaycastHit2D topRight = Physics2D.Raycast(new Vector2(boxie.transform.position.x + (boxie.transform.localScale.x / 2) +.01f, boxie.transform.position.y - (boxie.transform.localScale.y / 2)), Vector2.right,dis,layer_mask);
        RaycastHit2D middleRight = Physics2D.Raycast(new Vector2(boxie.transform.position.x + (boxie.transform.localScale.x / 2) +.01f, boxie.transform.position.y), Vector2.right,dis,layer_mask);
        RaycastHit2D bottomRight = Physics2D.Raycast(new Vector2(boxie.transform.position.x + (boxie.transform.localScale.x / 2) +.01f, boxie.transform.position.y + (boxie.transform.localScale.y / 2)), Vector2.right,dis,layer_mask);

        RaycastHit2D topLeft = Physics2D.Raycast(new Vector2(boxie.transform.position.x - (boxie.transform.localScale.x / 2) -.01f, boxie.transform.position.y - (boxie.transform.localScale.y / 2)), -Vector2.right,dis,layer_mask);
        RaycastHit2D middleLeft = Physics2D.Raycast(new Vector2(boxie.transform.position.x - (boxie.transform.localScale.x / 2) -.01f, boxie.transform.position.y), -Vector2.right,dis,layer_mask);
        RaycastHit2D bottomLeft = Physics2D.Raycast(new Vector2(boxie.transform.position.x - (boxie.transform.localScale.x / 2) -.01f, boxie.transform.position.y + (boxie.transform.localScale.y / 2)), -Vector2.right,dis,layer_mask);
        
        Vector3 pos = gameObject.transform.position;

        AnimPlayer.SetBool("Move", false);

        if (Input.GetKey(KeyCode.D))
        {
            if (faceLeft)
            {
                faceLeft = false;
                renderer.flipX = false;
            }
            AnimPlayer.SetBool("Move",true);
            if(!topRight.collider && !middleRight.collider && !bottomRight.collider) 
            {
                gameObject.transform.position = new Vector3(pos.x + .1f, pos.y, pos.z);
            }
            else
            {
                if(topRight.collider) 
                {
                     gameObject.transform.position = new Vector2(topRight.point.x - .01f - (boxie.transform.localScale.x / 2), topRight.point.y + (boxie.transform.localScale.y / 2));
                }
                else if(middleRight.collider) 
                {
                     gameObject.transform.position = new Vector2(middleRight.point.x - .01f - (boxie.transform.localScale.x / 2), middleRight.point.y);
                }
                else
                {
                    gameObject.transform.position = new Vector2(bottomRight.point.x - .01f - (boxie.transform.localScale.x / 2), bottomRight.point.y - (boxie.transform.localScale.y / 2));
                }
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!faceLeft)
            {
                faceLeft = true;
                renderer.flipX = true;
            }
            AnimPlayer.SetBool("Move", true);
            if (topLeft.collider == null && middleLeft.collider == null && bottomLeft.collider == null) 
            {
                gameObject.transform.position = new Vector3(pos.x - .1f, pos.y, pos.z);
            }
            else 
            {
                if(topLeft.collider) 
                {
                     gameObject.transform.position = new Vector2(topLeft.point.x + .01f + (boxie.transform.localScale.x / 2), topLeft.point.y + (boxie.transform.localScale.y / 2));
                }
                else if(middleLeft.collider) 
                {
                     gameObject.transform.position = new Vector2(middleLeft.point.x + .01f + (boxie.transform.localScale.x / 2), middleLeft.point.y);
                }
                else
                {
                    gameObject.transform.position = new Vector2(bottomLeft.point.x + .01f + (boxie.transform.localScale.x / 2), bottomLeft.point.y - (boxie.transform.localScale.y / 2));
                }
            }
        }
        if (Input.GetKey(KeyCode.Space) && hasJumped == false)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 6f), ForceMode2D.Impulse);
            hasJumped = true;
        }
    }
}
