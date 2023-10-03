using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using Cinemachine;
using Photon.Realtime;

public class ItemInfoUI: MonoBehaviour
{
    public TextMeshProUGUI weaponName;
    public TextMeshProUGUI weaponDamage;
    public TextMeshProUGUI weaponAmmo;
    public TextMeshProUGUI weaponType;
    public TextMeshProUGUI weaponInfo;

    public Image rifleImage;
    public Image shotgunImage;
    public Image rifleAmmoImage;
    public Image shotgunAmmoImage;

    private ItemGet1 itemRay;

    private void Start()
    {
        itemRay = FindObjectOfType<CinemachineVirtualCamera>().transform.parent.gameObject.GetComponent<ItemGet1>();
    }



    public void  SetItemInfo()
    {
        if(itemRay.hitInfo.collider.gameObject.name == "CrimsonFirescale(get)(Clone)")
        {
            weaponName.text = "�Ҳɺ��" +"(0��)";
            weaponDamage.text = "���� ����� " + "4";
            weaponAmmo.text = "źâ�뷮" + "30";
            weaponType.text = "�Ϲ�ź";
            weaponInfo.text = "�� 30�� �������Դϴ�.";
            rifleImage.gameObject.SetActive(true);
            rifleAmmoImage.gameObject.SetActive(true);
            shotgunImage.gameObject.SetActive(false);
            shotgunAmmoImage.gameObject.SetActive(false);

        }
        else if (itemRay.hitInfo.collider.gameObject.name == "Shotgun(get)(Clone)")
        {
            weaponName.text = "�� ��" + "(0��)";
            weaponDamage.text = "���� ����� " + "3";
            weaponAmmo.text = "źâ�뷮" + "8";
            weaponType.text = "����ź";
            weaponInfo.text = "�� 8�� �����Դϴ�.";
            shotgunImage.gameObject.SetActive(true);
            shotgunAmmoImage.gameObject.SetActive(true);
            rifleImage.gameObject.SetActive(false);
            rifleAmmoImage.gameObject.SetActive(false);
        }
        
    }
        
}
