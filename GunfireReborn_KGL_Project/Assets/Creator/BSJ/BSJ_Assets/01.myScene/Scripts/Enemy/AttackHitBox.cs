using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class AttackHitBox : MonoBehaviourPun
{
    public int damage;                  // ������ ������
    private bool isAttcked = false;     // ���������� �������� �ִ� ���� �����ϱ� ���� ����

    // ��Ʈ�ڽ��� ��Ҵٸ�
    //private void OnTriggerEnter(Collider other)
    //{
    //    // �÷��̾����� Ž�� && �̹� ������ �޾Ҵ��� üũ
    //    if(other.CompareTag("Player") && isAttcked == false)
    //    {
    //        // �÷��̾�� ������ �ֱ�
    //        playerHp player = other.GetComponent<playerHp>();
    //        photonView.RPC("MasterCall", RpcTarget.MasterClient, player, damage);

    //        // : Todo

    //        // ������ ������ üũ (�������� �ߺ����� ���� �� ����)
    //        isAttcked = true;
    //    }
    //}

    //[PunRPC]
    //public void MasterCall(playerHp player, int damage)
    //{
    //    player.PlayerTakeDamage(damage);
    //}

    // ��Ʈ�ڽ��� ����� �� isAttcked �ʱ�ȭ
    private void OnDisable()
    {
        isAttcked = false;
    }
}
