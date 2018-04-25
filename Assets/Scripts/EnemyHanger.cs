using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                    Debug.Log("OOH YEAH");

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

        }     
        
    }

    void increaseSound(int scale)
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name == "SoundBar")
            {
                Vector3 tempPos = gameObject.transform.GetChild(i).GetChild(0).transform.position;
                Vector3 tempScale = gameObject.transform.GetChild(i).GetChild(0).transform.localScale;

                if ((tempPos.x + (.0001f * scale)) <= maxX)
                {
                    tempPos.x = maxX;
                }
                else if ((tempPos.x + (.0001f * scale)) >= 0f)
                {
                    tempPos.x = 0f;
                }
                else
                {
                    tempPos.x += (.0001f * scale);
                }

                if ((tempScale.x + (.0002f * scale)) >= maxScale)
                {
                    tempScale.x = maxScale;
                }
                else if ((tempScale.x + (.0002f * scale)) <= 0)
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


}