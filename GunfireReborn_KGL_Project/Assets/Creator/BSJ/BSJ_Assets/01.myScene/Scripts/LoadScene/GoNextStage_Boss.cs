using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoNextStage_Boss : MonoBehaviourPun
{
    public GameObject[] playerPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("GoToBoss", RpcTarget.All);
        }
        else
        {
            Debug.Log(other.tag);
            return;
        }
    }

    [PunRPC]
    public void GoToBoss()
    {
        // ���� �並 ��� �ִ� �÷��̾ ã�Ƽ� ��ġ ����ȭ
        PhotonView[] allPhotonView = PhotonView.FindObjectsOfType<PhotonView>();
        List<PhotonView> players = new List<PhotonView>();

        // ���� �並 ��� �ִ� �÷��̾ ã�Ƽ� ��ġ ����ȭ
        foreach (PhotonView findPlayer in allPhotonView)
        {
            if (findPlayer.CompareTag("Player"))
            {
                players.Add(findPlayer);
            }
        }

        // ��ġ �ʱ�ȭ 
        // (3, 0, -3, -6) 4���� x ���� ���ʷ� �̰����� �� ����. y = -4, z = 27�� �̸� ��ǥ�� �����صξ���. 
        for (int i = 0; i < players.Count; i++)
        {
            if (i == 0)
            { players[i].transform.position = playerPos[0].transform.position; }
            else if (i == 1)
            { players[i].transform.position = playerPos[1].transform.position; }
            else if (i == 2)
            { players[i].transform.position = playerPos[2].transform.position; }
            else if (i == 3)
            { players[i].transform.position = playerPos[3].transform.position; }
        }
    }
}
