using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour
{
    public Enemy enem;
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Is Hit");
        enem.Turn();
    }
}
