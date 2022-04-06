using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    Vector3 nextPosition;
    public float smoothFactor = 7;
    
    private void Start()
    {
        nextPosition.z = transform.position.z;
    }

    private void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        if (Target)
        {
            nextPosition.x = Target.position.x;
            nextPosition.y = Target.position.y;

            Vector3 smoothPosition = Vector3.Lerp(transform.position, nextPosition, smoothFactor * Time.fixedDeltaTime);
            transform.position = smoothPosition;
        }
    }
}
