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
    Player pScript;
    // Use this for initialization
    void Start ()
    {
        pScript = GetComponent<Player>();
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

        float xOffset = boxie.offset.x;
        float yOffset = boxie.offset.y;

        float xSize = boxie.size.x;
        float ySize = boxie.size.y;



        RaycastHit2D topRight = Physics2D.Raycast(new Vector2((boxie.transform.position.x + xOffset) + (xSize / 2) +.01f, (boxie.transform.position.y + yOffset) - (ySize / 2)), Vector2.right,dis,layer_mask);
        RaycastHit2D middleRight = Physics2D.Raycast(new Vector2((boxie.transform.position.x + xOffset) + (xSize / 2) +.01f, (boxie.transform.position.y + yOffset)), Vector2.right,dis,layer_mask);
        RaycastHit2D bottomRight = Physics2D.Raycast(new Vector2((boxie.transform.position.x + xOffset) + (xSize / 2) +.01f, (boxie.transform.position.y + yOffset) + (ySize / 2)), Vector2.right,dis,layer_mask);

        RaycastHit2D topLeft = Physics2D.Raycast(new Vector2((boxie.transform.position.x + xOffset) - (xSize / 2) -.01f, (boxie.transform.position.y + yOffset) - (ySize / 2)), -Vector2.right,dis,layer_mask);
        RaycastHit2D middleLeft = Physics2D.Raycast(new Vector2((boxie.transform.position.x + xOffset) - (xSize / 2) -.01f, (boxie.transform.position.y + yOffset)), -Vector2.right,dis,layer_mask);
        RaycastHit2D bottomLeft = Physics2D.Raycast(new Vector2((boxie.transform.position.x + xOffset) - (xSize / 2) -.01f, (boxie.transform.position.y + yOffset) + (ySize / 2)), -Vector2.right,dis,layer_mask);
        
        Vector3 pos = gameObject.transform.position;

        AnimPlayer.SetBool("Move", false);

        
        if (Input.GetKey(KeyCode.D)&& !pScript.dead)
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
                     gameObject.transform.position = new Vector2((topRight.point.x - xOffset) - .01f - (xSize / 2), (topRight.point.y + yOffset) + (ySize / 2));
                }
                else if(middleRight.collider) 
                {
                     gameObject.transform.position = new Vector2((middleRight.point.x - xOffset) - .01f - (xSize / 2), (middleRight.point.y + yOffset));
                }
                else
                {
                    gameObject.transform.position = new Vector2((bottomRight.point.x - xOffset) - .01f - (xSize / 2), (bottomRight.point.y + yOffset) - (ySize / 2));
                }
            }
        }
        if (Input.GetKey(KeyCode.A) && !pScript.dead)
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
                     gameObject.transform.position = new Vector2((topLeft.point.x + xOffset) + .01f + (xSize / 2), (topLeft.point.y + yOffset) + (ySize / 2));
                }
                else if(middleLeft.collider) 
                {
                     gameObject.transform.position = new Vector2((middleLeft.point.x + xOffset) + .01f + (xSize / 2), (middleLeft.point.y + yOffset));
                }
                else
                {
                    gameObject.transform.position = new Vector2((bottomLeft.point.x + xOffset) + .01f + (xSize / 2), (bottomLeft.point.y + yOffset) - (ySize / 2));
                }
            }
        }
        if (Input.GetKey(KeyCode.Space) && hasJumped == false && !pScript.dead)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 6f), ForceMode2D.Impulse);
            hasJumped = true;
        }
    }
}
