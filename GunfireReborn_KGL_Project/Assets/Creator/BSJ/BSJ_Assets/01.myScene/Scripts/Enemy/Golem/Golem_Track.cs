using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Golem_Track : StateMachineBehaviour
{
    MyGolem enemy;
    NavMeshAgent agent;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.GetComponent<MyGolem>();
        agent = animator.GetComponent<NavMeshAgent>();

        agent.isStopped = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (enemy.trackPlayer == null)
        {
            animator.SetBool("Idle", true);
            animator.SetBool("Track", false);
        }
        // Ÿ�� �÷��̾� ����
        agent.SetDestination(enemy.trackPlayer.transform.position);

        // Ÿ�� �÷��̾���� �Ÿ� ���
        float distanceToTarget = Vector3.Distance(enemy.trackPlayer.transform.position, enemy.transform.position);

        // ���� �Ÿ� �̻� �Ѿ�� ������ ���� ����
        if (distanceToTarget > enemy.trackingRange)
        {
            // Debug.Log("���� ����, ���");
            NavAgentStop(agent);
            animator.SetBool("Idle", true);
            animator.SetBool("Track", false);
        }

        // ���� �� ���� ���� �ȿ� ������ ����
        if (distanceToTarget < enemy.attackRange)
        {
            int randomAttackPattern = Random.Range(0, 2);

            if(randomAttackPattern == 0)
            {
                animator.SetTrigger("T_Attack01");
            }

            else if (randomAttackPattern == 1)
            {
                animator.SetTrigger("T_Attack02");
            }
        }
    }

    void NavAgentStop(NavMeshAgent _agent)
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }
}
