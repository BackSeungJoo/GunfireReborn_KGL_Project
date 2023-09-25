using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager_SSC : MonoBehaviourPunCallbacks
{
    private string gameVersion = "3"; //���ӹ���

    public Text connectionInfoText; //��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton; //�� ���� ��ư
    // Start is called before the first frame update

    //���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
        //���ӿ� �ʿ��� ���ӹ��� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        //�� ���� ��ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;
        //���� �õ� ������ �ؽ�Ʈ�� ǥ��
        connectionInfoText.text = "������ ������ ������ ...";
    }

    //������ ���� ���� ���� �� �ڵ� ����
    public override void OnConnectedToMaster()
    {
        //�� ���� ��ư Ȱ��ȭ
        joinButton.interactable = true;
        //���� ���� ǥ��
        connectionInfoText.text = "�¶��� : ������ ������ �����";
    }

    //������ ���� ���� ���н� �ڵ�����
    public override void OnDisconnected(DisconnectCause cause)
    {
        //�� ���� ��ư ��Ȱ��ȭ
        joinButton.interactable = false;
        //���� ���� ǥ��
        connectionInfoText.text = "�������� : ������ ������ ������� ����\n ���� ��õ� ��...";
        //������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    //�� ���� �õ� 
    public void Connect()
    {
        //�ߺ� ���� �õ��� ���� ���� ���ӹ�ư ��� ��Ȱ��ȭ
        joinButton.interactable = false;

        //�����ͼ����� �������̶��
        if(PhotonNetwork.IsConnected)
        {
            //������ ����
            connectionInfoText.text = "�뿡 ����...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //������ ������ ���� ���� �ƴ϶�� ������ ������ ���� �õ�
            connectionInfoText.text = "�������� : ������ ������ �����������\n ���� ��õ���...";
            //������ �������� ������ �õ�
            PhotonNetwork.ConnectUsingSettings();
        }
     
    }
    //����� ���� ���� �� ������ ������ ��� �ڵ�����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //���� ���� ǥ��
        connectionInfoText.text = "����� ����, ���ο� �� ����...";
        //�ִ� 4���� ���� ������ ��� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    //�뿡 ������ �Ϸ�� ��� �ڵ�����
    public override void OnJoinedRoom()
    {
        //���� ���� ǥ��
        connectionInfoText.text = "�� ���� ����";
        //��� �� �����ڰ� Main���� �ε��ϰ� ��
        PhotonNetwork.LoadLevel("Map_01_BSJ");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
