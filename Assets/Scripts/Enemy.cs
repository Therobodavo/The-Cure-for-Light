using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    bool isLeft = true;
    bool isRight = false;
    
    
    GameObject[] turnObjs;
    List<Collider2D> turnCols = new List<Collider2D>();
    // Use this for initialization
    void Start()
    {
        //Finding the platforms to check for jumping
        turnObjs = GameObject.FindGameObjectsWithTag("EnemyTurn");
        for (int i = 0; i < turnObjs.Length; i++)
        {
            turnCols.Add(turnObjs[i].GetComponent<Collider2D>());
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //If the enemy is facing left
		if(isLeft)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-.25f, 0), ForceMode2D.Impulse);
        }

        //If the enemy is facing right
        if(isRight)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(.25f, 0), ForceMode2D.Impulse);

        }
    }

    public void Turn()
    {
        if (isLeft)
        {
            isLeft = false;
            isRight = true;
        }
        else if(isRight)
        {
            isRight = false;
            isLeft = true;
        }
    }

}


