using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject gameContoller;
    public int playerID;
    public bool invunerable;
    public GameObject bananaPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Banana")) 
        {



            if (invunerable == false)
            {
                gameContoller.GetComponent<gameController>().playerHit(playerID);
                Destroy(other);
            }

            
        }
    }

    public void StartInv ()
    {
        StartCoroutine(Invunerable());
    }

    public IEnumerator Invunerable()
    {
        invunerable = true;
        yield return new WaitForSeconds(0.2f);
            invunerable =  false;

    }


    public void PlayerFire(float angleInput, float forceInput, int ID)
    {
        
            
            {

                StartInv();

            
                 GameObject banana = Instantiate(bananaPrefab, this.transform.position, Quaternion.identity);
            if (ID == 2)
            {
                banana.transform.rotation = Quaternion.Euler(0, 180f, 0);
            }
               
                banana.GetComponent<banana>().vertSpeed = Mathf.Sin((angleInput * Mathf.PI) / 180) * forceInput;
                banana.GetComponent<banana>().horzSpeed = Mathf.Cos((angleInput * Mathf.PI) / 180) * forceInput;
                

                if (ID == 1) banana.GetComponent<banana>().targetID = 2;
                else banana.GetComponent<banana>().targetID = 1;

            this.GetComponent<animationController>().setAnim("throw");



            }

            FindObjectOfType<audioManager>().Play("BananaThrown");

            // StartCoroutine(turnTimer());


        }
    }
           

