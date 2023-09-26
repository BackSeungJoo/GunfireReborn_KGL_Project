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

    private LoadingManager loadingManager;

    private void Start()
    {
        #region Photon : ���� ����� ���ÿ� ������ ���� ���� �õ�
        //���ӿ� �ʿ��� ���ӹ��� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
        #endregion

        loadingManager = GameObject.Find("@Managers").GetComponent<LoadingManager>();
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
        StartCoroutine(loadingManager.LoadSceneMap("Main_Map_01"));
    }
    #endregion

   
}