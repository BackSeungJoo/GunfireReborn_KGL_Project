using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFloorHitBox : MonoBehaviourPun
{
    public int damage;                  // ������ ������
    private bool isAttcked = false;     // ���������� �������� �ִ� ���� �����ϱ� ���� ����

    // ��Ʈ�ڽ��� ��Ҵٸ�
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾����� Ž�� && �̹� ������ �޾Ҵ��� üũ
        if (other.CompareTag("Player") && isAttcked == false)
        {
            // �÷��̾�� ������ �ֱ�
            playerHp player = other.GetComponent<playerHp>();
            photonView.RPC("MasterCall", RpcTarget.MasterClient, player, damage);

            // ������ ������ üũ (�������� �ߺ����� ���� �� ����)
            isAttcked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isAttcked = false;
    }

    [PunRPC]
    public void MasterCall(playerHp player, int damage)
    {
        player.PlayerTakeDamage(damage);
    }

    // ��Ʈ�ڽ��� ����� �� isAttcked �ʱ�ȭ
    private void OnDisable()
    {
        isAttcked = false;
    }
}
