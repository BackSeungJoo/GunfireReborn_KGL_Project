using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{


    [SerializeField]
    private float walkSpeed;
    private Rigidbody playerRB;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float dashSpeed;

    private Animator animator;
    //�� ��������
    private CapsuleCollider capsuleCollider;
    private bool isGround = true;
    private bool dashCool = false;

    float _moveDirX = default;
    float _moveDirZ = default;
    void Start()
    {
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>(); 
        playerRB = GetComponent<Rigidbody>();
        
    }

    
    void Update()
    {
        if(!photonView.IsMine)
        {
            return;
        }

        MoveUni();
        CharacterRotate();
        TryJump();
        IsGround();
        Dash();
    }

    private void MoveUni()
    {
        _moveDirX = Input.GetAxisRaw("Horizontal");
        _moveDirZ = Input.GetAxisRaw("Vertical");


        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * walkSpeed;


        playerRB.MovePosition(transform.position + _velocity * Time.deltaTime);

        animator.SetFloat("H", _moveDirX);
        animator.SetFloat("V", _moveDirZ);

    }

    private void CharacterRotate()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        playerRB.MoveRotation(playerRB.rotation * Quaternion.Euler(_characterRotationY));
    }

    private  void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& isGround == true)
        {
            playerRB.velocity = transform.up * jumpForce;
            animator.SetTrigger("Jump");
        }
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position,Vector3.down,capsuleCollider.bounds.extents.y+0.1f);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&& dashCool == false)
        {
            // �뽬 �Է� ������ ĳ������ ���� ��ǥ��� ��ȯ�մϴ�.
            Vector3 dashDirection = transform.TransformDirection(new Vector3(_moveDirX, 0f, _moveDirZ).normalized);

            // �뽬 �Է� ������ �����ϸ� �뽬�մϴ�.
            if (dashDirection.magnitude > 0.1f)
            {
                playerRB.velocity = dashDirection * dashSpeed;
                Invoke("StopDash", 0.1f);
            }
            else
            {
                // �뽬 �Է� ������ ���� ���, �÷��̾ ���� �ٶ󺸴� �������� �뽬�մϴ�.
                dashDirection = transform.forward.normalized;
                playerRB.velocity = dashDirection * dashSpeed;
                Invoke("StopDash", 0.1f);
            }
        }
    }

    private void StopDash()
    {
        dashCool = true;
        Invoke("DashCoolOn", 3f);
        Debug.LogFormat("��Ÿ����");
        playerRB.velocity = Vector3.zero;
    }
    private void DashCoolOn()
    {
        dashCool = false;
        Debug.LogFormat("��Ÿ�ӳ�");
    }
}
