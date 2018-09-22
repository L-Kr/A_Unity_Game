using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2 : MonoBehaviour {

    private GameObject Exprossion;
    private float StartTime;
    private GameObject m_camera;

	// Use this for initialization
	void Start () {
        Exprossion = transform.parent.Find("Attack2-2").gameObject;
	}

    private void OnEnable()
    {
        if(m_camera == null)
            m_camera = GameObject.Find("Heros").transform.Find("_Main Camera").gameObject;
        StartTime = Time.time;
        GetComponent<Rigidbody>().velocity = (m_camera.transform.forward + new Vector3(0f, 0.2f, 0f)) * 25.0f;
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
