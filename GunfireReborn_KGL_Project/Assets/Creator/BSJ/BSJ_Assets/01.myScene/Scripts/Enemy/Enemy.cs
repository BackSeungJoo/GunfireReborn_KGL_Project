using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using System.Threading;
using Photon.Realtime;

// �θ� Ŭ����
public class Enemy : MonoBehaviourPun //IPunObservable
{
    public enum Type { Melee, Range, Boss}; // Ÿ�� ( �ٰŸ�, ���Ÿ�, ����)
    public Type enemyType;                  // �� Ÿ��

    public float trackingSpeed;             // ���� ���ǵ�
    public float trackingRange;             // ���� �Ÿ�
    public float attackRange;               // ���� ���� ����

    public Transform targetPlayer;          // �÷��̾� ��ġ
    public PhotonView trackPlayer;       // ���� ������ �÷��̾� ( ���� )
    //public GameObject trackPlayer;          // ���� ������ �÷��̾�
    public Animator animator;               // �ִϸ�����

    public bool isIdle;                     // ��� ����
    public bool isTracking;                 // ���� ����
    public bool isAttacking;                // ���� ����

    public NavMeshAgent nav;                // �׺���̼� ���

    protected Vector3 targetDirection;      // ���� ����

    // �����
    public AudioClip soundEffect;
    public AudioSource audioSource;

    // �⺻ ���� ����Ʈ
    public GameObject normalAttackEffect;
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (PhotonNetwork.IsMasterClient)
    //    {
    //        stream.SendNext(isIdle);
    //        stream.SendNext(isTracking);
    //        stream.SendNext(isAttacking);
    //    }
    //    else
    //    {
    //        isIdle = (bool)stream.ReceiveNext();
    //        isTracking = (bool)stream.ReceiveNext();
    //        isAttacking = (bool)stream.ReceiveNext();
    //    }
    //}

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundEffect;
    }

    // ���� ����� �÷��̾� ã��
    public void FindClosestPlayer()
    {
        // ����� ��ũ��Ʈ�� ���� �ִ� ��� Ž��
        // PhotonView[] players = GameObject.FindObjectsOfType<PhotonView>();
        // �׽�Ʈ ��
        PhotonView[] players = PhotonView.FindObjectsOfType<PhotonView>();
        List<PhotonView> playerWithTag = new List<PhotonView>();

        foreach (PhotonView player in players)
        {
            if (player.gameObject.CompareTag("Player"))
            {
                playerWithTag.Add(player);
            }
        }

        // ���� ó�� �Ÿ��� ���Ѵ�� ����
        float closestDistance = Mathf.Infinity;

        // photonview�� ���� �ִ� ��� ��� �߿� Player �±׸� ���� ��� Ž��
        // foreach (PhotonView player in players)
        // �׽�Ʈ
        // foreach (GameObject player in players)
        foreach (PhotonView player in playerWithTag)
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
                    photonView.RPC("SetTrackPlayer", RpcTarget.Others, trackPlayer.ViewID);
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

            // photonView.RPC("AnimSetBoolTrack", RpcTarget.All, true);
            // photonView.RPC("AnimSetBoolIdle", RpcTarget.All, false);

            animator.SetBool("Track", true);
            animator.SetBool("Idle", false);

            targetPlayer = trackPlayer.transform;
            photonView.RPC("SetTargetPlayer", RpcTarget.Others);
            // Debug.Log(closestDistance);
        }
    }

    // ���� ����
    public void StartAttack()
    {
        // ������ ���·� ���ݸ�Ǹ� ����
        nav.ResetPath();

        nav.isStopped = true;
        nav.velocity = Vector3.zero;

        isAttacking = true;
        isTracking = false;

        // photonView.RPC("AnimSetBoolAttack", RpcTarget.All, true);
        // photonView.RPC("AnimSetBoolTrack", RpcTarget.All, false);
        animator.SetBool("Attack", true);
        animator.SetBool("Track", false);
    }

    // �����ϴ� ���� ( �ִϸ��̼� �̺�Ʈ )
    public void Attack()
    {
        // Debug.Log("Attack");

        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if(enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // ���� �� ( �ִϸ��̼� �̺�Ʈ )
    public void AttackEnd()
    {
        // Debug.Log("AttackEnd");
        // ���� (Ʈ���Ÿ� ���� �ڽĿ�����Ʈ Ȱ��ȭ�ؼ� �÷��̾�� �������� �ִ� ���)
        if(enemyType == Type.Melee)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
        isAttacking = false;
        //photonView.RPC("AnimSetBoolAttack", RpcTarget.AllBuffered, animator, false);
        animator.SetBool("Attack", false);

        targetPlayer = null;

        if(normalAttackEffect != null)
        {
            normalAttackEffect.SetActive(false);
        }
    }



    // ��� ����
    public void StartIdle()
    {
        nav.isStopped = true;
        nav.velocity = Vector3.zero;

        isTracking = false;
        //photonView.RPC("AnimSetBoolTrack", RpcTarget.All, false);
        animator.SetBool("Track", false);

        targetPlayer = null;
    }

    // ���� ����Ʈ, ȿ����
    public void Attack_VFX()
    {
        if(normalAttackEffect != null)
        {
            normalAttackEffect.SetActive(true);
        }

        audioSource.Play();
    }

    [PunRPC]
    public void AnimSetBoolIdle(bool state)
    {
        animator.SetBool("Idle", state);
    }

    [PunRPC]
    public void AnimSetBoolTrack(bool state)
    {
        animator.SetBool("Track", state);
    }

    [PunRPC]
    public void AnimSetBoolAttack(bool state)
    {
        animator.SetBool("Attack", state);
    }

    [PunRPC]
    public void SetTrackPlayer(int viewID)
    {
        PhotonView photonView = PhotonView.Find(viewID);
        if (photonView != null)
        {
            trackPlayer = photonView;
        }
        else
        {
            Debug.LogError("PhotonView with View ID " + viewID + " not found.");
        }
    }

    [PunRPC]
    public void SetTargetPlayer()
    {
        if(trackPlayer != null)
        {
            targetPlayer = trackPlayer.transform;
        }
    }
}
