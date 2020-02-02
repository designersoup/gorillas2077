using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreRoundDisplay : MonoBehaviour
{
    public GameObject gameController;
    public GameObject player1Caption;
    public GameObject player2Caption;
 
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void preRoundCalled(string p1Name, string p2Name)
    
    {
        player1Caption.GetComponent<Text>().text = p1Name;
        player2Caption.GetComponent<Text>().text = p2Name;

        StartCoroutine(preRoundStart());

    }

    public IEnumerator preRoundStart()
    {
        
        anim.enabled = true;
        anim.Play("preRoundAnimation", 0, 0f);
        yield return new WaitForSeconds(4.0f);
        
        gameController.GetComponent<gameController>().PreGameExit();
        anim.enabled = false;
       
        
    }

    public void PlaySound()
    {
        FindObjectOfType<audioManager>().Play("PreGameSwoosh");
    }

    public void BombDrop()
    {
        FindObjectOfType<audioManager>().ExplosionSFX(1);
    }
}
