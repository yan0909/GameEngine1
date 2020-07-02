using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;


public class Controller : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator animator;

    //public float jumpPower = 8;
    public float moveSpeed = 1;
    public float runSpeed = 5;

    //private bool isJumping;
    public Text uiText;
    private int giftCount;
    private Collider touchedGift;

    public FollowCam cam;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        //isJumping = false;
        giftCount = 0;
    }

    void Update()
    {
        Move();
        //Jump();

        uiText.text = $"Gift {giftCount}/5";
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

    /*void Jump()
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
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Box"))
            return;

        //Destroy(other.gameObject);
        other.enabled = false;
        other.transform.localScale = Vector3.zero;
        giftCount++;
        // 사운드
        cam.PlayGiftSound(other.GetComponent<AudioSource>());
        //audioSource.Play();
    }
}