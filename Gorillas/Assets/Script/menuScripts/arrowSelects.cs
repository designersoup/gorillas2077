using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrowSelects : MonoBehaviour
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
        displayText.text = value.ToString() + "" + suffix;
    }
}


