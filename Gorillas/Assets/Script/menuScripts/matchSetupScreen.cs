using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class matchSetupScreen : MonoBehaviour
{

    public GameObject gameController;

    public GameObject nameInputP1;
        public GameObject nameInputP2;
    public GameObject namePlaceholderP1;
    public GameObject namePlaceholderP2;
    // Start is called before the first frame update
    void Start()
    {
        namePlaceholderP1.GetComponent<Text>().text = gameController.GetComponent<gameController>().playerOne.name;
        namePlaceholderP2.GetComponent<Text>().text = gameController.GetComponent<gameController>().playerTwo.name; 

    }

   public void SetNames()
    {
        if (nameInputP1.GetComponent<Text>().text !="" ) gameController.GetComponent<gameController>().playerOne.name = nameInputP1.GetComponent<Text>().text;
        if (nameInputP2.GetComponent<Text>().text != "")  gameController.GetComponent<gameController>().playerTwo.name = nameInputP2.GetComponent<Text>().text;
        gameController.GetComponent<gameController>().GameStart();

    }

    public void BackButton()
    {
        gameController.GetComponent<gameController>().setupScreen.SetActive(false);
        gameController.GetComponent<gameController>().menuCard.SetActive(true);

    }
}
