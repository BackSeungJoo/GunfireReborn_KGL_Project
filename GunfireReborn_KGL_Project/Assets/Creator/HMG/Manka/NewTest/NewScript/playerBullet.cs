using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class playerBullet : MonoBehaviourPun
{
    public int maxNBullet  = 675;
    public int remainNBullet;

    public int maxBBullet = 160;
    public int remainBBullet;
    private float activeFalseDistance = 2f;     // �ش� �Ÿ���ŭ �����̰��� ��Ȱ��ȭ

    [SerializeField] private Image Normal;
    [SerializeField] private TextMeshProUGUI NormalBulletText;

    [SerializeField] private Image Large;
    [SerializeField] private TextMeshProUGUI LargeBulletText;

    [SerializeField] private Image Special;
    [SerializeField] private TextMeshProUGUI SpecialBulletText;

    //private TMP_Text bulletText;

    // Start is called before the first frame update
    private void Awake()
    {
        remainNBullet = 90;
        remainBBullet = 10;

        Normal.fillAmount = (float)remainNBullet / (float)maxNBullet;
        Large.fillAmount = (float)remainBBullet / (float)maxBBullet;
        Special.fillAmount = 1.0f;

        NormalBulletText.text = remainNBullet + " / " + maxNBullet;
        LargeBulletText.text = remainBBullet + " / " + maxBBullet;
        SpecialBulletText.text = "45 / 45";
    }

    private void Update()
    {
        Normal.fillAmount = (float)remainNBullet / (float)maxNBullet;
        Large.fillAmount = (float)remainBBullet / (float)maxBBullet;

        NormalBulletText.text = remainNBullet + " / " + maxNBullet;
        LargeBulletText.text = remainBBullet + " / " + maxBBullet;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("DropBigBullet"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < activeFalseDistance)
            {
                other.gameObject.SetActive(false);

                remainBBullet +=30;

                if (remainBBullet > maxBBullet)
                {
                    remainBBullet = maxBBullet;
                }

                // �÷��̾� �ʿ��� �ش� �������� �Ծ��� �� �����ϴ� ����
            }
        }


        if(other.CompareTag("DropNormalBullet"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < activeFalseDistance)
            {
                other.gameObject.SetActive(false);

                remainNBullet += 100;

                if(remainNBullet > maxNBullet)
                {
                    remainNBullet = maxNBullet;
                }

                // �÷��̾� �ʿ��� �ش� �������� �Ծ��� �� �����ϴ� ����
            }
        }

    }
}
