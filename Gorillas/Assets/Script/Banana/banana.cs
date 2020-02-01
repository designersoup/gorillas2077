using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class banana : MonoBehaviour
{
    
    public float velocity;
    public float gravity;
    public float calcGrav;
    public float angle;
    public int targetID;
    public float vertSpeed;
    public float horzSpeed;
    public GameObject cameraObj;
    public GameObject gameController;

    public GameObject explosion;
    public GameObject explosionFX;
    public GameObject explosionPart;

    public bool gamePaused;

    // Start is called before the first frame update


    void Start()
    {

        cameraObj = GameObject.Find("Main Camera");
        cameraObj.GetComponent<cameraControl>().bananaActive = true;
        gameController = GameObject.Find("GameController");
       
        //  StartCoroutine(bananaLife());
    }

    // Update is called once per frame
    

    private void FixedUpdate()
    {
        if (gamePaused == false)
        {
            transform.Translate((Vector2.right * horzSpeed * Time.deltaTime));

            vertSpeed = vertSpeed - (gravity * Time.deltaTime);
            transform.Translate((Vector2.up * vertSpeed * Time.deltaTime));
            cameraObj.GetComponent<cameraControl>().bananaPosition = transform.position;
        }
       
    }


    private IEnumerator bananaLife()
    {
        yield return new WaitForSeconds(3.0f);
        cameraObj.GetComponent<cameraControl>().bananaActive = false;
        gameController.GetComponent<gameController>().TurnOver();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            gameController.GetComponent<gameController>().TurnOver();
            Instantiate(explosion, transform.position, transform.rotation);
            Instantiate(explosionPart, transform.position, transform.rotation);
            Instantiate(explosionFX, transform.position, transform.rotation);
            FindObjectOfType<audioManager>().ExplosionSFX(Random.Range(0,4));
            cameraObj.GetComponent<cameraControl>().bananaActive = false;
            foreach(Transform child in this.transform)
{
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
            Destroy(other);
            Debug.Log("Building Hit");
            

        }

        if (other.gameObject.CompareTag("Boundry"))
        {
            Debug.Log("Boundry Hit");
            cameraObj.GetComponent<cameraControl>().bananaActive = false;
            
            Destroy(gameObject);
            gameController.GetComponent<gameController>().TurnOver();

        }
    }
}