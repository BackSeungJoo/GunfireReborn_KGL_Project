using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance
    {
        get
        {
            //���� ��Ŭ�� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;

        }
    }

    private static GameManager m_instance;

    public int nowStage = 0;        // ���� ���������� ������
    public BlackSmithUI blackSmithUI; // ��ȭ Ƚ�� �ʱ�ȭ

    private void Awake()
    {
        if (instance != this)
        {
            //�ڽ��� �ı�
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // �������� �ε��� ����
        nowStage = 1;
    }
}