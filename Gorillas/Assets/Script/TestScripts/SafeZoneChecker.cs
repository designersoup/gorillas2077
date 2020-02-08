using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneChecker : MonoBehaviour
{

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.mousePosition.y < 400) { Debug.Log("Danger Zone"); }
        // else Debug.Log("Safe Zone");


        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(worldPoint.x);

        

    }

    public bool SafeZoneCheck (float pointX, float pointY)
    {
        bool isSafe = true;

        for (int i = 0; i < 18; i++)
        {
            if (pointX > -9.0f + i && pointX < -8.0f + i)
            {
                float tempY = -4.77f + ((GetComponent<AICityScript>().GetZoneHeight(i)));
                // Debug.Log("Y zone = " + temp);

                if (pointY > tempY) isSafe = true;
                else
                {
                    if (pointX > target.transform.position.x) isSafe = true;
                    else isSafe = false;
                }
                
            }



        }
        
        return isSafe; 
    }
}
