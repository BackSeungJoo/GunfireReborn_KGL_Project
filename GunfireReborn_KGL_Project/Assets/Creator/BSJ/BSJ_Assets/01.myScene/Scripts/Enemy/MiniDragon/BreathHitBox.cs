using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BreathHitBox : MonoBehaviourPun
{
    // ��ũ Enemy�� ������ ��ũ��Ʈ �����.
    public int damage;  // ������ ������

    // ��Ʈ�ڽ��� ��Ҵٸ�
    //private void OnTriggerEnter(Collider other)
    //{
    //    // �÷��̾����� Ž�� && �̹� ������ �޾Ҵ��� üũ
    //    if (other.CompareTag("Player"))
    //    {
    //        // �÷��̾�� ������ �ֱ�
    //        playerHp player = other.GetComponent<playerHp>();
    //        photonView.RPC("MasterCall_Breath", RpcTarget.MasterClient, player, damage);
    //    }
    //}

    //[PunRPC]
    //public void MasterCall_Breath(playerHp player, int damage)
    //{
    //    player.PlayerTakeDamage(damage);
    //}
}
