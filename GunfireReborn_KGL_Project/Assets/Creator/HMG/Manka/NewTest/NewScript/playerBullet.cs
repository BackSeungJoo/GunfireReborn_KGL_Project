using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class playerBullet : MonoBehaviourPun
{
    public int bullet  = 675 ;
    public int remainBullet = 30;
    
    public int sBullet = 45;
    public int remainSBullet = 4;   
    
    public int lBullet = 160;       
    public int remainlBullet = 10;  

    private int Gold;                //������\
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
        if (Vector3.Distance(transform.position, other.transform.position) < activeFalseDistance)
        {
            Debug.Log("�������ִ°ž�?");
            other.gameObject.SetActive(false);
            if (other.tag == "Coin")
            {
                Gold += 5;
            }
            // �÷��̾� �ʿ��� �ش� �������� �Ծ��� �� �����ϴ� ����
        }

    }
}
