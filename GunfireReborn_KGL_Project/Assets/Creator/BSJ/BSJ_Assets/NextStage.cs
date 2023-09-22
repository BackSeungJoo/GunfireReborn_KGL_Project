using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class NextStage : MonoBehaviourPun
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //GameObject player = other.gameObject;
            //player.transform.position = new Vector3(0, 10, 0);

            photonView.RPC("LoadNextScene", RpcTarget.All);
        }
        else
        {
            Debug.Log(other.tag);
            return;
        }
    }

    [PunRPC]
    public void LoadNextScene()
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
        for(int i = 0; i < players.Count; i++)
        {
            if(i == 0)
            { players[i].transform.position = new Vector3(3, -4, 27); }
            else if(i == 1)
            { players[i].transform.position = new Vector3(0, -4, 27); }
            else if(i == 2)
            { players[i].transform.position = new Vector3(-3, -4, 27); }
            else if(i == 3)
            { players[i].transform.position = new Vector3(-6, -4, 27); }
        }

        SceneManager.LoadScene("Map_02_BSJ");
    }
}
