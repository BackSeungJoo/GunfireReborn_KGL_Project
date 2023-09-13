using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    // ������ �� ������
    public GameObject[] enemyPrefab; // �켱 �θ����� üũ�� �� ����.
    // ���� ����
    public float spawnTimer = 5f;
    private float _timer = 0f;
    // ���� ��ġ
    public Transform[] spawnPoints;

    // ���� ���� ������ ����
    private GameObject spawnEnemy;

    private void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > spawnTimer )
        {
            if(PhotonNetwork.IsMasterClient)
            {
                int ranEnemy = Random.Range(0, 5); // �ϴ� �θ����� üũ
                string spawnEnemyName = (ranEnemy == 0) ? enemyPrefab[0].name : enemyPrefab[1].name;

                // PhotonView ������Ʈ ��������
                PhotonView photonView = GetComponent<PhotonView>();

                // �� ���� ����ȭ
                photonView.RPC("SpawnEnemy", RpcTarget.All, spawnEnemyName);
            }

            _timer = 0f;
        }
    }

    [PunRPC]
    private void SpawnEnemy(string enemyName)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            // �� ����
            GameObject enemy = PhotonNetwork.Instantiate(enemyName, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        }
    }
}
