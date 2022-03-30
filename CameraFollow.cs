//This is a smooth-follow camera script in 2D
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1,10)]
    public float smoothFactor;

    // Start is called before the first frame update

    // Update is called once per frame
    public void FixedUpdate()
    {
        Follow();
    }

    public void Follow(){
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position,targetPosition,smoothFactor*Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
