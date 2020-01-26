using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class endOfGame : MonoBehaviour
{

    
    public GameObject winnerName;
    public GameObject player1NameCap;
    public GameObject player2NameCap;
    public GameObject player1ScoreCap;
    public GameObject player2ScoreCap;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void showScores(string player1Name, int player1score, string player2Name, int player2score)
    {
        if (player1score > player2score) winnerName.GetComponent<Text>().text = player1Name;
        if (player2score > player1score) winnerName.GetComponent<Text>().text = player2Name;
        if (player1score == player2score) winnerName.GetComponent<Text>().text = "Tied!";

        player1NameCap.GetComponent<Text>().text = player1Name;
        player2NameCap.GetComponent<Text>().text = player2Name;
        player1ScoreCap.GetComponent<Text>().text = player1score + " wins";
        player2ScoreCap.GetComponent<Text>().text = player2score + " wins";

    }


}
