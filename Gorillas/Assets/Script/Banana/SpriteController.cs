using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(lifetimeCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator lifetimeCo()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);

    }
}
