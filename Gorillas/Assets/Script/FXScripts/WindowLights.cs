using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowLights : MonoBehaviour
{
    public GameObject windowLight;
    public Color[] windowColors;
    // Start is called before the first frame update
    void Start()
    {

        for (int j = 0; j < 3; j++)
        {


            for (int i = 0; i < 5; i++)
            {
                if (Random.Range(0,50) == 1)
                CreateLight(-0.4f + (i * 0.2f), 0.16f - (j * 0.16f));
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateLight(float x, float y)
    {
        GameObject wL = Instantiate(windowLight, new Vector2(0, 0), Quaternion.identity);
        wL.transform.parent = gameObject.transform;
        wL.transform.localPosition = new Vector2(x, y);
        wL.GetComponent<SpriteRenderer>().color = windowColors[Random.Range(0,4)];

    }
}
