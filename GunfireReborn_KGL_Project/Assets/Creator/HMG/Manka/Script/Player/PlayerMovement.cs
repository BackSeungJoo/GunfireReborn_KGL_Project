using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotateSpeed = 7f;
    public float jumpForce = 5f;
    public float dashSpeed;
    public bool isJumping;
    public bool dashCool;
    
    public Vector3 movement;
    private PlayerInput playerInput;
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;

    private float lookSensitivity =5f;

    // TEST : 
    float y_Rotation = default;
    Quaternion rotationY = default;
    // Start is called before the first frame update
    void Start()
    {
        //if (!photonView.IsMine)
        //{
        //    return;
        //}
        playerInput = GetComponent<PlayerInput>();
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Cursor.lockState = CursorLockMode.Locked; // ���콺 ���
        //Cursor.visible = false; // ���콺 �����

        //playerRigidbody.transform.position = new Vector3 (transform.position.x, 5, transform.position.z);

        Dash();

        Move();

        Rotate();
    }

    private void Move()
    {
        movement.x = playerInput.HMove;
        movement.z = playerInput.VMove;
        if (playerInput.jump == true && isJumping == false)
        { //�÷��̾ �������̾ƴϸ鼭 ����Ű�� �������� �����ϵ��� ������
            playerAnimator.Play("Jumping", -1, 0);
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, jumpForce, playerRigidbody.velocity.z);
            playerInput.jump = false;
            isJumping = true;
        }
        else if(!(isJumping == true))
        { //�÷��̾ �������� �ƴ϶�� �̵��� �����ϰ� ������
            playerRigidbody.velocity = transform.TransformDirection(movement * moveSpeed)+ Vector3.up * playerRigidbody.velocity.y;
            
           /* Vector3 movementX = transform.right * playerInput.HMove;
            Vector3 movementZ = transform.right * playerInput.VMove;

            movement = (movementX + movementZ).normalized*moveSpeed;

            playerRigidbody.MovePosition(transform.position + movement* Time.deltaTime);*/
            
            playerAnimator.SetFloat("H", movement.x); 
            playerAnimator.SetFloat("V", movement.z);
        }
    }
    private void Rotate()
    {
        

       // Debug.LogFormat("before{0}", gameObject.transform.position.y);
        float y_Rotation = Input.GetAxis("Mouse X");

        //Debug.LogFormat("{0}", y_Rotation);
        Vector3 rotationY = new Vector3(0f, y_Rotation*lookSensitivity, 0f); //* lookSensitivity;
        //Debug.LogFormat("{0}", rotationY);
        /* // TEST : 
         y_Rotation = Input.GetAxisRaw("Mouse X");
         rotationY = Quaternion.Euler(0f, y_Rotation, 0f) ;
        playerRigidbody.MoveRotation(playerRigidbody.rotation * rotationY);

         Debug.LogFormat("{0}", playerRigidbody.velocity);*/
        playerRigidbody.rotation *= Quaternion.Euler(rotationY);
        Debug.LogFormat("{0}", playerRigidbody.position.y);

        /*Debug.LogFormat("after{0}", gameObject.transform.position.y);*/
        //playerRigidbody.rotation = playerRigidbody.rotation * Quaternion.Euler(0f, playerInput.RMove * rotateSpeed, 0f);

        //transform.Rotate(0f, y_Rotation * rotateSpeed, 0f);
        //Debug.LogFormat("{0}", playerRigidbody.velocity);

    }

    private void Dash()
    {
        if (playerInput.dash == true && dashCool ==false)
        {
         // �뽬 �Է� ������ ĳ������ ���� ��ǥ��� ��ȯ�մϴ�.
            Vector3 dashDirection = transform.TransformDirection(new Vector3(playerInput.HMove, 0f, playerInput.VMove).normalized);

            // �뽬 �Է� ������ �����ϸ� �뽬�մϴ�.
            if (dashDirection.magnitude > 0.1f)
            {
                playerRigidbody.velocity = dashDirection * dashSpeed;
                Invoke("StopDash", 0.1f);
                Debug.LogFormat("�뽬����");
            }
            else
            {
                // �뽬 �Է� ������ ���� ���, �÷��̾ ���� �ٶ󺸴� �������� �뽬�մϴ�.
                dashDirection = transform.forward.normalized;
                playerRigidbody.velocity = dashDirection * dashSpeed;
                Invoke("StopDash", 0.1f);
                Debug.LogFormat("�뽬����");
            }
        }
    }

    private void StopDash()
    {
        playerInput.dash = false;
        dashCool = true;
        Invoke("DashCoolOn", 3f);
        Debug.LogFormat("��Ÿ����");
    }
    private void DashCoolOn()
    {
        dashCool = false;
        playerInput.dash = false;
        //���Է¹����� �ذ��Ϸ��� false�� ��Ÿ�� ������ false�� ��������
        Debug.LogFormat("��Ÿ�ӳ�");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Ground")
        {
            isJumping = false;
        }
    }
}
