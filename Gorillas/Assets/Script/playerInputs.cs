using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerInputs : MonoBehaviour
{
    public GameObject gameController;
    public InputField angleInput;
    public InputField forceInput;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire()
    {
        float angle = float.Parse(angleInput.text);
        float force = float.Parse(forceInput.text);
        gameController.GetComponent<gameController>().playerFire(angle, force);
    }

    public void ResetValues()
    {
        angleInput.text = "";
        forceInput.text = "";
    }
}
