using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public Vector3 startingPosition;
    public Vector3 bananaPosition;
    public Vector3 targetPosition;
    public bool bananaActive;
    public float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (bananaActive)
        {
            targetPosition = new Vector3(startingPosition.x, bananaPosition.y - 2.0f, startingPosition.z);
            targetPosition.y = Mathf.Clamp(targetPosition.y, 0, 20);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }

        else
        {
            targetPosition = startingPosition;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }


}
