using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BreathHitBox : MonoBehaviour
{
    public int damage;                  // ������ ������

    // ��Ʈ�ڽ��� ��Ҵٸ�
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾����� Ž�� && �̹� ������ �޾Ҵ��� üũ
        if (other.CompareTag("Player"))
        {
            // �÷��̾�� ������ �ֱ�
            Debug.Log("���ݴ���" + damage);

            // Health ��ũ��Ʈ�� �ִ� TakeDamage �޼��� RPC (remote procedure call)
            // other.transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
            // �÷��̾� ü���� ��� ��0
        }
    }
}
