using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniDragon : MeleeEnemyController
{
    public GameObject attackHitBox; // ���� ����
    public GameObject floorEffect;  // �ٴ� ����Ʈ
    public GameObject chargeEffect; // ���� ���� ����Ʈ

    private void Awake()
    {
        enemyType = Type.Melee;

        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        isIdle = true;              // ��� ����
        isTracking = false;         // ���� ����
        isAttacking = false;        // ���� ����
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
            // ���� �÷��̾ �ٶ󺻴�. (�극�� �� ���� �ٶ󺸰� �ϱ� ����)
            // transform.LookAt(targetPlayer);

            // y�� ȸ�� ����
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
                StartAttack();
            }

            // ��� ( ���� ���� ������ �÷��̾ ������ )
            else if (Vector3.Distance(transform.position, targetPlayer.position) > trackingRange)
            {
                StartIdle();
            }
        }
    }

    // �극�� ������ ( �ִϸ��̼� �̺�Ʈ )
    public void chargeBreath()
    {
        // �극�� ������ Ȱ��ȭ
        chargeEffect.gameObject.SetActive(true);
    }


    // ���� ���� (�ִϸ��̼� �̺�Ʈ)
    public void MiniDragonAttack()
    {
        Debug.Log("Attack");

        // �극�� ������ ����Ʈ ��Ȱ��ȭ
        chargeEffect.gameObject.SetActive(false);

        // ���� ���� Ȱ��ȭ
        attackHitBox.gameObject.SetActive(true);

        // �ٴ� ����Ʈ Ȱ��ȭ
        floorEffect.gameObject.SetActive(true);
    }

    // ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void MiniDragonAttackEnd()
    {
        Debug.Log("AttackEnd");

        isAttacking = false;
        animator.SetBool("Attack", false);
        targetPlayer = null;

        // ���� ���� ��Ȱ��ȭ
        attackHitBox.gameObject.SetActive(false);

        // �ٴ� ����Ʈ ��Ȱ��ȭ
        floorEffect.gameObject.SetActive(false);
    }
}
