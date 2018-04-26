using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
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

                    //Calculate formula, closer to object = faster increase of sound bar
                    increaseSound(Mathf.Abs((int)((enemyCollider.radius / dis)* 10)));
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
            if(gameObject.transform.GetChild(i).name == "Canvas") 
            {
                if(gameObject.transform.GetChild(i).GetChild(0).GetComponent<Slider>().value == 0)
                {
                    gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.r, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.g, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.b, 0);
                }
                else if(gameObject.transform.GetChild(i).GetChild(0).GetComponent<Slider>().value > 0)
                {
                    gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.r, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.g, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.b, 255);
                }
            }
        }
    }

    void increaseSound(int scale)
    {
        for(int i = 0; i < gameObject.transform.childCount; i++) 
        {
            if(gameObject.transform.GetChild(i).name == "Canvas") 
            {
                gameObject.transform.GetChild(i).GetChild(0).GetComponent<Slider>().value += scale;
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


