using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsScriptTest : MonoBehaviour
{
    public int angle;
    public int speed;
    public float vertSpeed;
    public float horzSpeed;
    public float gravity;
    public float calcGrav;
    public float scaler;
    // Start is called before the first frame update
    void Start()
    {
        vertSpeed = Mathf.Sin((angle * Mathf.PI) / 180) * speed;
        horzSpeed = Mathf.Cos((angle * Mathf.PI) / 180) * speed;
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate((Vector2.right * horzSpeed * Time.deltaTime) * scaler);

       vertSpeed = vertSpeed -  (gravity * Time.deltaTime);
        transform.Translate((Vector2.up * vertSpeed * Time.deltaTime) * scaler);
    }
}
