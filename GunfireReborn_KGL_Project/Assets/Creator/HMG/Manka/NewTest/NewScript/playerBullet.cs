using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class playerBullet : MonoBehaviourPun
{
    public int maxNBullet  = 675 ;
    public int remainNBullet;

    public int maxBBullet = 160;       
    public int remainBBullet = 10;
    private float activeFalseDistance = 2f;     // �ش� �Ÿ���ŭ �����̰��� ��Ȱ��ȭ

    //private TMP_Text bulletText;

    // Start is called before the first frame update
    void Start()
    {
        remainBBullet = 90;
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        
        if (other.CompareTag("DropBigBullet"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < activeFalseDistance)
            {
                other.gameObject.SetActive(false);
                remainBBullet +=30;
                // �÷��̾� �ʿ��� �ش� �������� �Ծ��� �� �����ϴ� ����
            }
        }


        if(other.CompareTag("DropNormalBullet"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < activeFalseDistance)
            {
                other.gameObject.SetActive(false);
                remainNBullet += 100;
                // �÷��̾� �ʿ��� �ش� �������� �Ծ��� �� �����ϴ� ����
            }
        }

    }
}
