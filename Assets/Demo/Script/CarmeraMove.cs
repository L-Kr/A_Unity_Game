using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarmeraMove : MonoBehaviour {

    public Vector2 MouseSensitivity = new Vector2(5, 5);
    private float AngleX = 0;
    private float AngleY = 0;
    private GameObject m_camera;

	void Start () {
        m_camera = transform.Find("_Main Camera").gameObject;
        Cursor.lockState = CursorLockMode.Locked;
	}
	
	void Update () {
        float MouseMoveX = Input.GetAxis("Mouse X");
        float MouseMoveY = Input.GetAxis("Mouse Y");

        MouseMoveX *= MouseSensitivity.x;
        MouseMoveY *= MouseSensitivity.y;

        AngleX += MouseMoveX;
        AngleY += MouseMoveY;

        AngleY = AngleY > 360 ? AngleY -= 360 : AngleY;
        AngleY = AngleY < -360 ? AngleY += 360 : AngleY;
        AngleY = Mathf.Clamp(AngleY, -45, 85);

        AngleX = AngleX > 360 ? AngleX -= 360 : AngleX;
        AngleX = AngleX < -360 ? AngleX += 360 : AngleX;

        Quaternion Qx = Quaternion.AngleAxis(AngleX, Vector3.up);
        Quaternion Qy = Quaternion.AngleAxis(0, Vector3.left);
        transform.rotation = Qx * Qy;

        Qy = Quaternion.AngleAxis(AngleY, Vector3.left);
        m_camera.transform.rotation = Qx * Qy;
    }
}
