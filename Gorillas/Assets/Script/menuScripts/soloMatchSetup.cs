using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class soloMatchSetup : MonoBehaviour
{

    public GameObject gameController;

    public GameObject nameInputP1;
        public GameObject nameInputP2;
    public GameObject namePlaceholderP1;
    public GameObject namePlaceholderP2;
   
    public float[] difficultySettings;
    // Start is called before the first frame update
    void Start()
    {
        namePlaceholderP1.GetComponent<Text>().text = "AI";
        namePlaceholderP2.GetComponent<Text>().text = gameController.GetComponent<gameController>().playerTwo.name; 

    }

   public void SetNames()
    {
        if (nameInputP1.GetComponent<Text>().text !="" ) gameController.GetComponent<gameController>().playerOne.name = nameInputP1.GetComponent<Text>().text;
        if (nameInputP2.GetComponent<Text>().text != "")  gameController.GetComponent<gameController>().playerTwo.name = nameInputP2.GetComponent<Text>().text;

        float tempDiff = difficultySettings[this.GetComponentInChildren<difficultySelect>().value - 1];
        gameController.GetComponent<gameController>().AIAbility = tempDiff;
        gameController.GetComponent<gameController>().StartOnePlayer();

    }

    public void BackButton()
    {
        gameController.GetComponent<gameController>().soloSetupScreen.SetActive(false);
        gameController.GetComponent<gameController>().menuCard.SetActive(true);

    }
}
