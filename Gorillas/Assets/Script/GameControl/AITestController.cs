using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITestController : MonoBehaviour
{

    public GameObject player;
    public GameObject target;

    public float speed;
    public float angle;
    public float gravity;
    public float distance;
    public float bananaRange = 0.0f;
    public bool firing;
    public float counter;


    public GameObject markerDot;


    // Start is called before the first frame update
    void Start()
    {


        // StartCoroutine(testFiring());
        // player.GetComponent<AI>().AIFire();
        firing = true;
        resetTarget();


    }

    // Update is called once per frame
    void Update()
    {
       

        
    }

   


    public void landingCalculator()
    {


        angle = 90;
        distance = Vector3.Distance(player.transform.position, target.transform.position);
        distance = distance - 0.5f;
        bananaRange = (((speed * speed) * Mathf.Sin(((2 * angle) * Mathf.PI) / 180)) / gravity);
        

        while (bananaRange < distance)
        {
            bananaRange = (((speed * speed) * Mathf.Sin(((2 * angle) * Mathf.PI) / 180)) / gravity);
            angle = angle - 1.0f;
            
            
            
        }

        drawDots();





        //  player.GetComponent<AI>().AIFire(angle, speed);



    }

    public void positionTarget()
    {
        target.transform.position = new Vector2(Random.Range(-3.5f, 7.6f), target.transform.position.y);
    }

   

    public void resetTarget()
    {

        target.SetActive(true);
        positionTarget();
        landingCalculator();
    }

    public void drawDots()

    {
        float startPosition = player.transform.position.x;
        float increment = (target.transform.position.x - player.transform.position.x) / 19;
        float upForce = 4f;
        float flightTime = 2.0f;
        float timeInc = 2.0f / 19;
        float grav = 0.98f;

        for (int i = 0; i < 20; i++)
        {
            upForce = upForce - grav;
            Instantiate(markerDot, new Vector2 (startPosition + (i * increment), player.transform.position.y + upForce), transform.rotation);
            
        }
    }




}
