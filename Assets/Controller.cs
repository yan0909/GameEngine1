using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator animator;

    private float jumpPower = 4;
    private float moveSpeed = 1;
    private float runSpeed = 4;

    private bool isJumping;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
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

        if (h != 0f || v != 0f)
        {
            Vector3 lerpLookPos = Vector3.Lerp(transform.forward, new Vector3(h, 0, v), 0.75f);
            transform.LookAt(transform.position + lerpLookPos);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetInteger("runLevel", 2);
                transform.Translate((new Vector3(h, 0, v) * runSpeed) * Time.deltaTime, Space.World);
            }
            else
            {
                animator.SetInteger("runLevel", 1);
                transform.Translate((new Vector3(h, 0, v) * moveSpeed) * Time.deltaTime, Space.World);
            }
        }
        else
        {
            animator.SetInteger("runLevel", 0);
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