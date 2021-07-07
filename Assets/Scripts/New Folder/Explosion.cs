using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioManager audioM;

    // Start is called before the first frame update
    void Start()
    {
        audioM = this.GetComponent<AudioManager>();
        audioM.Play("Explo" + Random.Range(1,3));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExplosionEnd()
    {
        Destroy(this.gameObject,2);
    }
}
