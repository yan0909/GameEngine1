using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody rigid;

    private float jumpPower = 4;
    private float moveSpeed = 1;
    private float runSpeed = 2;

    private bool isJumping;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        isJumping = false;
    }

    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate((new Vector3(h, 0, v) * moveSpeed) * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            transform.Translate((new Vector3(h, 0, v) * runSpeed) * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //바닥에 있으면 점프를 실행
            if (!isJumping)
            {
                isJumping = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }
            else
            {
                return;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //바닥에 닿으면
        if (collision.gameObject.CompareTag("Ground"))
        {
            //점프가 가능한 상태로 만듦
            isJumping = false;
        }
    }
}