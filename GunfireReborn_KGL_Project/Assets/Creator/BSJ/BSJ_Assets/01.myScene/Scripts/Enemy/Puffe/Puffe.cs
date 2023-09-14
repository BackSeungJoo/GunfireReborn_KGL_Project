using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

// ���� Puffe Ŭ����
public class Puffe : Enemy
{
    public int damage;

    public GameObject gatherEnergyVFX;      // ����Ʈ 1
    public GameObject gatherEnergyVFX2;     // ����Ʈ 2
    public GameObject energyShotVFX;        // ����Ʈ 3
    public GameObject magicCircleVFX;       // ����Ʈ 4

    public LineRenderer lineRenderer;       // ���� ������
    public GameObject shotPoint;            // ���� ������ ���� ��ġ
    public Material lineRenMat;             // ���� ������ ���׸���

    private Vector3 startPoint;     // ���� ���� ����
    private Vector3 direction;      // ���� ����
    private Vector3 endPoint;       // ���� �� ����

    private Vector3 targetDirection;

    private void Awake()
    {
        enemyType = Type.Range;

        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        isIdle = true;              // ��� ����
        isTracking = false;         // ���� ����
        isAttacking = false;        // ���� ����

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
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
            if (isAttacking == false)
            {
                // ���� �÷��̾ �ٶ󺻴�.
                // transform.LookAt(targetPlayer);

                // y�� ȸ�� ����
                targetDirection = targetPlayer.position - transform.position;
                targetDirection.y = 0;
                transform.rotation = Quaternion.LookRotation(targetDirection.normalized);

                // ���� �÷��̾ ���ؼ� �̵��Ѵ�.
                nav.SetDestination(targetPlayer.position);
            }

            // ���� ( ���� �÷��̾ ���ݹ��� �ȿ� ������ )
            if (Vector3.Distance(transform.position, targetPlayer.position) <= attackRange)
            {
                // ���� ����
                StartAttack();
            }

            // ��� ( ���� ���� ������ �÷��̾ ������ )
            else if (Vector3.Distance(transform.position, targetPlayer.position) > trackingRange)
            {
                // ��� ����
                StartIdle();
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ
    public void StartEnergyShot()
    {
        // �̹̼� ���� ������� ����
        ChangeEmissionColor(Color.yellow);

        // ���� �÷��̾ �ٶ󺻴�.
        transform.LookAt(targetPlayer);

        // ����Ʈ Ȱ��ȭ
        gatherEnergyVFX.gameObject.SetActive(true);
        gatherEnergyVFX2.gameObject.SetActive(true);
        magicCircleVFX.gameObject.SetActive(true);

        // Line Renderer ��ġ ����
        // ������
        startPoint = shotPoint.transform.position;
        // ����
        direction = (targetPlayer.position - startPoint).normalized;
        // ����
        endPoint = startPoint + direction * 50f;

        // Line Renderer �������� ���� ����
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        // Line Renderer �ѱ�
        lineRenderer.enabled = true;
    }

    public void EnergyShot()
    {
        // �̹̼� ���� �������� ����
        ChangeEmissionColor(Color.red);

        // ����Ʈ
        gatherEnergyVFX.gameObject.SetActive(false);
        gatherEnergyVFX2.gameObject.SetActive(false);

        energyShotVFX.gameObject.SetActive(true);

        // ���� �߻� (���� ���� ������ ~ ���� ���� ����)
        RaycastHit hit;
        if(Physics.Raycast(startPoint, direction, out hit, Vector3.Distance(startPoint, endPoint)))
        {
            Debug.DrawRay(startPoint, direction, Color.white);
            // �浹�� ��ü�� �ִٸ�
            if(hit.collider != null && hit.collider.CompareTag("Player"))
            {
                // Health ��ũ��Ʈ�� �ִ� TakeDamage �޼��� RPC (remote procedure call)
                // hit.transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }

    public void EndEnergyShot()
    {
        // Line Renderer ����
        lineRenderer.enabled = false;

        // ����Ʈ
        energyShotVFX.gameObject.SetActive(false);
        magicCircleVFX.gameObject.SetActive(false);
    }

    // �̹̼� ���� ����
    public void ChangeEmissionColor(Color color)
    {
        // ��Ƽ������ ���̼� ������ �����մϴ�.
        lineRenMat.SetColor("_EmissionColor", color);

        // ��Ƽ������ ���̼� Ȱ��ȭ�� Ȱ��ȭ�ؾ� ���� ������ ǥ�õ˴ϴ�.
        lineRenMat.EnableKeyword("_EMISSION");
    }
}
