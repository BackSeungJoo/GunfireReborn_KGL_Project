using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetDropVelocity : MonoBehaviourPun
{
    private Rigidbody rb;
    private float randonPosX;       // ������ �� Ƣ����� �ӵ��� �� x��
    private float randonPosY;       // ������ �� Ƣ����� �ӵ��� �� y��
    private float randonPosZ;       // ������ �� Ƣ����� �ӵ��� �� z��
    private Vector3 newVelocity;    // ������ �� Ƣ����� �ӵ�

    private GameObject[] allPlayers;         // ��� �÷��̾�
    private GameObject myClientPlayer;       // �� Ŭ���̾�Ʈ�� �÷��̾�
    private float moveSpeed = 5f;            // �÷��̾� ������ �ٰ����� �ӵ�
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // �÷��̾� �±� �߿� �� Ŭ���̾�Ʈ�� �÷��̾ ã�� ��ġ ��ȯ
        allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject myPlayer in allPlayers)
        {
            if (myPlayer.GetPhotonView().IsMine == true)
            {
                myClientPlayer = myPlayer;
            }
        }
    }

    private void OnEnable()
    {
        SetVelocity();
        rb.AddForce(newVelocity, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.CompareTag("weapon"))
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void Update()
    {
        // �÷��̾� ������ �̵� ���� ���
        if (myClientPlayer != null && this.gameObject.CompareTag("weapon") == false)
        {
            // �÷��̾� ������ �̵�
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, myClientPlayer.transform.position, step);
        }
    }



    public void SetVelocity()
    {
        randonPosX = Random.Range(0f, 0.1f);
        randonPosY = Random.Range(0f, 0.5f);
        randonPosZ = Random.Range(0f, 0.1f);
        newVelocity = new Vector3(randonPosX, randonPosY, randonPosZ);
    }
}
