using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Sound Bar Variables
    const float maxScale = .9f;
    const float maxX = -.45f;

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
                    increaseSound(Mathf.Abs((int)((dis - enemyCollider.radius)* 10)));
                }
                break;
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

}


