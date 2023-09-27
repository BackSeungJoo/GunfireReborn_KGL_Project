using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryCanvas : MonoBehaviour
{
    private GameObject player;                      // �÷��̾�
    private WeaponManager1 weapon;

    public TextMeshProUGUI weaponName1;
    public TextMeshProUGUI weaponName2;

    public TextMeshProUGUI weaponDamage1;
    public TextMeshProUGUI weaponDamage2;

    public TextMeshProUGUI weaponAmmo1;
    public TextMeshProUGUI weaponAmmo2;

    public TextMeshProUGUI weaponType1;
    public TextMeshProUGUI weaponType2;

    public Image weaponTypeImage1;
    public Image weaponTypeImage2;

    public Image weaponTypeImage3;
    public Image weaponTypeImage4;

    public Image bulletTypeImage1;
    public Image bulletTypeImage2;

    public Image bulletTypeImage3;
    public Image bulletTypeImage4;

    public TextMeshProUGUI weaponInfo1;
    public TextMeshProUGUI weaponInfo2;

    public TextMeshProUGUI weaponDamage3;
    public TextMeshProUGUI weaponAmmo3;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CinemachineVirtualCamera>().transform.parent.gameObject;
        weapon = player.GetComponent<WeaponManager1>();
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInventory();
    }

    private void checkInventory()
    {
        weaponDamage3.text = "���� ����� 5";
        weaponAmmo3.text = "źâ �뷮 9";
        if (weapon.slotWeapons[0] !=null)
        {
            if (weapon.slotWeapons[0] == "CrimsonFirescale")
            {
                weaponName1.text = "�Ҳ� ��� (" + UpgradeManager.up_Instance.rifleUp +"��)";
                weaponDamage1.text = UpgradeManager.up_Instance.rifleDamage.ToString();
                weaponAmmo1.text = "30";
                weaponInfo1.text = "�������Դϴ�.";
                weaponType1.text = "Ư��ź";
            }
            else if (weapon.slotWeapons[0] == "Shotgun")
            {
                weaponName1.text = "�� �� (" + UpgradeManager.up_Instance.shotgunUp + "��)";
                weaponDamage1.text = UpgradeManager.up_Instance.shotgunDamage.ToString();
                weaponAmmo1.text = "8";
                weaponInfo1.text = "�����Դϴ�.";
                weaponType1.text = "����ź";
            }
        }
        if (weapon.slotWeapons[1] != null)
        {
            if (weapon.slotWeapons[1] == "CrimsonFirescale")
            {
                weaponName2.text = "�Ҳ� ��� (" + UpgradeManager.up_Instance.rifleUp + "��)";
                weaponDamage2.text = UpgradeManager.up_Instance.rifleDamage.ToString();
                weaponAmmo2.text = "30";
                weaponInfo2.text = "�������Դϴ�.";
                weaponType2.text = "Ư��ź";
            }
            else if (weapon.slotWeapons[1] == "Shotgun")
            {
                weaponName2.text = "�� �� (" + UpgradeManager.up_Instance.shotgunUp + "��)";
                weaponDamage2.text = UpgradeManager.up_Instance.shotgunDamage.ToString();
                weaponAmmo2.text = "8";
                weaponInfo2.text = "�����Դϴ�.";
                weaponType2.text = "����ź";
            }
        }
    }
}
