using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class Orc : Enemy
{
    public GameObject Attack01_chargeEffect;    // ���� ���� 1 ���� ���� ����Ʈ
    public GameObject Attack01_Effect01;        // ���� ���� 1 ���� �� ����Ʈ 1
    public GameObject Attack01_Effect02;        // ���� ���� 1 ���� �� ����Ʈ 2

    public GameObject StartAttack02_Effect;     // ���� ���� 2 ���� ���� ����Ʈ
    public GameObject Attack02_Effect01;        // ���� ���� 2 ���� �� ����Ʈ 1
    public GameObject Attack02End_Effect;       // ���� ���� 2 ���� �� ����Ʈ
    public GameObject AttackEndStun_Effect;     // ���� ���� 2 ���� ����Ʈ

    public int ranPattern;      // � ������ ������ ���ΰ�?

    public bool isPattern01;    // ���� ���� 1
    public bool isPattern02;    // ���� ���� 2

    public bool isStop;         // ���� ������ �Ǵ�

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
        isStop = false;             // ���� ����

        // ó�� ���� ���ϱ�
        SetNextPattern();
    }

    private void Update()
    {
        // �⺻���´� ��� ����
        if (isTracking == false && isAttacking == false)
        {
            isIdle = true;
            animator.SetBool("Idle", true);
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
            
            if(isAttacking == true && isStop == false)
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
                else if (ranPattern == 1)
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

    #region ���� 1
    // ���� 1 ���� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern01_ChargeEffect()
    {
        // ����Ʈ Ȱ��ȭ
        Attack01_chargeEffect.SetActive(true);
    }

    // ���� 1 ���� ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern01_ChargeEffectEnd()
    {
        // ����Ʈ Ȱ��ȭ
        Attack01_chargeEffect.SetActive(false);
    }


    // ���� 1 �����ϴ� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern01_Attack01()
    {
        // ����Ʈ
        Attack01_Effect01.SetActive(true);
        Attack01_Effect02.SetActive(true);

        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if (enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // ���� 1 ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern01_EndAttack()
    {
        // ����Ʈ
        Attack01_Effect01.SetActive(false);
        Attack01_Effect02.SetActive(false);
        Attack02_Effect01.SetActive(false);

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
    #endregion

    #region ���� 2
    // ���� 2 ���� ���� ����Ʈ ( �ִϸ��̼� ����Ʈ )
    public void Orc_Pattern02_StartAttckEffect()
    {
        // ����
        Stop();

        // ����Ʈ
        StartAttack02_Effect.SetActive(true);
    }

    // ���� 2 �����ϴ� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern02_Attack()
    {
        // ���� ����
        isStop = false;

        // ����Ʈ
        StartAttack02_Effect.SetActive(false);
        Attack02_Effect01.SetActive(true);

        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if (enemyType == Type.Melee)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }

    // ���� 2 ���� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern02_EndAttack()
    {
        // ����Ʈ
        Attack01_Effect01.SetActive(false);
        Attack01_Effect02.SetActive(false);
        Attack02_Effect01.SetActive(false);
        Attack02End_Effect.SetActive(true);
        AttackEndStun_Effect.SetActive(true);


        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if (enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // ���� 2 ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern02_EndStun()
    {
        Attack02End_Effect.SetActive(false);
        AttackEndStun_Effect.SetActive(false);
        isStop = false;
    }


    // ���� 2 ���� ���� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Orc_Pattern02_FinalEnd()
    {
        isAttacking = false;
        isPattern01 = false;
        isPattern02 = false;

        animator.SetBool("Attack", false);
        animator.SetBool("Track", false);
        animator.SetBool("Pattern_01", false);
        animator.SetBool("Pattern_02", false);

        animator.SetBool("Idle", true);
        targetPlayer = null;

        SetNextPattern();
    }
    #endregion

    // ���� ���� ���ϴ� �Լ�
    public void SetNextPattern()
    {
        // ���� ���� ���ϱ�
        ranPattern = Random.Range(0, 2);
    }

    // �����ϴ� �Լ�
    public void Stop()
    {
        isStop = true;
        nav.ResetPath();
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
    }
}
