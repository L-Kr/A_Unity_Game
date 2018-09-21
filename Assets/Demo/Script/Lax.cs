using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lax : MonoBehaviour {

    private Animator m_animator;
    private CharacterController m_control;
    private GameObject m_camera;
    public float MixAngle = -30;
    public float MaxAngle = 30;
    public float Speed = 1f;
    private CD_Manager Lax_cd;

    private int Jump_time = 10;

	void Start () {
        m_animator = GetComponent<Animator>();
        m_control = GetComponent<CharacterController>();
        m_camera = transform.Find("_Main Camera").gameObject;
        Lax_cd = new CD_Manager(7f, 10f, 30f);
    }
	
	void Update () {
        Animator_control();
        float t = Time.deltaTime;
        if (Lax_cd.cdq > 0)
            Lax_cd.cdq -= t;
        if (Lax_cd.cde > 0)
            Lax_cd.cde -= t;
        if (Lax_cd.cdr > 0)
            Lax_cd.cdr -= t;
	}

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

        if (m_skill1 && Lax_cd.cdq <= 0)
        {
            Lax_cd.cdq = Lax_cd.Q;
            m_animator.SetBool("Skill_Q", true);
            GameObject Skill_1 = GameObject.Find("Skill").transform.Find("Attack1").gameObject;
            Skill_1.transform.position = transform.position + transform.forward + transform.up;
            Skill_1.transform.localRotation = transform.localRotation;
            Skill_1.SetActive(true);
        }
        else
        {
            m_animator.SetBool("Skill_Q", false);
        }

        if (m_skill3 && Lax_cd.cde <= 0)
        {
            Lax_cd.cde = Lax_cd.E;
            m_animator.SetBool("Skill_E", true);
            GameObject Skill_3 = GameObject.Find("Skill").transform.Find("Attack2").gameObject;
            GameObject Skill_3_boom = GameObject.Find("Skill").transform.Find("Attack2-2").gameObject;
            Skill_3.transform.position = transform.position + transform.forward + transform.up;
            Skill_3.transform.localRotation = transform.localRotation;
            Skill_3.SetActive(true);
        }
        else
        {
            m_animator.SetBool("Skill_E", false);
        }

        if (m_skill4 && Lax_cd.cdr <= 0)
        {
            StartCoroutine(R_cd());
            m_animator.SetBool("Skill_R", true);
            GameObject Skill = GameObject.Find("Skill").transform.Find("Attack1").gameObject;
            GameObject Skill_4 = Instantiate(Skill, Skill.transform.parent);
            Skill_4.transform.position = transform.position + transform.forward + transform.up;
            Skill_4.transform.localRotation = transform.localRotation;
            Skill_4.SetActive(true);
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

    IEnumerator R_cd()
    {
        yield return new WaitForSeconds(2f);
        Lax_cd.cdr = Lax_cd.R;
    }
}
