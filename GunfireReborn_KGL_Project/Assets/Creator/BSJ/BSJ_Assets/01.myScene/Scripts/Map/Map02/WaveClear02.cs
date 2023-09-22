using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaveClear02 : MonoBehaviourPun
{
    public GameObject NextWave;

    public bool waveClear = false;

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            // �ڽĿ�����Ʈ �� �ϳ��� Ȱ��ȭ�� �Ǿ��ִٸ� Ŭ���� ����
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }
        }

        // ��� �ڽ� ������Ʈ�� ��Ȱ��ȭ ���¶�� Ŭ���� ����
        photonView.RPC("CheckWaveClear", RpcTarget.All, true);

        if (waveClear)
        {
            NextWave.SetActive(true);
        }
    }

    [PunRPC]
    public void CheckWaveClear(bool _waveClear)
    {
        waveClear = _waveClear;
    }
}
