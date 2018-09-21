using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

    public GameObject Enemy;

    void Start(){

    }

	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
            Instantiate(Enemy, new Vector3(Random.Range(0, 45), 100, Random.Range(0, 45)), transform.rotation);

    }
}
