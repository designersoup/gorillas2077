using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class difficultySelect : MonoBehaviour
{

    public int value;
    public int lowerLimit;
    public int upperLimit;
     public Text displayText;
    public string suffix;

    // Start is called before the first frame update
    void Start()
    {
        UpdateDisplay();
    }

   public void ArrowUp()
    {
        if (value < upperLimit)
        {
            value++;
        }
        UpdateDisplay();

    }

    public void ArrowDown()
    {
        if (value > lowerLimit)
        {
            value--;
        }

        UpdateDisplay();

    }

    void UpdateDisplay()
    {
        string level = "easy";
        switch (value)
        {
            case 1:
                level = "clueless";
                break;
            case 2:
                level = "laid back";
                    break;
            case 3:
                level = "hot shot";
                    break;
            case 4:
                level = "unbeatable";
                break;

            
        }

        displayText.text = level;
    }
}


