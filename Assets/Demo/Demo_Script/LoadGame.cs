using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour {

    private GameObject m_canvas;
    private Slider m_slider;
    private Button m_login;
    private List<Button> m_herochoic;
    private Text m_tips;
    private GameObject m_username;
    private GameObject m_password;
    AsyncOperation m_operation;

	void Start () {
        m_canvas = GameObject.Find("Canvas");
        m_slider = m_canvas.transform.Find("Slider").GetComponent<Slider>();
        m_login = m_canvas.transform.Find("LogIn/Button").GetComponent<Button>();
        m_login.onClick.AddListener(Log_in);
        m_tips = m_canvas.transform.Find("Tip").GetComponent<Text>();
        m_username = m_canvas.transform.Find("LogIn/User_Input").gameObject;
        m_password = m_canvas.transform.Find("LogIn/Password_Input").gameObject;
        m_herochoic = new List<Button>();
        Transform Heros = m_canvas.transform.Find("Hero_Choic");
        for (int i = 1; i < Heros.childCount; i++)
            m_herochoic.Add(Heros.GetChild(i).GetComponent<Button>());
        foreach (Button b in m_herochoic)
            b.onClick.AddListener(delegate () { Hero_Choic(b.transform); });
        StartCoroutine(GameLoading());
	}
	
	void Update () {
        float t = m_operation.progress;
        if (t >= 0.9f)
            t = 1f;
        if (t != m_slider.value)
            m_slider.value = Mathf.Lerp(m_slider.value, t, Time.deltaTime);
	}

    IEnumerator GameLoading()
    {
        m_operation = SceneManager.LoadSceneAsync("Game");
        m_operation.allowSceneActivation = false;
        yield return m_operation;
    }

    IEnumerator Tips_show()
    {
        yield return new WaitForSeconds(2.0f);
        m_tips.gameObject.SetActive(false);
        m_login.enabled = true;
    }

    void Log_in()
    {
        string user = m_username.GetComponent<InputField>().text;
        string pass = m_password.GetComponent<InputField>().text;
        Debug.Log(user + "+" + pass);
        Demo_SQL.Instance().Username = user;
        if(m_canvas.transform.Find("Hero_Choic/Choic").gameObject.activeSelf == false)
        {
            m_tips.text = "请选择英雄！";
            m_tips.gameObject.SetActive(true);
            m_login.enabled = false;
            StartCoroutine(Tips_show());
        }
        else if(user != "Helloworld" || pass != "123456")
        {
            m_tips.text = "用户名或密码错误！";
            m_tips.gameObject.SetActive(true);
            m_login.enabled = false;
            StartCoroutine(Tips_show());
        }
        else
        {
            m_operation.allowSceneActivation = true;
        }
    }

    void Hero_Choic(Transform h_trans)
    {
        GameObject m_choic = m_canvas.transform.Find("Hero_Choic/Choic").gameObject;
        if (!m_choic.activeSelf)
            m_choic.SetActive(true);
        m_choic.transform.position = h_trans.position;
        Demo_SQL.Instance().Heroname = h_trans.name;
    }
}
