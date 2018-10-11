using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Control : MonoBehaviour {

    private GameObject m_canvas;
    private Text m_username;
    private Text m_heroname;

	void Start () {
        m_canvas = GameObject.Find("Canvas");
        m_username = m_canvas.transform.Find("Hero_Info/Player_name").gameObject.GetComponent<Text>();
        m_username.text = Demo_SQL.Instance().Username;
        m_heroname = m_canvas.transform.Find("Hero_Info/Hero_name").gameObject.GetComponent<Text>();
        m_heroname.text = Demo_SQL.Instance().Heroname;
	}
	
	void Update () {
		
	}
}
