using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator anim;
    private float JumpFar = 5.0f;
    public float speed;
    private Vector3 v = new Vector3(0, 0, 0);
    public GameObject Attack1;
    public GameObject Attack2;
    public Transform CameraTrans;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        anim.SetBool("Run", Input.GetAxis("Vertical") > 0);
        anim.SetBool("WalkBackward", Input.GetAxis("Vertical") < 0);
        anim.SetBool("Attack", Input.GetButton("Fire1"));
        anim.SetBool("Jumping", Input.GetButton("Jump"));

        v.x = Input.GetAxis("Horizontal") * 5.0f;
        v.z = Input.GetAxis("Vertical") * 5.0f;
        v = transform.TransformDirection(v);
        if (Input.GetButton("Jump"))
            v.y = speed;
        v.y -= 10 * Time.deltaTime;
        GetComponent<CharacterController>().Move(v * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("Attack1", true);
            Vector3 vp = CameraTrans.position + CameraTrans.forward * 2.0f;
            Instantiate(Attack1, vp, CameraTrans.rotation);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("Attack1", true);
            Vector3 vp = CameraTrans.position + CameraTrans.forward * 3.0f;
            vp.y += 1.0f;
            Instantiate(Attack2, vp, CameraTrans.rotation);
        }
        else
            anim.SetBool("Attack1", false);
    }
}