using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;

public class Boom : MonoBehaviourPun
{
    public Transform target_Tran;       // Ÿ�� ��ġ
    public float initialAngle = 30f;    // ó�� ���󰡴� ����

    private EvilMage evilMage;          // �θ� Ŭ����
    private Rigidbody rb;               // Rigidbody

    private void Awake()
    {
        evilMage = GetComponentInParent<EvilMage>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        // ���ư� Ÿ�� �÷��̾��� transform
        target_Tran = evilMage.targetPlayer.transform;

        // ������ �
        Vector3 velocity = GetVelocity(transform.position, target_Tran.position, initialAngle);      
        rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        photonView.RPC("InActiveBoom", RpcTarget.All);
    }

    public Vector3 GetVelocity(Vector3 startPos, Vector3 target, float initialAngle)
    {
        // Unity ���� ������Ʈ�� �������� �߷��� ũ�⸦ ��Ÿ���� ��. 9.81
        float gravity = Physics.gravity.magnitude;

        // ó�� ���ư��� ������ �������� ����
        float angle = initialAngle * Mathf.Deg2Rad;

        // Ÿ�� ��ġ
        Vector3 targetPos = new Vector3(target.x, 0, target.z);

        // ó�� �߻� ��ġ
        Vector3 shotPos = new Vector3(startPos.x, 0, startPos.z);

        // �Ÿ� ���ϱ�
        float distance = Vector3.Distance(targetPos, shotPos);

        // ���� ���� ��� (�ʱ� �ӵ� ��� �� �߷��� ������ �ݿ��ϱ� ����)
        float yOffset = startPos.y - target.y;

        // { �߻�ü�� �ʱ� �ӵ��� ����ϴ� �ֿ� ����
        // �Ÿ��� ������ ��� : Mathf.Pow(distance, 2)
        // �Ÿ� ������ �߷��� ������ ���Ѵ�. �� ���� �߻� ������ ���� ź��Ʈ �� ���� ���� yOffset���� ����
        // �߻�ü�� �߻� ������ ���� �ڻ��� : (1 / Mathf.Cos(angle))
        // Mathf.Cos(angle) : �� ������ ���Ѵ�. �� �κ��� �߻� ������ ������ ����
        // Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
        
        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
        
        // } �߻�ü�� �ʱ� �ӵ��� ����ϴ� �ֿ� ����


        // �ʱ� �ӵ��� ����Ͽ� 3D �������� �߻�ü�� �ӵ� ���� ���
        // y ������ �ʱ�ӵ��� �߻簢��(����)�� ���� ������ �����Ͽ� �߻�ü�� �������� �ö󰬴ٰ� �ٽ� �Ʒ��� �������� � ��� ��Ÿ��
        // z ������ �ʱ�ӵ��� �߻簢��(����)�� �ڻ��� ������ �����Ͽ� �߻�ü�� ���� �������� �̵��ϴµ� ����.
        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        // Vector3.Angle(Vector3.forward, planarTarget - planarPosition)
        // �� �κ��� ���� ��ġ���� ��ǥ ��ġ������ ���Ϳ� ���� ���� ���� ������ ���
        // (target.x > player.x ? 1 : -1)
        // �� �κ��� ��ǥ ��ġ�� ���� ��ġ�� x ��ǥ ���� ���Ͽ� ��ǥ�� ���� ��ġ�� �����ʿ� �ִ��� ���ʿ� �ִ��� �Ǵ�
        // �׿� ���� ������ ��� �Ǵ� ������ ������. �� ���� ȸ�� ������ �����ϴµ� ���
        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, targetPos - shotPos) * (target.x > startPos.x ? 1 : -1);


        // �ʱ� �ӵ� ���͸� angleBetweenObjects������ŭ Vector3.up �� ������ ȸ����Ű�� ���� ��Ÿ��
        // Quaternion.AngleAxis(angleBetweenObjects, Vector3.up)
        // �� �κ��� angleBetweenObjects ������ �������� Vector3.up �� ������ ȸ���ϴ� ���ʹϾ��� ����
        // angleBetweenObjects�� �ռ� ���� ���� ��ġ���� ��ǥ ��ġ ������ ������ ��Ÿ����, Vector3.up�� y���� �������� ȸ���� ��Ÿ��
        // * velocity �ʱ�ӵ����͸� velocity�� �����Ͽ�, �ʱ� �ӵ� ���͸� �ش� ���� angleBetweenObjects ��ŭ ȸ��
        // �̷��� ȸ�� ��Ų ���Ͱ� �����ӵ��� �ȴ�.
        // ��� ������ finalVelocity������ �ʱ� �ӵ��� ���� ��ġ���� ��ǥ ��ġ������ ����angleBetweenObjects�� ����Ͽ�
        // ȸ���� �ӵ� ���Ͱ� ����ȴ�.
        // �� �ӵ� ���͸� ����Ͽ� �߻�ü�� ���� �����ϵ��� �����ȴ�.

        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    }

    [PunRPC]
    public void InActiveBoom()
    {
        // ����Ʈ Ȱ��ȭ
        evilMage.Active_BombEffect(gameObject.transform.position);

        // �θ� ������Ʈ ������ �ٽ� ���´�
        gameObject.transform.SetParent(evilMage.boomPos);

        // ���ӿ�����Ʈ(źȯ) ��Ȱ��ȭ
        this.gameObject.SetActive(false);
    }
}
