using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICityScript : MonoBehaviour
{

    public GameObject[,] buildings;
    public GameObject buildingBlock;
    public GameObject player;
    public GameObject target;
    public int maxHeight;
    public float[] buildingHeights;
    private GameObject[] destroyBuildings;

    public GameObject marker;

    // Start is called before the first frame update
    void Start()
    {
       // generateCity();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void generateCity()
    {
        player.transform.position = new Vector2(-8.5f, -3.5f);
        target.transform.position = new Vector2(8.5f, -3.5f);
        buildings = new GameObject[20, maxHeight];
        buildingHeights = new float[20];
        for (int i = 0; i < 20; i++)
        {
           
            buildingHeights[i] = 0;


            for (int j = 0; j < Random.Range(5, maxHeight); j++)
            {
                buildings[i, j] = Instantiate(buildingBlock, new Vector2(-9.5f + i, -4.77f + (j / 2.0f)), transform.rotation);



                buildingHeights[i] = buildingHeights[i] + 0.5f;

            }


            Instantiate(marker, new Vector2(-9.5f + i, -4.77f+ (buildingHeights[i])), transform.rotation);

        }
        int player1offset = Random.Range(1, 5);
        int player2offset = Random.Range(1, 5);
//        Debug.Log(player1offset);
        player.transform.position = new Vector2(player.transform.position.x + player1offset, player.transform.position.y + (buildingHeights[1 + player1offset] - 1.0f));
        target.transform.position = new Vector2(target.transform.position.x - player2offset, target.transform.position.y + (buildingHeights[18 - player2offset] - 1.0f));



    }

    public void resetCity()
    {

        destroyBuildings = GameObject.FindGameObjectsWithTag("Building");

        for (var i = 0; i < destroyBuildings.Length; i++)
        {
            Destroy(destroyBuildings[i]);
        }



        generateCity();
    }

    public float GetZoneHeight(int zone)
    {

       return  buildingHeights[zone + 1];
       
    }
}


