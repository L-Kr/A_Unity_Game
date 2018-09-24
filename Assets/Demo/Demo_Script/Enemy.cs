using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    private Transform Player;
    private NavMeshAgent Agent;
    private Animator m_animator;
    public float HP_Max = 1000;
    public float MP_Max = 1000;
    public float HP_Now;
    public float MP_Now;
    public float Hp_Recovery = 1f;   //每秒回红
    public float Mp_Recovery = 5f;   //每秒回蓝

    // Use this for initialization
    void Start () {
        Agent = GetComponent<NavMeshAgent>();
        m_animator = GetComponent<Animator>();
        Player = GameObject.Find("Heros").transform;
        HP_Now = HP_Max;
        MP_Now = MP_Max;
	}

    // Update is called once per frame
    void Update() {
        float t = Time.deltaTime;
        Agent.destination = Player.position;
        if (Agent.remainingDistance < 10f)
            m_animator.SetBool("Running", false);
        else
            m_animator.SetBool("Running", true);

        if (HP_Now <= 0)
            m_animator.SetBool("Dying", true);
        else
        {
            HP_Now += t * Hp_Recovery;
            MP_Now += t * Mp_Recovery;
        }
            
    }
}
