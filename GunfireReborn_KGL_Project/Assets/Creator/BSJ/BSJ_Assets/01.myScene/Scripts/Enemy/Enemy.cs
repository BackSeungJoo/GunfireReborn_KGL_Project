using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

// �θ� Ŭ����
public class Enemy : MonoBehaviour
{
    public enum Type { Melee, Range, Boss}; // Ÿ�� ( �ٰŸ�, ���Ÿ�, ����)
    public Type enemyType;                  // �� Ÿ��

    public float trackingSpeed;             // ���� ���ǵ�
    public float trackingRange;             // ���� �Ÿ�
    public float attackRange;               // ���� ���� ����

    public Transform targetPlayer;          // �÷��̾� ��ġ
    public PhotonView trackPlayer;           // ���� ������ �÷��̾�
    public Animator animator;               // �ִϸ�����

    public bool isIdle;                     // ��� ����
    public bool isTracking;                 // ���� ����
    public bool isAttacking;                // ���� ����

    public NavMeshAgent nav;                // �׺���̼� ���

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    // ���� ����� �÷��̾� ã��
    public void FindClosestPlayer()
    {
        // ����� ��ũ��Ʈ�� ���� �ִ� ��� Ž��
        PhotonView[] players = GameObject.FindObjectsOfType<PhotonView>();

        // ���� ó�� �Ÿ��� ���Ѵ�� ����
        float closestDistance = Mathf.Infinity;

        // photonview�� ���� �ִ� ��� ��� �߿� Player �±׸� ���� ��� Ž��
        foreach (PhotonView player in players)
        {
            if (player.CompareTag("Player"))
            {
                // Ž�� ������ �Ÿ� ���ϱ�
                float distance = Vector3.Distance(transform.position, player.transform.position);

                // �Ÿ��� closestDistance���� ������ ���� ����� ��� ���� ����
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    trackPlayer = player;
                }
            }
        }

        // ������ ���� closestDistance���� ���� �Ÿ����� �۴ٸ�,
        if (closestDistance <= trackingRange)
        {
            // ���� ����
            nav.isStopped = false;
            isTracking = true;
            isIdle = false;
            animator.SetBool("Track", true);
            animator.SetBool("Idle", false);

            targetPlayer = trackPlayer.transform;
            // Debug.Log(closestDistance);
        }
    }

    // ���� ����
    public void StartAttack()
    {
        // ������ ���·� ���ݸ�Ǹ� ����
        // rb.velocity = Vector3.zero;
        nav.isStopped = true;
        nav.velocity = Vector3.zero;

        isAttacking = true;
        isTracking = false;
        animator.SetBool("Attack", true);
        animator.SetBool("Track", false);
    }

    // �����ϴ� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Attack()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;

        Debug.Log("Attack");

        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if(enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void AttackEnd()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;

        Debug.Log("AttackEnd");
        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if(enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
        isAttacking = false;
        animator.SetBool("Attack", false);
        targetPlayer = null;
    }



    // ��� ����
    public void StartIdle()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;

        isTracking = false;
        animator.SetBool("Track", false);

        targetPlayer = null;
    }
}
