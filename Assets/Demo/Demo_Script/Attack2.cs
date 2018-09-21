using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour {

    private GameObject Exprossion;
    private float StartTime;

	// Use this for initialization
	void Start () {
        Exprossion = transform.parent.Find("Attack2-2").gameObject;
	}

    private void OnEnable()
    {
        StartTime = Time.time;
        GetComponent<Rigidbody>().velocity = transform.forward * 25.0f;
    }

    // Update is called once per frame
    void Update () {
        if (Time.time-StartTime > 3.0f)
        {
            Exprossion.SetActive(true);
            Exprossion.transform.position = transform.position;
            gameObject.SetActive(false);
        }
	}
}
