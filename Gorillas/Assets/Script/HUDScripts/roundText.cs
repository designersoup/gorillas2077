using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roundText : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

   public void StartAnim(int input)
    {
        this.transform.GetComponent<Text>().text = "Round " + input;
        anim = gameObject.GetComponent<Animator>();
        
        anim.Play("roundTextAnim", 0,0f);

    }

    public void PauseAnim()
    {
        anim.enabled = false;
    }

    public void UnPauseAnim()
    {
        anim.enabled = true;
    }
}
