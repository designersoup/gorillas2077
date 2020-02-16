using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    public GameObject skyscraperPF;
    public float parallaxScale;
   
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 25 / parallaxScale; i++)
        {
            var tower = Instantiate(skyscraperPF, new Vector2(-9.5f + i * parallaxScale, 0f), transform.rotation);
            tower.transform.parent = this.transform;
            

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void newBlock()
    {
        var tower = Instantiate(skyscraperPF, new Vector2(13.5f, 0f), transform.rotation);
        tower.transform.parent = this.transform;
    }

    public void destroyCity()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

}
