using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
       // anim.SetBool("isIdle", true);
       
    }

    // Update is called once per frame
    void Update()
    {
       /* if (anim.GetCurrentAnimatorStateInfo(0).IsName("throw"))
        {
            anim.SetBool("isThrowing", false);

        }*/
    }

    public void setAnim(string input)
    {
        switch (input)
        {
            case "idle":
                anim.SetBool("isIdle", true);
                anim.SetBool("isThrowing", false);
                anim.SetBool("isCelebrating", false);

                break;

            case "default":
                anim.SetBool("isIdle", false);
                anim.SetBool("isThrowing", false);
                anim.SetBool("isCelebrating", false);
                break;

            case "throw":
                anim.SetBool("isThrowing", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isCelebrating", false);

                break;

            case "celebrate":
                anim.SetBool("isCelebrating", true);
                anim.SetBool("isIdle", false);
                anim.SetBool("isThrowing", false);
                break;


        }
        
    }
}
