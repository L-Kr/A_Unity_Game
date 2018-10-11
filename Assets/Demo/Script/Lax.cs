using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lax : MonoBehaviour {

    private Animator m_animator;    //动画控制
    private CharacterController m_control;      //角色控制
    private GameObject m_camera;        //主摄像机
    private GameObject m_canvas;        //UI场景
    public float Speed = 1f;    //移动速度
    public float Hp_Max = 1000f;  //总红量
    public float Mp_Max = 2000f;  //总蓝量
    public float Hp_Recovery = 1f;   //每秒回红
    public float Mp_Recovery = 5f;   //每秒回蓝
    private float Hp_Now;   //当前红量
    private float Mp_Now;   //当前蓝量
    private CD_Manager Lax_cd;  //Lax的CD系统
    private GameObject Img_Hero_red;    //英雄红量图标
    private GameObject Img_Hero_blue;   //英雄蓝量图标
    private GameObject Img_Skill_Q;     //Q技能cd图标
    private GameObject Img_Skill_E;     //E技能cd图标
    private GameObject Img_Skill_R;     //R技能cd图标

    public int R_Count_Max;
    private int R_Count_Now;   //R技能剩余发射数

    private int Jump_time = 10;     //控制跳跃的最大高度

	void Start () {
        m_animator = GetComponent<Animator>();
        m_control = GetComponent<CharacterController>();
        m_camera = transform.Find("_Main Camera").gameObject;
        m_canvas = GameObject.Find("Canvas");
        Img_Skill_Q = m_canvas.transform.Find("Hero_Skill/Skill_Q").gameObject;
        Img_Skill_E = m_canvas.transform.Find("Hero_Skill/Skill_E").gameObject;
        Img_Skill_R = m_canvas.transform.Find("Hero_Skill/Skill_R").gameObject;
        Img_Hero_red = m_canvas.transform.Find("Hero_Info/HP").gameObject;
        Img_Hero_blue = m_canvas.transform.Find("Hero_Info/MP").gameObject;
        Hp_Now = Hp_Max;
        Mp_Now = Mp_Max;
        R_Count_Now = R_Count_Max;
        Lax_cd = new CD_Manager(7f, 10f, 30f);
        Lax_cd.Set_consume(50f, 75f, 50f);
    }
	
	void Update () {
        Animator_control();
        Skill_Flush();
	}

    /*动画及角色控制*/
    void Animator_control()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        bool m_attack = Input.GetAxis("Fire1") > 0;
        bool m_jump = Input.GetButton("Jump");
        bool m_skill1 = Input.GetKey(KeyCode.Q);
        bool m_skill3 = Input.GetKey(KeyCode.E);
        bool m_skill4 = Input.GetKey(KeyCode.R);

        m_animator.SetBool("Running", h != 0 || v != 0);
        m_animator.SetBool("Attack", m_attack);

        if (m_skill1 && Lax_cd.cdq <= 0 && Mp_Now > Lax_cd.Consume_q)
        {
            Lax_cd.cdq = Lax_cd.Q;
            Mp_Now -= Lax_cd.Consume_q;
            m_animator.SetBool("Skill_Q", true);
            GameObject Skill_1 = GameObject.Find("Skill").transform.Find("Attack1").gameObject;
            //            Skill_1.transform.position = transform.position + transform.forward + transform.up;
            Skill_1.transform.position = m_camera.transform.position + m_camera.transform.forward;
            Skill_1.transform.localRotation = transform.localRotation;
            Skill_1.SetActive(true);
        }
        else
        {
            m_animator.SetBool("Skill_Q", false);
        }

        if (m_skill3 && Lax_cd.cde <= 0 && Mp_Now > Lax_cd.Consume_e)
        {
            Lax_cd.cde = Lax_cd.E;
            Mp_Now -= Lax_cd.Consume_e;
            m_animator.SetBool("Skill_E", true);
            GameObject Skill_3 = GameObject.Find("Skill").transform.Find("Attack2").gameObject;
            Skill_3.transform.position = transform.position + transform.forward + transform.up;
            Skill_3.transform.localRotation = transform.localRotation;
            Skill_3.SetActive(true);
        }
        else
        {
            m_animator.SetBool("Skill_E", false);
        }

        if (m_skill4 && Lax_cd.cdr <= 0 && Mp_Now > Lax_cd.Consume_r)
        {
            if(R_Count_Now == R_Count_Max)
                StartCoroutine(R_cd());
            R_Count_Now--;
            if (R_Count_Now == 0)
            {
                Lax_cd.cdr = Lax_cd.R;
                R_Count_Now = R_Count_Max;
            }
            Mp_Now -= Lax_cd.Consume_r;
            m_animator.SetBool("Skill_R", true);
            GameObject Skill = GameObject.Find("Skill").transform.Find("Attack1").gameObject;
            GameObject Skill_4 = Instantiate(Skill, Skill.transform.parent);
            Skill_4.transform.position = transform.position + transform.forward + transform.up;
            Skill_4.transform.localRotation = transform.localRotation;
            Skill_4.SetActive(true);
            Skill_4.GetComponent<Attack>().Hurt = 75f;
        }
        else
        {
            m_animator.SetBool("Skill_R", false);
        }

        if (m_animator.GetCurrentAnimatorStateInfo(0).IsName("Skill4"))
            return;

        Vector3 moveDirection = Vector3.zero;
        moveDirection = new Vector3(h, 0, v);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed;
        if (m_jump && Jump_time != 0)
        {
            Jump_time--;
            moveDirection.y = 20f;
        }
        if (m_control.isGrounded)
            Jump_time = 10;
        moveDirection.y -= 5f;
        m_control.Move(moveDirection * Time.deltaTime);
    }

    /*技能刷新及每秒回复红蓝*/
    void Skill_Flush()
    {
        float t = Time.deltaTime;
        if (Lax_cd.cdq > 0)
        {
            Img_Skill_Q.GetComponent<Slider>().value = (Lax_cd.Q - Lax_cd.cdq) / Lax_cd.Q;
            Lax_cd.cdq -= t;
        }
        if (Lax_cd.cde > 0)
        {
            Img_Skill_E.GetComponent<Slider>().value = (Lax_cd.E - Lax_cd.cde) / Lax_cd.E;
            Lax_cd.cde -= t;
        }
        if (Lax_cd.cdr > 0)
        {
            Img_Skill_R.GetComponent<Slider>().value = (Lax_cd.R - Lax_cd.cdr) / Lax_cd.R;
            Lax_cd.cdr -= t;
        }

        Hp_Now += Hp_Recovery * t;
        Mp_Now += Mp_Recovery * t;
        if (Hp_Now > Hp_Max)
            Hp_Now = Hp_Max;
        if (Mp_Now > Mp_Max)
            Mp_Now = Mp_Max;
        Img_Hero_red.GetComponent<Slider>().value = Hp_Now / Hp_Max;
        Img_Hero_blue.GetComponent<Slider>().value = Mp_Now / Mp_Max;
    }

    IEnumerator R_cd()
    {
        yield return new WaitForSeconds(1f);
        if (R_Count_Now != R_Count_Max)
        {
            Lax_cd.cdr = Lax_cd.R;
            R_Count_Now = R_Count_Max;
        }
    }
}
