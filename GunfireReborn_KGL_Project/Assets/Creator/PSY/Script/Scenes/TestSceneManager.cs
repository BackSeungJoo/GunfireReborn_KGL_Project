using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TestSceneManager : MonoBehaviourPunCallbacks
{
    private LoadingManager LoadingManager;

    private string gameVersion = "3"; //���ӹ���

    //���� ����� ���ÿ� ������ ���� ���� �õ�
    private void Start()
    {
        LoadingManager = GameObject.Find("@Managers").GetComponent<LoadingManager>();

        //���ӿ� �ʿ��� ���ӹ��� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    //������ ���� ���� ���� �� �ڵ� ����
    public override void OnConnectedToMaster()
    {
        Connect();
    }

    //������ ���� ���� ���н� �ڵ�����
    public override void OnDisconnected(DisconnectCause cause)
    {
        //������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    //�� ���� �õ� 
    public void Connect()
    {
        //�����ͼ����� �������̶��
        if (PhotonNetwork.IsConnected)
        {
            //������ ����
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //������ �������� ������ �õ�
            PhotonNetwork.ConnectUsingSettings();
        }

    }
    //����� ���� ���� �� ������ ������ ��� �ڵ�����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //�ִ� 4���� ���� ������ ��� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    //�뿡 ������ �Ϸ�� ��� �ڵ�����
    public override void OnJoinedRoom()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Map_01_PSY");
        operation.allowSceneActivation = false;

        while (!operation.isDone && operation.allowSceneActivation == false)
        {
            yield return new WaitForSeconds(3f);

            operation.allowSceneActivation = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}