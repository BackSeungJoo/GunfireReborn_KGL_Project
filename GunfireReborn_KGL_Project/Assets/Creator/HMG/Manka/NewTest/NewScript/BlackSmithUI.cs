using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class BlackSmithUI : MonoBehaviour
{
    private GameObject player;                      // �÷��̾�
    private WeaponManager1 weapon;                  // �����Ŵ���

    public TextMeshProUGUI weaponName1;             // ����1�� �̸�
    public TextMeshProUGUI weaponName2;             // ����2�� �̸�
    
    public TextMeshProUGUI weaponDamage1;           // ����1�� ������
    public TextMeshProUGUI weaponDamage2;           // ����2�� ������
   
    public TextMeshProUGUI weaponAmmoCount1;        // ����1�� źâ
    public TextMeshProUGUI weaponAmmoCount2;        // ����2�� źâ

    public TextMeshProUGUI Gold;                    // ���

    public Image weaponImage1;                      // 1���ԺҲɺ�ÿ����̹���
    public Image weaponImage2;                      // 1�������������̹���

    public Image weaponImage3;                      // 2���ԺҲɺ�ÿ����̹���
    public Image weaponImage4;                      // 2�������������̹���

    public Image bulletImage1;                      // 1���� Ư��ź�̹���
    public Image bulletImage2;                      // 1���� ����ź�̹���

    public Image bulletImage3;                      // 2���� Ư��ź�̹���
    public Image bulletImage4;                      // 2���� ����ź�̹���

    public TextMeshProUGUI bulletText1;             // 1���� �Ѿ� Text
    public TextMeshProUGUI bulletText2;             // 2���� �Ѿ� Text

    string slot1Weapon;                             // �÷��̾��� weapon1�̸��� ���� ����
    string slot2Weapon;                             // �÷��̾��� weapon2�̸��� ���� ����

    public TextMeshProUGUI weaponInfo1;             // ����1��������
    public TextMeshProUGUI weaponInfo2;             // ����2��������

    public TextMeshProUGUI inforceCountText;        // ��ȭȽ���ؽ�Ʈ
    private int inforceCount = 3;                   // ��ȭȽ��

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CinemachineVirtualCamera>().transform.parent.gameObject;
        weapon = player.GetComponent<WeaponManager1>();
        inforceCountText.text = "�ܿ� ��ȭ Ƚ�� : 3";

    }

    private void OnEnable()
    {
        UpdateWeapon();
        UpdateText();
    }
    // Update is called once per frame
    void Update()
    {
        Gold.text = (player.GetComponent<PlayerGold>().Gold).ToString();
        if (slot1Weapon == "CrimsonFirescale")
        {
            weaponDamage1.text = "���� ����� "  + (UpgradeManager.up_Instance.rifleDamage).ToString();
        }
        else if (slot1Weapon == "Shotgun")
        {
            weaponDamage1.text = "���� ����� " + (UpgradeManager.up_Instance.shotgunDamage).ToString();
        }

        if (slot2Weapon == "CrimsonFirescale")
        {
            weaponDamage2.text = "���� ����� " + (UpgradeManager.up_Instance.rifleDamage).ToString();
        }
        else if (slot2Weapon == "Shotgun")
        {
            weaponDamage2.text = "���� ����� " + (UpgradeManager.up_Instance.shotgunDamage).ToString();
        }
    }

    #region ������Ʈ ����
    private void UpdateWeapon()
    {
        if (weapon.slotWeapons[0] != null)
        {
            slot1Weapon = weapon.slotWeapons[0];
        }
        if (weapon.slotWeapons[1] != null)
        {
            slot2Weapon = weapon.slotWeapons[1];
        }
    }
    #endregion
    private void UpdateText()
    {
        //����ؽ�Ʈ ������Ʈ
        Gold.text = (player.GetComponent<PlayerGold>().Gold).ToString();
        //����1���������̸�, �ؽ�Ʈ ������Ʈ
        if (slot1Weapon == "CrimsonFirescale")
        {
            weaponName1.text = "�Ҳ� ���";
            weaponAmmoCount1.text = "źâ�뷮 30";
            bulletText1.text = "Ư��ź";
            weaponInfo1.text = "�� �������Դϴ�.";
            weaponDamage1.text = "���� ����� " + (UpgradeManager.up_Instance.rifleDamage).ToString();
        }
        else if (slot1Weapon == "Shotgun")
        {
            weaponName1.text = "�� ��";
            weaponAmmoCount1.text = "źâ�뷮 8";
            bulletText1.text = "����ź";
            weaponInfo1.text = "�� �����Դϴ�.";
            weaponDamage1.text = "���� ����� " + (UpgradeManager.up_Instance.shotgunDamage).ToString();

        }
        //����2���������̸��ؽ�Ʈ ������Ʈ
        if (slot2Weapon == "CrimsonFirescale")
        {
            weaponName2.text = "�Ҳ� ���";
            weaponAmmoCount2.text = "źâ�뷮 30";
            bulletText2.text = "Ư��ź";
            weaponInfo2.text = "�� �������Դϴ�.";
            weaponDamage2.text = "���� �����  " + (UpgradeManager.up_Instance.rifleDamage).ToString();
        }
        else if (slot2Weapon == "Shotgun")
        {
            weaponName2.text = "�� ��";
            weaponAmmoCount2.text = "źâ�뷮 8";
            bulletText2.text = "����ź";
            weaponInfo2.text = "�� �����Դϴ�.";
            weaponDamage2.text = "���� ����� " + (UpgradeManager.up_Instance.shotgunDamage).ToString();
        }
    }

    public void UpGrade1()
    {
        if (inforceCount >= 1 && player.GetComponent<PlayerGold>().Gold >= 100)
        {
            if (slot1Weapon == "CrimsonFirescale")
            {
                UpgradeManager.up_Instance.RifleUp();
                inforceCount -= 1;
                inforceCountText.text = "�ܿ� ��ȭ Ƚ�� : " + inforceCount.ToString();
                player.GetComponent<PlayerGold>().Gold -= 100;
            }
            else if (slot1Weapon == "Shotgun")
            {
                UpgradeManager.up_Instance.ShotgunUp();
                inforceCount -= 1;
                inforceCountText.text = "�ܿ� ��ȭ Ƚ�� : " + inforceCount.ToString();
                player.GetComponent<PlayerGold>().Gold -= 100;
            }
        }
    }

    public void UpGrade2()
    {
        if (inforceCount >= 1 && player.GetComponent<PlayerGold>().Gold>=100)
        {
            if (slot2Weapon == "CrimsonFirescale")
            {
                UpgradeManager.up_Instance.RifleUp();
                inforceCount -= 1;
                inforceCountText.text = "�ܿ� ��ȭ Ƚ�� : " + inforceCount.ToString();
                player.GetComponent<PlayerGold>().Gold -= 100;
            }
            else if (slot2Weapon == "Shotgun")
            {
                UpgradeManager.up_Instance.ShotgunUp();
                inforceCount -= 1;
                inforceCountText.text = "�ܿ� ��ȭ Ƚ�� : " + inforceCount.ToString();
                player.GetComponent<PlayerGold>().Gold -= 100;
            }
        }
    }
}
