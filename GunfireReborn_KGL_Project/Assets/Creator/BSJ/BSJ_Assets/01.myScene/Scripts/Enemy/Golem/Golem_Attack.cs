using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Golem_Attack : MonoBehaviour
{
    public GameObject attack01_HitBox;
    public GameObject attack02_HitBox;

    private Animator animator;
    private bool isAttack;

    NavMeshAgent agent;
    MyGolem enemy;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<MyGolem>();
        agent = GetComponent<NavMeshAgent>();
    }


    // �ִϸ��̼� �̺�Ʈ
    public void Golem_StartAttack()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        Golem_LookTargetPlayer();
    }

    public void Golem_Attack01()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        attack01_HitBox.SetActive(true);
    }

    public void Golem_Attack02()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        attack02_HitBox.SetActive(true);
    }

    public void Golem_AttackEnd()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        attack01_HitBox.SetActive(false);
        attack02_HitBox.SetActive(false);
    }

    public void Golem_ResetState()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        animator.SetBool("Idle", false);
        animator.SetBool("Track", true);
    }

    public void Golem_LookTargetPlayer()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        // Ÿ�� �÷��̾� �ٶ󺸱� && y�� ȸ�� ����
        Vector3 targetDirection = enemy.trackPlayer.transform.position - transform.position;
        targetDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(targetDirection.normalized);

        // �ε巯�� ȸ�� ����
        float rotationSpeed = 0.5f; // ���� ������ ȸ�� �ӵ�
        transform.rotation = Quaternion.Slerp(transform.rotation, enemy.trackPlayer.transform.rotation, rotationSpeed * Time.deltaTime);
    }
}