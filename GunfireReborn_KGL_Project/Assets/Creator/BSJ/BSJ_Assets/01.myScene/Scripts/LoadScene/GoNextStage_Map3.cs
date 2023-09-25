using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextStage_Map3 : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("LoadNextScene_Map3", RpcTarget.All);
        }
        else
        {
            Debug.Log(other.tag);
            return;
        }
    }

    [PunRPC]
    public void LoadNextScene_Map3()
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
            { players[i].transform.position = new Vector3(-30, 6, -32); }
            else if (i == 1)
            { players[i].transform.position = new Vector3(-32, 6, -32); }
            else if (i == 2)
            { players[i].transform.position = new Vector3(-34, 6, -32); }
            else if (i == 3)
            { players[i].transform.position = new Vector3(-36, 6, -32); }
        }

        SceneManager.LoadScene("Map_03_BSJ");
    }
}
