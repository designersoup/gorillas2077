using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyscraperGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject buildingBlock;
    public int min;
    public int max;
    public GameObject cityBuilder;
    public GameObject gorilla;
    public Sprite[] buildingBlocks;
    public bool playerBlock = false;
    private int stories = 0;
    public float parallaxScale;
    public float offsetHeight;

    void Awake()
    {
        if (parallaxScale == 1)cityBuilder = GameObject.Find("ScrollingCity");
        else cityBuilder = GameObject.Find("ScrollingCityLayer2");
        int rnd = Random.Range(min, max);
        int rndTwo = Random.Range(2, 7);
        int rndGorilla = Random.Range(0, 12);
        if (rndGorilla == 10) playerBlock = true; 
        
        for (int i = 0; i < rnd; i++)
        {


            var story = Instantiate(buildingBlock, new Vector2(this.transform.position.x, this.transform.position.y - 4.77f + offsetHeight + (i/2.0f * parallaxScale)), transform.rotation);
            story.transform.parent = this.transform;
            if (i > rnd - rndTwo) story.GetComponent<SpriteRenderer>().sprite = buildingBlocks[Random.Range(1,5)];
            else story.GetComponent<SpriteRenderer>().sprite = buildingBlocks[0];
            if (parallaxScale == 0.5)
            {
                story.GetComponent<SpriteRenderer>().sortingOrder = -5;
                story.transform.localScale = new Vector2(0.5f, 0.5f);
            }
                stories++;
            

        }
        if (playerBlock) {
            var gorillaBlock = Instantiate(gorilla, new Vector2(this.transform.position.x, this.transform.position.y - 4.77f + offsetHeight + (stories / (2.0f / parallaxScale)) + 0.25f * parallaxScale), transform.rotation);
            gorillaBlock.transform.parent = this.transform;
            int rndDirection = Random.Range(0,3);
            if (rndDirection == 1)
            {
                gorillaBlock.transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            else
            {
                gorillaBlock.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (parallaxScale == 0.5)
            {
                gorillaBlock.GetComponent<SpriteRenderer>().sortingOrder = -5;
                gorillaBlock.transform.localScale = new Vector2(0.5f, 0.5f);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
       

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * (2.0f * parallaxScale) * Time.deltaTime);
        if (this.transform.position.x < -9.4f)
        {
            //Call function to generate new block
            foreach (Transform child in this.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            cityBuilder.GetComponent<CityBuilder>().newBlock();
            Destroy(this.gameObject);

        }
    }
}
