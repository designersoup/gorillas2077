using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public bool active;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AIFire(float angle, float force)
    {
        StartCoroutine(AIWait(angle, force));
    }

    public IEnumerator AIWait(float angle, float force)
    {
        yield return new WaitForSeconds(Random.Range(0.4f, 0.9f));
        this.GetComponent<playerScript>().PlayerFire(angle, force, 1);
        Debug.Log("AI Fired");

    }
}
