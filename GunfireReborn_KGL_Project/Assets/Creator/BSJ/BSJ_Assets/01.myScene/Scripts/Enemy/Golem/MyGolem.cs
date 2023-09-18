using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyGolem : MonoBehaviour
{
    public enum Type { Melee, Range, Boss }; // Ÿ�� ( �ٰŸ�, ���Ÿ�, ����)
    public Type enemyType;                  // �� Ÿ��

    public float trackingSpeed;             // ���� ���ǵ�
    public float trackingRange;             // ���� �Ÿ�
    public float attackRange;               // ���� ���� ����

    // public Transform targetPlayer;          // �÷��̾� ��ġ
    // public PhotonView trackPlayer;           // ���� ������ �÷��̾�
    public GameObject trackPlayer;
    public Animator animator;               // �ִϸ�����

    public bool isIdle;                     // ��� ����
    public bool isTracking;                 // ���� ����
    public bool isAttacking;                // ���� ����

    public NavMeshAgent nav;                // �׺���̼� ���

    public AudioClip attackAudioClip01;     // ����� Ŭ��
    public AudioClip attackAudioClip02;     // ����� Ŭ��
    private AudioSource attackAudioSource;  // ����� �ҽ�

    public GameObject normalAttackEffect01;   // ���� 1 ����Ʈ
    public GameObject normalAttackEffect02;   // ���� 1 ����Ʈ

    private void Awake()
    {
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        attackAudioSource = GetComponent<AudioSource>();
    }

    // ���� ����Ʈ, ȿ����
    public void Attack01_VFX()
    {
        normalAttackEffect01.SetActive(true);
        attackAudioSource.clip = attackAudioClip01;
        attackAudioSource.Play();
    }

    public void Attack02_VFX()
    {
        normalAttackEffect02.SetActive(true);
        attackAudioSource.clip = attackAudioClip02;
        attackAudioSource.Play();
    }

    public void AttackEnd_VFX()
    {
        normalAttackEffect01.SetActive(false);
        normalAttackEffect02.SetActive(false);
    }
}
