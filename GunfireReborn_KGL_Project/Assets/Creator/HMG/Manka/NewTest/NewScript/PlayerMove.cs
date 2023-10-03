using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Photon.Pun;

public class PlayerMove : MonoBehaviourPun
{
    private MainUI mainUI;
    

    [SerializeField]
    private float walkSpeed;
    private Rigidbody playerRB;
    
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
        mainUI = GameObject.Find("MainUICanvas").GetComponent<MainUI>();
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

    

    private  void TryJump()
    {    // Space Ű�� ������ �÷��̾ ���鿡 �ִ� �����̸� "Groggy" �±װ� �ƴ� ���
        if (Input.GetKeyDown(KeyCode.Space)&& isGround == true && 
            !((gameObject.tag =="Groggy")==true))
        {
            // �÷��̾��� Rigidbody�� ���� �������� ���� ���� ����
            playerRB.velocity = transform.up * jumpForce;
            // �ִϸ��̼ǿ��� 'Jump' Ʈ���Ÿ� Ȱ��ȭ�Ͽ� ���� �ִϸ��̼��� ���
            animator.SetTrigger("Jump");
        }
    }
    private void IsGround()
    {
        // �÷��̾��� �Ʒ� �������� ����ĳ��Ʈ�� �߻��Ͽ� ���� �浹 ���θ� Ȯ��
        isGround = Physics.Raycast
        (transform.position,Vector3.down,capsuleCollider.bounds.extents.y+0.1f);
    }

    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)&& dashCool == false)
        {   //LeftShiftŰ�� �Է¹ް� ��Ÿ������ �ƴҶ�
            // �뽬 �Է� ������ ĳ������ ���� ��ǥ��� ��ȯ�մϴ�.
            Vector3 dashDirection = transform.TransformDirection
            (new Vector3(_moveDirX, 0f, _moveDirZ).normalized);
            if (dashDirection.magnitude > 0.1f)
            {   // �뽬 �Է� ������ �����ϸ� �뽬�մϴ�.
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
            // �뽬 ��Ÿ�� ����Ʈ
            mainUI.CountDashCoolTime();
            StartCoroutine(mainUI.DashEffect());
        }
    }
    private void StopDash()
    {
        dashCool = true;
        Invoke("DashCoolOn", 3f);
        playerRB.velocity = Vector3.zero;
    }
    private void DashCoolOn()
    {
        dashCool = false;
    }
}
