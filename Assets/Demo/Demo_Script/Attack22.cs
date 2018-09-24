using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack22 : MonoBehaviour {

    public ParticleSystem p;
    public float Hurt = 350f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (p.isStopped)
            gameObject.SetActive(false);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().HP_Now -= Hurt;
            Debug.Log(other.gameObject.GetComponent<Enemy>().HP_Now);
        }
    }
}
