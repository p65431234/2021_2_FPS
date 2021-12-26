using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed = 1f;

    void Move()
    {
        float xmove = Input.GetAxis("Horizontal");
        float zmove = 0f;

        if (Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.UpArrow))
        {
            zmove = 1f;
        }

        if (Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.DownArrow))
        {
            zmove = -1f;
        }


        Vector3 temppos = transform.position;
        temppos.x += xmove * MoveSpeed * Time.deltaTime;
        temppos.z += zmove * MoveSpeed * Time.deltaTime;

        transform.position = temppos;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
