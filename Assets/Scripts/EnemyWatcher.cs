﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWatcher : MonoBehaviour {

    //Movement Var
    bool moveLeft = false;
    public float speed;
    SpriteRenderer renderer;
    Animator AnimPlayer;
    CircleCollider2D enemyCollider;
    public GameObject player;
    bool rage = false;
    float yPos;
    void Start()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Circle")
            {
                enemyCollider = gameObject.transform.GetChild(i).GetComponent<CircleCollider2D>();
            }
        
        }
        renderer = GetComponent<SpriteRenderer>();
        AnimPlayer = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    void Update()
    {

        if(player.transform.position.x > transform.position.x)
        {
            renderer.flipX = true;
        }
        if (player.transform.position.x < transform.position.x)
        {
            renderer.flipX = false;
        }

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Circle")
            {
                enemyCollider = gameObject.transform.GetChild(i).GetComponent<CircleCollider2D>();
                if (enemyCollider.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds))
                {
                    Debug.Log("BLOOP");
                    float dis = (player.transform.position - gameObject.transform.position).magnitude;

                    //Calculate formula, closer to object = faster increase of sound bar
                    increaseSound(Mathf.Abs((int)((enemyCollider.radius / dis) * 10)));
                }
                
            }

            BoxCollider2D spriteCollider = GetComponent<BoxCollider2D>();
           if (spriteCollider.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds))
           {
            increaseSound(10000);
           }
          
            if (gameObject.transform.GetChild(i).name == "Canvas")
            {
                if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<Slider>().value == 0)
                {
                    gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.r, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.g, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.b, 0);
                }
                else if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<Slider>().value > 0)
                {
                    gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.r, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.g, gameObject.transform.GetChild(i).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().color.b, 255);
                }
            }

            if (rage)
            {
                //transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
            }
        }
    }

    void increaseSound(int scale)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Canvas")
            {
                gameObject.transform.GetChild(i).GetChild(0).GetComponent<Slider>().value += scale;
                if (gameObject.transform.GetChild(i).GetChild(0).GetComponent<Slider>().value >= 1000)
                {
                    AnimPlayer.SetBool("Rage", true);
                    if (!rage)
                    {
                        //yPos = transform.position.y + 0.2f;
                    }

                    rage = true;
                }
            }
        }
    }


  

}


