using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winCaption : MonoBehaviour
{
    public GameObject winnerName;
    
    // Start is called before the first frame update
    void Start()
    {

        
        winnerName.GetComponent<Text>().text = "Timmy";
        hidePanel();
        Debug.Log("Win Panel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showPanel(string winner)
    {
        foreach (Transform child in transform)

            child.gameObject.SetActive(true);
        winnerName.GetComponent<Text>().text = winner;


    }

    public void hidePanel()
    {
        foreach(Transform child in transform)
        {
            Debug.Log(child);
            child.gameObject.SetActive(false);
        }
        
    }

    

}
