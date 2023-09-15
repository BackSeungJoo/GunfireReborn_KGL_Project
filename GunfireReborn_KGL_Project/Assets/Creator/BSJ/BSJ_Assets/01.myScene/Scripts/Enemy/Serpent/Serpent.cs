using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using UnityEngine.AI;

public class Serpent : Enemy
{
    public int damage;

    public GameObject chargeEffect1;        // ���� ����Ʈ 1
    public GameObject chargeEffect2;        // ���� ����Ʈ 2
    public GameObject floorEffect;          // �ٴ� ����Ʈ
    public GameObject energyShotEffect;     // ���� �� ����Ʈ

    public LineRenderer lineRenderer;       // ���� ������
    public GameObject shotPoint;            // ���� ������ ���� ��ġ
    public Material lineRenMat;             // ���� ������ ���׸���
    public float startLineWidth;            // ���� ���� ������ ��
    public float endLineWidth;              // �� ���� ������ ��
    public float lindWidthOffset;           // ���� ���� ���� Ű�� ��.

    private Vector3 startPoint;     // ���� ���� ����
    private Vector3 direction;      // ���� ����
    private Vector3 endPoint;       // ���� �� ����

    private void Awake()
    {
        enemyType = Type.Range;

        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;

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
            if (isAttacking == false)
            {
                // ���� ��� �ٶ󺸱� ( y ���� �������Ѽ� �������̰� �־ �̻��ϰ� ȸ������ ���� )
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
        ChangeEmissionColor(Color.green);

        // ���� �÷��̾ �ٶ󺻴�.
        transform.LookAt(targetPlayer);

        // ����Ʈ
        chargeEffect1.SetActive(true);
        chargeEffect2.SetActive(true);
        floorEffect.SetActive(true);

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
        lineRenderer.startWidth = startLineWidth;
        lineRenderer.endWidth = endLineWidth;
        lineRenderer.enabled = true;
    }

    public void EnergyShot()
    {
        // Line Renderer ũ�� ����
        lineRenderer.startWidth = startLineWidth + lindWidthOffset;
        lineRenderer.endWidth = endLineWidth + lindWidthOffset;

        // �̹̼� ���� �������� ����
        ChangeEmissionColor(Color.red);

        // ����Ʈ
        chargeEffect1.SetActive(false);
        chargeEffect2.SetActive(false);
        floorEffect.SetActive(false);
        energyShotEffect.SetActive(true);

        // ���� �߻� (���� ���� ������ ~ ���� ���� ����)
        RaycastHit hit;
        if (Physics.Raycast(startPoint, direction, out hit, Vector3.Distance(startPoint, endPoint)))
        {
            Debug.DrawRay(startPoint, direction, Color.white);
            // �浹�� ��ü�� �ִٸ�
            if (hit.collider != null && hit.collider.CompareTag("Player"))
            {
                // Health ��ũ��Ʈ�� �ִ� TakeDamage �޼��� RPC (remote procedure call)
                // hit.transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            }
        }
    }

    public void EndEnergyShot()
    {
        // ����Ʈ ����
        energyShotEffect.SetActive(false);

        // Line Renderer ����
        lineRenderer.enabled = false;
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
