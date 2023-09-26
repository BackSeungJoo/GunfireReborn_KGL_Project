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
    private string gameVersion = "3"; //���ӹ���

    private void Start()
    {
        #region Photon : ���� ����� ���ÿ� ������ ���� ���� �õ�
        //���ӿ� �ʿ��� ���ӹ��� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
        #endregion
    }

    #region Photon
    /// <summary>
    /// ������ ���� ���� ���� �� �ڵ� ����
    /// </summary>
    public override void OnConnectedToMaster()
    {
        Connect();
    }

    /// <summary>
    /// ������ ���� ���� ���н� �ڵ�����
    /// </summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        //������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

    /// <summary>
    ///  �� ���� �õ� 
    /// </summary>
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

    /// <summary>
    /// ����� ���� ���� �� ������ ������ ��� �ڵ�����
    /// </summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //�ִ� 4���� ���� ������ ��� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    /// <summary>
    /// �뿡 ������ �Ϸ�� ��� �ڵ�����
    /// </summary>
    public override void OnJoinedRoom()
    {
        StartCoroutine(LoadScene());
    }
    #endregion

    #region �񵿱� �ε�
    /// <summary>
    /// �񵿱� �ε� �Լ�
    /// </summary>
    private IEnumerator LoadScene()
    {
        // "Map_01_PSY" ���� �񵿱� �۾����� �ε��Ѵ�.
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main_Map_01");  
        operation.allowSceneActivation = false;  // ���� �ε��ϴµ� �غ� �ȵ�.

        while (!operation.isDone && operation.allowSceneActivation == false)  // ���� �ε尡 ���� ������ �ݺ�
        {
            yield return new WaitForSeconds(3f);    // ���� �ð� 3�� �����̸� �ش�.

            operation.allowSceneActivation = true;  // �� �ε��� �غ� ������.
        }
    }
    #endregion
}