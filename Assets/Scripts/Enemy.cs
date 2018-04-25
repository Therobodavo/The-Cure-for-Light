using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Sound Bar Variables
    const float maxScale = .9f;
    const float maxX = -.45f;

    //Movement Var
    bool moveLeft = false;
    public float speed;
    SpriteRenderer renderer;

    CircleCollider2D enemyCollider;
    public GameObject player;
    void Start()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).name == "Circle")
            {
                enemyCollider = gameObject.transform.GetChild(i).GetComponent<CircleCollider2D>();
            }
            if (gameObject.transform.GetChild(i).name == "EnemySprite")
            {             
                renderer = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
            }
        }
        player = GameObject.Find("Player");
    }

    void Update ()
    {
        for(int i = 0; i < gameObject.transform.childCount; i++)
        {
            if(gameObject.transform.GetChild(i).name == "Circle")
            {
                enemyCollider = gameObject.transform.GetChild(i).GetComponent<CircleCollider2D>();
                if(enemyCollider.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds))
                {
                    float dis = (player.transform.position - gameObject.transform.position).magnitude;
                    Debug.Log("OOOOH YEAH");
                    //Calculate formula, closer to object = faster increase of sound bar
                    increaseSound(Mathf.Abs((int)((enemyCollider.radius / dis)* 100)));
                }
                break;
            }
            if(gameObject.transform.GetChild(i).name == "EnemySprite")
            {
              BoxCollider2D  spriteCollider = gameObject.transform.GetChild(i).GetComponent<BoxCollider2D>();
                if (spriteCollider.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds))
                {
                    increaseSound(10000);
                }

           
            }
        }
    }

    void increaseSound(int scale)
    {
         for(int i = 0; i < gameObject.transform.childCount; i++)
         {
            if(gameObject.transform.GetChild(i).name == "SoundBar")
            {
                Vector3 tempPos = gameObject.transform.GetChild(i).GetChild(0).transform.position;
                Vector3 tempScale = gameObject.transform.GetChild(i).GetChild(0).transform.localScale;

                if((tempPos.x + (.0001f * scale)) <= maxX)
                {
                    tempPos.x = maxX;
                }
                else if((tempPos.x + (.0001f * scale)) >= 0f)
                {
                    tempPos.x = 0f;
                }
                else
                {
                    tempPos.x += (.0001f * scale);
                }

                if((tempScale.x + (.0002f * scale)) >= maxScale)
                {
                    tempScale.x = maxScale;
                }
                else if((tempScale.x + (.0002f * scale)) <= 0)
                {
                    tempScale.x = 0;
                }
                else
                {
                    tempScale.x += (.0002f * scale);
                }
                gameObject.transform.GetChild(i).GetChild(0).transform.position = tempPos;
                gameObject.transform.GetChild(i).GetChild(0).transform.localScale = tempScale;
                break;
            }
         }
    }


    private void FixedUpdate()
    {
        float dis = .1f;
        int layer_mask = LayerMask.GetMask("Structure");
        RaycastHit2D topRight = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x / 2) + .01f, transform.position.y - (transform.localScale.y / 2)), Vector2.right, dis, layer_mask);
        RaycastHit2D middleRight = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x / 2) + .01f, transform.position.y), Vector2.right, dis, layer_mask);
        RaycastHit2D bottomRight = Physics2D.Raycast(new Vector2(transform.position.x + (transform.localScale.x / 2) + .01f, transform.position.y + (transform.localScale.y / 2)), Vector2.right, dis, layer_mask);

        RaycastHit2D topLeft = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x / 2) - .01f, transform.position.y - (transform.localScale.y / 2)), -Vector2.right, dis, layer_mask);
        RaycastHit2D middleLeft = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x / 2) - .01f, transform.position.y), -Vector2.right, dis, layer_mask);
        RaycastHit2D bottomLeft = Physics2D.Raycast(new Vector2(transform.position.x - (transform.localScale.x / 2) - .01f, transform.position.y + (transform.localScale.y / 2)), -Vector2.right, dis, layer_mask);

        Vector3 pos = gameObject.transform.position;

        if (moveLeft)
        {
            if (!topRight.collider && !middleRight.collider && !bottomRight.collider)
            {
                gameObject.transform.position = new Vector3(pos.x + speed, pos.y, pos.z);
            }
            else
            {
                if (topRight.collider)
                {
                    gameObject.transform.position = new Vector2(topRight.point.x - .01f - (transform.localScale.x / 2), topRight.point.y + (transform.localScale.y / 2));
                }
                else if (middleRight.collider)
                {
                    gameObject.transform.position = new Vector2(middleRight.point.x - .01f - (transform.localScale.x / 2), middleRight.point.y);
                }
                else
                {
                    gameObject.transform.position = new Vector2(bottomRight.point.x - .01f - (transform.localScale.x / 2), bottomRight.point.y - (transform.localScale.y / 2));
                }
                moveLeft = false;
                renderer.flipX = false;
            }
        }
        if (!moveLeft)
        {
            if (topLeft.collider == null && middleLeft.collider == null && bottomLeft.collider == null)
            {
                gameObject.transform.position = new Vector3(pos.x - speed, pos.y, pos.z);
            }
            else
            {
                if (topLeft.collider)
                {
                    gameObject.transform.position = new Vector2(topLeft.point.x + .01f + (transform.localScale.x / 2), topLeft.point.y + (transform.localScale.y / 2));
                }
                else if (middleLeft.collider)
                {
                    gameObject.transform.position = new Vector2(middleLeft.point.x + .01f + (transform.localScale.x / 2), middleLeft.point.y);
                }
                else
                {
                    gameObject.transform.position = new Vector2(bottomLeft.point.x + .01f + (transform.localScale.x / 2), bottomLeft.point.y - (transform.localScale.y / 2));
                }
                moveLeft = true;
                renderer.flipX = true;
           
            }
        }
    }

}


