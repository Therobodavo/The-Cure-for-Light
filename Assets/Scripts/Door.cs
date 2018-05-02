using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //public  Material doorcol;
    public Player play;
    public string nextLevel;
	// Use this for initialization
	void Start ()
    {

        //doorcol.color = Color.white;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(play.hasItem)
        {
            //doorcol.color = Color.green;
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
        }
    }
}
