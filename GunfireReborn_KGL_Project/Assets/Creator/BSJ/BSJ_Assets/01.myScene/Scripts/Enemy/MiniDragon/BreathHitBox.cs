using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BreathHitBox : MonoBehaviourPun
{
    // ��ũ Enemy�� ������ ��ũ��Ʈ �����.
    public int damage;  // ������ ������
    private bool isAttcked = false;     // ���������� �������� �ִ� ���� �����ϱ� ���� ����

    // ��Ʈ�ڽ��� ��Ҵٸ�
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾����� Ž�� && �̹� ������ �޾Ҵ��� üũ
        if (other.CompareTag("Player") && isAttcked == false)
        {
            // �÷��̾�� ������ �ֱ�
            playerHp player = other.GetComponent<playerHp>();
            //player.photonView.RPC("PlayerTakeDamage", RpcTarget.MasterClient, damage);
            player.PlayerTakeDamage(damage);

            // ������ ������ üũ (�������� �ߺ����� ���� �� ����)
            isAttcked = true;
        }
    }


    // ��Ʈ�ڽ��� ����� �� isAttcked �ʱ�ȭ
    private void OnDisable()
    {
        isAttcked = false;
    }
}
