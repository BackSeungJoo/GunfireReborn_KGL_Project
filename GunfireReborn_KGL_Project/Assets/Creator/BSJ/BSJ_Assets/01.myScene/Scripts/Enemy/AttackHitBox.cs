//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun;

//public class AttackHitBox : MonoBehaviour
//{
//    public int damage;                  // ������ ������
//    private bool isAttcked = false;     // ���������� �������� �ִ� ���� �����ϱ� ���� ����

//    // ��Ʈ�ڽ��� ��Ҵٸ�
//    private void OnTriggerEnter(Collider other)
//    {
//        // �÷��̾����� Ž�� && �̹� ������ �޾Ҵ��� üũ
//        if(other.CompareTag("Player") && isAttcked == false)
//        {
//            // �÷��̾�� ������ �ֱ�
//            Debug.Log("���ݴ���" + damage);
            
//            // Health ��ũ��Ʈ�� �ִ� TakeDamage �޼��� RPC (remote procedure call)
//            other.transform.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);

//            // ������ ������ üũ (�������� �ߺ����� ���� �� ����)
//            isAttcked = true;
//        }
//    }

//    // ��Ʈ�ڽ��� ����� �� isAttcked �ʱ�ȭ
//    private void OnDisable()
//    {
//        isAttcked = false;
//    }
//}
