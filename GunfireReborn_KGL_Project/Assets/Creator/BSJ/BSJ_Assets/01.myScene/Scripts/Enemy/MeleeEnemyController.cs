//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;
//using Unity.VisualScripting;
//using UnityEngine.UIElements;
//using UnityEngine.UI;
//using UnityEngine.AI;

//public class MeleeEnemyController : Enemy
//{
//    private void Awake()
//    {
//        enemyType = Type.Melee;

//        animator = GetComponent<Animator>();
//        nav = GetComponent<NavMeshAgent>();

//        isIdle = true;              // ��� ����
//        isTracking = false;         // ���� ����
//        isAttacking = false;        // ���� ����
//    }

//    private void Update()
//    {
//        // �⺻���´� ��� ����
//        if (isTracking == false && isAttacking == false)
//        {
//            isIdle = true;
//            animator.SetBool("Idle", true);
//        }

//        // �÷��̾���� ��ġ�� ���ؼ� ���� �Ÿ������� �ٰ����� ����
//        FindClosestPlayer();

//        // ���� ����� �ִٸ�
//        if (targetPlayer != null)
//        {
//            if (isAttacking == false)
//            {
//                // ���� �÷��̾ �ٶ󺻴�.
//                transform.LookAt(targetPlayer);

//                // ���� �÷��̾ ���ؼ� �̵��Ѵ�.
//                nav.SetDestination(targetPlayer.position);
//            }

//            // ���� ( ���� �÷��̾ ���ݹ��� �ȿ� ������ )
//            if (Vector3.Distance(transform.position, targetPlayer.position) <= attackRange)
//            {
//                StartAttack();
//            }

//            // ��� ( ���� ���� ������ �÷��̾ ������ )
//            else if (Vector3.Distance(transform.position, targetPlayer.position) > trackingRange)
//            {
//                StartIdle();
//            }
//        }
//    }

//}
