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
        // Smoothly tilts a transform towards a target rotation.
        float tiltAround = tiltAngle;

        Quaternion target = Quaternion.Euler(tiltAround, 0, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
