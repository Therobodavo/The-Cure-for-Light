﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHanger : MonoBehaviour
{

    //Sound Bar Variables
    const float maxScale = .9f;
    const float maxX = -.45f;

    //Movement Var
    bool moveLeft = false;
    public float speed;
    SpriteRenderer renderer;

   public CapsuleCollider2D enemyCollider;
    public GameObject player;

    public GameObject[] Lights;
   public bool shy = false;
    void Start()
    {
        Lights = GameObject.FindGameObjectsWithTag("Lights");
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Circle")
            {
                enemyCollider = gameObject.transform.GetChild(i).GetComponent<CapsuleCollider2D>();
            }
            if (gameObject.transform.GetChild(i).name == "EnemySprite")
            {
                renderer = gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
            }
        }
        player = GameObject.Find("Player");

    }

    void Update()
    {
      //  shy = false;

      
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "Circle")
            {
                enemyCollider = gameObject.transform.GetChild(i).GetComponent<CapsuleCollider2D>();
                foreach (GameObject light in Lights)
                {
                    if (light.GetComponent<CircleCollider2D>().bounds.Intersects(enemyCollider.bounds))
                    {
                        shy = true;
                    }
                }
                if (enemyCollider.bounds.Intersects(player.GetComponent<BoxCollider2D>().bounds) && !shy)
                {
                    increaseSound(100);
                }
             
            }
            if (gameObject.transform.GetChild(i).name == "EnemySprite")
            {
                BoxCollider2D spriteCollider = gameObject.transform.GetChild(i).GetComponent<BoxCollider2D>();
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


}