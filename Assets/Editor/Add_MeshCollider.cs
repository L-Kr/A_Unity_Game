using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Add_MeshCollider : MonoBehaviour {

    [MenuItem("MyEditors/AddMeshCollider")]
    static void Init () {
        GameObject Step = GameObject.Find("Rock");
        for (int i = 0; i < Step.transform.childCount; i++)
        {
            Transform t = Step.transform.GetChild(i);
            if (t.GetComponent<MeshRenderer>() != null)
                t.gameObject.AddComponent<MeshCollider>();
            else
            {
                for (int j = 0; j < t.childCount; j++)
                {
                    Transform temp = t.GetChild(j);
                    if (temp.GetComponent<MeshRenderer>() != null)
                    {
                        temp.gameObject.AddComponent<MeshCollider>();
                        Debug.Log(temp.gameObject.name + "---has added");
                    }
                }
            }
        }
	}
	
}
