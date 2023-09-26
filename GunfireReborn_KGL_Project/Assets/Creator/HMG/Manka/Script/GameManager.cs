using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static GameManager instance
    {
        get 
        {
            //���� ��Ŭ�� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;

        }
    }

    private static GameManager m_instance;

    public GameObject playerPrefab;

    public int nowStage = 0;        // ���� ���������� ������

    private void Awake()
    {
        if(instance != this)
        {
            //�ڽ��� �ı�
            Destroy(gameObject);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo Info)
    {
      
    }
    // Start is called before the first frame update
    void Start()
    {
        // Vector3 randomSpawnPos = Random.insideUnitSphere * 5f;
        //��ġ�� y���� 0���� ����
        // randomSpawnPos.y = 0f;

        //��Ʈ��ũ���� ��� Ŭ���̾�Ʈ���� ��������
        //�ش� ���� ������Ʈ�� �ֵ����� ���� �޼��带 ���� ������ Ŭ���̾�Ʈ�� ����
        PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);

        nowStage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("TestScene");
    }

}
