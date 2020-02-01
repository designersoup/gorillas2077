using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class turnTimer : MonoBehaviour
{
    public float timeLeft;
    public bool timeUp;
    public bool turnStopped;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        
        timeUp = false;
    }

    public void StartTimer(float input)
    {
        turnStopped = false;
        timeLeft = input;
        StartCoroutine(turnTiming());
        
    }

    public void StopTimer()
    {
        turnStopped = true;
        
    }

    

    public IEnumerator turnTiming()
    {
        while (timeLeft > 0 && turnStopped == false)
        {


            yield return new WaitForSeconds(0.1f);
            timeLeft = timeLeft - 0.1f;
            this.transform.GetComponent<Text>().text = timeLeft.ToString("F1");

        }
        if (timeLeft < 0) timeLeft = 0.0f;
        this.transform.GetComponent<Text>().text = timeLeft.ToString("F1");


        timeUp = true;
        TimeUp();


    }


    public void TimeUp()
    {
        

        if (timeLeft == 0)
        {
            this.transform.GetComponent<Text>().text = timeLeft.ToString("Time Up!");
            gameController.GetComponent<gameController>().TurnTimerUp();
        }

    }

    
}