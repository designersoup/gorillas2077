using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject gameContoller;
    public int playerID;
    public bool invunerable;
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
           
}
