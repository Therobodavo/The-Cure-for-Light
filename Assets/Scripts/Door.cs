using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //public  Material doorcol;
    public GameObject play;
    public string nextLevel;
	// Use this for initialization
	void Start ()
    {
        play = GameObject.Find("Player");
        //doorcol.color = Color.white;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(play.GetComponent<Player>().hasItem)
        {
            //doorcol.color = Color.green;
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
        }
    }
}
