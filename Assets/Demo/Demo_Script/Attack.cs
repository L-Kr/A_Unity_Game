using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private float StartTime;
    private GameObject m_camera;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
            Debug.Log("Bingo!");
 //       if (other.tag != "Player")
 //           Destroy(gameObject);
    }

    void Start()
    {
    }

    private void OnEnable()
    {
        if(m_camera == null)
            m_camera = GameObject.Find("Heros").transform.Find("_Main Camera").gameObject;
        StartTime = Time.time;
        StartCoroutine(Wait());
    }

    void Update()
    {
        if (Time.time - StartTime > 5.0f)
        {
            if (name == "Attack1")
                gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
    }

    IEnumerator Wait()
    {
        if(name != "Attack1")
            yield return new WaitForSeconds(1f);
        GetComponent<Rigidbody>().velocity = 20.0f * m_camera.transform.forward;
    }
}
