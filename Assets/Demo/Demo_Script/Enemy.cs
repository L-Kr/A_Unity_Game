using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public Transform Player;
    private NavMeshAgent Agent;

	// Use this for initialization
	void Start () {
        Agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        Agent.destination = Player.position;
        if (Agent.remainingDistance < 2)
        {
            GetComponent<Animator>().SetBool("Running", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Running", true);
        }
    }
}
