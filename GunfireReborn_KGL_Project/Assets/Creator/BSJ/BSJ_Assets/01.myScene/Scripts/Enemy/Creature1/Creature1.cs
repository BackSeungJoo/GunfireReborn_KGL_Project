using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class Creature1 : Enemy
{
    public GameObject chargeEffect; // ���� ���� ����Ʈ

    public int ranPattern;      // � ������ ������ ���ΰ�?

    public bool isPattern01;    // ���� ���� 1
    public bool isPattern02;    // ���� ���� 2

    private void Awake()
    {
        enemyType = Type.Melee;

        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        isIdle = true;              // ��� ����
        isTracking = false;         // ���� ����
        isAttacking = false;        // ���� ����
        isPattern01 = false;        // ���� 1
        isPattern02 = false;        // ���� 2
    }

    private void Update()
    {
        // �⺻���´� ��� ����
        if (isTracking == false && isAttacking == false)
        {
            isIdle = true;
            animator.SetBool("Idle", true);
            ranPattern = Random.Range(0, 2);    // ���� ���ϱ�
        }

        // �÷��̾���� ��ġ�� ���ؼ� ���� �Ÿ������� �ٰ����� ����
        FindClosestPlayer();

        // ���� ����� �ִٸ�
        if (targetPlayer != null)
        {
            // ���� ��� �ٶ󺸱� ( y ���� �������Ѽ� �������̰� �־ �̻��ϰ� ȸ������ ���� )
            targetDirection = targetPlayer.position - transform.position;
            targetDirection.y = 0;
            transform.rotation = Quaternion.LookRotation(targetDirection.normalized);

            if (isAttacking == false)
            {
                // ���� �÷��̾ ���ؼ� �̵��Ѵ�.
                nav.SetDestination(targetPlayer.position);
            }

            // ���� ( ���� �÷��̾ ���ݹ��� �ȿ� ������ )
            if (Vector3.Distance(transform.position, targetPlayer.position) <= attackRange)
            {
                // ���� 01
                if (ranPattern == 0)
                {
                    isPattern01 = true;
                    isPattern02 = false;
                    animator.SetBool("Pattern_01", true);
                    animator.SetBool("Pattern_02", false);

                    StartAttack();
                }

                // ���� 02
                else if(ranPattern == 1)
                {
                    isPattern01 = false;
                    isPattern02 = true;
                    animator.SetBool("Pattern_01", false);
                    animator.SetBool("Pattern_02", true);

                    StartAttack();
                }
            }

            // ��� ( ���� ���� ������ �÷��̾ ������ )
            else if (Vector3.Distance(transform.position, targetPlayer.position) > trackingRange)
            {
                StartIdle();
            }
        }
    }

    // ���� 1 ���� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void ChargeEffect_Creature1()
    {
        // ����Ʈ Ȱ��ȭ
        chargeEffect.SetActive(true);
    }

    // ���� 1 ���� ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void ChargeEffectEnd_Creature1()
    {
        // ����Ʈ Ȱ��ȭ
        chargeEffect.SetActive(false);
    }


    // ���� 1 �����ϴ� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Attack01_Creature1()
    {
        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if (enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // ���� 2 �����ϴ� ���� ( �ִϸ��̼� �̺�Ʈ )

    public void Attack02_Creature1()
    {
        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if (enemyType == Type.Melee)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void AttackEnd_Creature1()
    {
        // Debug.Log("AttackEnd");
        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if (enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }

        isAttacking = false;
        isPattern01 = false;
        isPattern02 = false;
        animator.SetBool("Attack", false);
        animator.SetBool("Track", false);
        animator.SetBool("Pattern_01", false);
        animator.SetBool("Pattern_02", false);

        animator.SetBool("Idle", true);
        targetPlayer = null;

        // ���� ���� ���ϱ�
        ranPattern = Random.Range(0, 2);
    }
}
