using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerGold : MonoBehaviourPun
{

    public int Gold;                // ������
    private float activeFalseDistance = 2f;     // �ش� �Ÿ���ŭ �����̰��� ��Ȱ��ȭ
    private TMP_Text goldText;

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
            }
        }
    }
}
