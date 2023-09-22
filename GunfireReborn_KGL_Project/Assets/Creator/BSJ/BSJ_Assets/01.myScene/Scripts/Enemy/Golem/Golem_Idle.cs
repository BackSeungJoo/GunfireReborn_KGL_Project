using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Golem_Idle : StateMachineBehaviour
{
    float trackRange;
    float nearestTargetDistance;

    GameObject[] findAllPlayers;
    GameObject targetPlayer;
    MyGolem enemyScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        targetPlayer = null;
        enemyScript = animator.GetComponent<MyGolem>();
        trackRange = enemyScript.trackingRange;

        findAllPlayers = GameObject.FindGameObjectsWithTag("Player");
        nearestTargetDistance = Mathf.Infinity;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        // �÷��̾� ��ġ ����
        foreach (GameObject player in findAllPlayers)
        {
            float distanceToTarget = Vector3.Distance(player.transform.position, enemyScript.transform.position);

            if (distanceToTarget < nearestTargetDistance)
            {
                nearestTargetDistance = distanceToTarget;

                if (nearestTargetDistance < trackRange)
                {
                    targetPlayer = player;
                }
            }
        }

        if(targetPlayer != null)
        {
            enemyScript.trackPlayer = targetPlayer;

            // Debug.Log("Ÿ�� �߰�, ���� ���� ...");
            animator.SetBool("Idle", false);
            animator.SetBool("Track", true);
        }
    }
}
