using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMover : MonoBehaviour
{
    Rigidbody rigid;
    Transform trans;

    public float force = 10f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
    }

    void Update ()
    {
		if (Input.GetKey(KeyCode.U))
        {
            trans.position += force * Vector3.up * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R))
        {
            rigid.position += force * Vector3.up * Time.deltaTime;
        }
	}
}
