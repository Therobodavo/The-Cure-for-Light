using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLights : MonoBehaviour {

    float smooth = 5.0f;
    float tiltAngle = 70.0f;
    bool increase = true;

    void Update()
    {
        if(increase)
            tiltAngle+= 0.01f;
        else
            tiltAngle -= 0.1f;

        if(tiltAngle >=  85)
        {
            increase = false;
        }

        if (tiltAngle <= 65)
        {
            increase = true;
        }
       

        Quaternion target = Quaternion.Euler(tiltAngle, 0, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
