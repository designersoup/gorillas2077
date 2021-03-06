﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AIController : MonoBehaviour
{
    LineRenderer lr;

    public bool debugMode;

    public GameObject player;
    public GameObject target;
    public GameObject cityScript;

    public int velocity;
    public float angle = 90;
    public int resolution = 10;
    public float distance;
    public float maxDistance;
    public bool countDown;
    public bool running;
    public GameObject[] markerArray;
    public bool[] markerSafe;
    public bool paused;

    public GameObject perimMarkerPF;
    public GameObject[] pMAr;

    public Color seekColor;
    public Color foundColor;

    public float g; // force of gravity on the y axis;
    float radianAngle;
    public GameObject markerPrefab;

    public GameObject bananaPrefab;
    public GameObject gameController;
    public float tolerance;


    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        g = Mathf.Abs(Physics2D.gravity.y);
        
        markerArray = new GameObject[resolution];
        markerSafe = new bool[resolution];
        for (int i = 0; i < resolution; i++)

        {
            markerArray[i] = Instantiate(markerPrefab, new Vector3(0, 0, 0), transform.rotation);
        }
        

       /* pMAr = new GameObject[4];
        for (int p = 0; p < pMAr.Length; p++)
        {
            pMAr[p] = Instantiate(perimMarkerPF, new Vector3(0, 0, 0), transform.rotation);
        }*/

       if (debugMode == true) resetTarget();







    }


    // Start is called before the first frame update
    void Start()
    {
      //  if (debugMode == false) AIturn();

    }

    public void StartAI()
    {
       // AIturn();
    }

    private void Update()
    {
        if (debugMode == true) RenderArc();
        
    }

    public void targetFound()
    {
        running = false;
        lr.material.SetColor("_BaseColor", foundColor);
        Debug.Log("Target found!");
      //  GameObject banana = Instantiate(bananaPrefab, player.transform.position, Quaternion.identity);
      //  banana.GetComponent<banana>().vertSpeed = Mathf.Sin((angle * Mathf.PI) / 180) * velocity;
      //  banana.GetComponent<banana>().horzSpeed = Mathf.Cos((angle * Mathf.PI) / 180) * velocity;
      //  StartCoroutine(resetPause());


    }


    //populate the line renderer
    void RenderArc()
    {
        

        
        lr.SetVertexCount(resolution + 1);
        lr.SetPositions(calculateArcArray());

        
    }


    // create an array Vector3 positions for the arc

    Vector3[] calculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];

        radianAngle = Mathf.Deg2Rad * angle;
        maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = calculateArcPoint(t, maxDistance);
            Vector2 Offset = new Vector2(player.transform.position.x, player.transform.position.y);
            arcArray[i].x = arcArray[i].x + Offset.x;
            arcArray[i].y = arcArray[i].y + Offset.y;



        }


        for (int i = 0; i < resolution; i++)
        {
           markerArray[i].transform.position = arcArray[i];
            markerSafe[i] = gameController.GetComponent<SafeZoneChecker>().SafeZoneCheck(markerArray[i].transform.position.x, markerArray[i].transform.position.y);
            
           
        }

          /*int x = resolution;
         /pMAr[0].transform.position = new Vector2(arcArray[x].x - 0.5f, arcArray[x].y - 0.5f);
          pMAr[1].transform.position = new Vector2(arcArray[x].x - 0.5f, arcArray[x].y + 0.5f);
          pMAr[2].transform.position = new Vector2(arcArray[x].x + 0.5f, arcArray[x].y + 0.5f);
          pMAr[3].transform.position = new Vector2(arcArray[x].x + 0.5f, arcArray[x].y - 0.5f);*/


        if (paused == false)
        {

           

           for (int k = 0; k < resolution; k++)
            {


                if (arcArray[k].x < target.transform.position.x + tolerance &&
                    arcArray[k].x > target.transform.position.x - tolerance &&
                    arcArray[k].y < target.transform.position.y + tolerance &&
                    arcArray[k].y > target.transform.position.y - tolerance)
                {

                    //  if (isAllSafe() == true) targetFound();
                    targetFound();
                }
            }

        
        }

      


        return arcArray;
    }

    private bool isAllSafe()
    {
        Debug.Log("Is Safe Called");
        for (int i = 0; i < markerSafe.Length; i++)

        {
            if (markerSafe[i] == false)
                return false;
        }
        return true;
    }

    
         

   // calculate height and distance of each vertex
   Vector3 calculateArcPoint(float t, float maxDistance)
    {
        float x = t * (maxDistance * 1.5f);
        float y = x * Mathf.Tan(radianAngle) - ((g * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }

    IEnumerator angleChange()
    {

        running = true;
        Debug.Log("Angle co-routine called");
        while (running == true)
        {
             yield return new WaitForFixedUpdate();
            

            if (angle < 0) countDown = false;
            if (angle < 45)
            {
                angle = 90;
                velocity++;
              //  resolution = velocity;
            }
            // if (angle > 90) countDown = true;
            if (countDown) angle--;
            else angle++;

            if (velocity > 20)
            {
                running = false;
                resetPause();
            }
            RenderArc();


        }
        
       

    }

    void resetTarget()
    {
        // target.transform.position = new Vector2(Random.Range(-3.0f, 7.0f), Random.Range(-3.0f,1.0f));
        // player.transform.position = new Vector2(player.transform.position.x, Random.Range(-3.0f, 1.0f));
        cityScript.GetComponent<AICityScript>().resetCity();
        
        angle = 90;
        velocity = 2;
        countDown = true;
       // RenderArc();
        running = false;
        lr.material.SetColor("_BaseColor", seekColor);

        if (debugMode == true) StartCoroutine(angleChange());
//        else AIturn();

    }


    public IEnumerator resetPause()
    {
        paused = true;
        yield return new WaitForSeconds(3.0f);
        Destroy(GameObject.FindWithTag("Banana"));
        resetTarget();
        paused = false;
    }


   public void AIturn(out float angleReturn, out float velocityReturn)
    {
        Debug.Log("Ai Turn called");
      //  angle = 90;
      //  countDown = true;
       velocity = 1;
        //  cityScript.GetComponent<AICityScript>().resetCity();
      //     angle = 90;
        //    velocity = 2;
           Debug.Log("Player Position x : " + target.transform.position.x);
           Debug.Log("Player Position y : " + target.transform.position.y);
        // distance = Vector3.Distance(player.transform.position, target.transform.position);
        // running = true;


        /*  while (running == true)
           {



               if (angle > 45)
               {
                      angle--;
               }
               else
               {
                      angle = 90;
                      velocity++;
               Debug.Log("Velocity = " + velocity);
               }

               if (velocity > 20)
               {

               Debug.Log("Couldn't find target!");
               angle = 30;
               velocity = 10;
               targetFound();


               }

         //  Debug.Log("Render Arc");
           RenderArc();

            //   Debug.Log("WhileLoopCalled: " + angle);

           }*/

        // StartCoroutine(angleChange());
        basicDistance(out angle, out velocity);
        Debug.Log("Returning angle");
        if (debugMode) RenderArc();
        angleReturn = angle;
        velocityReturn = velocity;





        
        

            //Debug.Break();




            

        



    }

    public void basicDistance(out float angleReturn, out int velocityReturn)

    {

        angle = 90;
        velocity = 1;
        distance = Vector3.Distance(player.transform.position, target.transform.position);
        float tempDistance = 0;
        while (tempDistance < distance - 0.2f)
        {
            float radianAngle = Mathf.Deg2Rad * angle;
            tempDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / g;
            angle--;
            if (angle < 45)
            {
                angle = 90;
                velocity++;
            }
            Debug.Log("Basic Distance Running");
        }

        angleReturn = angle;
        velocityReturn = velocity;

    }



}
