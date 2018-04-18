using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning : MonoBehaviour
{
    public Enemy enem;
    private void OnTriggerEnter(Collider other)
    {
        enem.Turn();
    }
}
