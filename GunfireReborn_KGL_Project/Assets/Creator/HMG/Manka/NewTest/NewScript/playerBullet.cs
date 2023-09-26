using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class playerBullet : MonoBehaviourPun
{
    public int nBullet  = 675 ;
    public int remainNBullet = 30;

    public int bBullet = 160;       
    public int remainBBullet = 10;  

    private int Gold;                // ������
    private float activeFalseDistance = 2f;     // �ش� �Ÿ���ŭ �����̰��� ��Ȱ��ȭ
    private TMP_Text goldText;

    //private TMP_Text bulletText;

    // Start is called before the first frame update
    void Start()
    {
        goldText = GameObject.Find("CoinText").GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
            goldText.text = Gold.ToString();

    }

    private void OnTriggerStay(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }

        // �����Ÿ� �̻� ���� ��Ȱ��ȭ
        if (other.CompareTag("Coin"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < activeFalseDistance)
            {
                other.gameObject.SetActive(false);
                Gold += 5;
                // �÷��̾� �ʿ��� �ش� �������� �Ծ��� �� �����ϴ� ����
            }
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
