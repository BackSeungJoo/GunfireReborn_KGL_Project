using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Photon.Pun;
using System.Xml;

public class ItemGet1 : MonoBehaviourPun
{
    GameObject nearObject;

    Item nearItem;
    //�÷��̾� ��������
    WeaponManager1 weaponManager;

    //���̸� �������� ķ
    public CinemachineVirtualCamera cam;

    //���� ����
    public RaycastHit hitInfo;

    //������ �����Ÿ�
    private float rayDistance = 10f;

    //���̰� �������� �����Һ���
    Vector3 hitPosition = Vector3.zero;

    public GameObject itemInfo;
    public ItemInfoUI ItemInfo2;

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        weaponManager = GetComponent<WeaponManager1>();
        itemInfo = GameObject.Find("MainUICanvas").transform.GetChild(0).gameObject;
        ItemInfo2 = itemInfo.GetComponent<ItemInfoUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if(!photonView.IsMine)
        {
            return;
        }

        ShotRaycast();
    }

    private void ShotRaycast()
    {
        //����ĳ��Ʈ �߻�
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, rayDistance))
        {   //���̰� �浹�Ѱ��
            Debug.DrawRay(cam.transform.position, cam.transform.forward * rayDistance, Color.red);
            if (hitInfo.transform.CompareTag("weapon"))
            {
                itemInfo.SetActive(true);
                ItemInfo2.SetItemInfo();
                GetItem();
            }
            else
            {
                itemInfo.SetActive(false);
            }
        }
        else
        {   //���̰� �ƹ��͵� �����������
            itemInfo.SetActive(false);
            hitPosition = cam.transform.position + cam.transform.forward * rayDistance;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("���ǿ� �����Ű�����?");
        if(other.tag == "weapon")
        {
            //���� Ʈ���ſ� ������� nearObject�� ���⸦��´�.
            nearObject = other.gameObject;
            nearItem = nearObject.GetComponent<Item>(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "weapon")
        {
            //���� Ʈ���Ÿ� ������� nearObject��  null�� ����.
            nearObject = null;
        }
    }

    private void GetItem()
    {
        //ToDo : if������ ���� �������̶�� �ٷ� return�ϵ��� �������Ѵ�.
        //�����߿��� �������� ������������.
        if (nearObject != null)
        {
            //Debug.LogFormat("{0}", nearObject.name);
            if (nearObject.tag == "weapon" && Input.GetButtonDown("Get"))
            {//������ �ְ�, ������ ���� �Է����ް�, ����ĳ��Ʈ�� �¾�����
             //�ϴ��� �ӽ������� �������� �ı��ϰ� ������

                ChangeWeapon();
                Destroy(nearObject.gameObject);
            }
        }
    }

    private void ChangeWeapon()
    {
        if(photonView.IsMine)
        {
            if (weaponManager.slotWeapons[0] == null)
            {//���� 1�������� ����ִٸ�  1���� ���������ϵ�����.
                weaponManager.EquipWeapon(nearObject.name, 0);
            }
            else if (weaponManager.slotWeapons[1] == null)
            {//���� 2�������� ����ִٸ�  2���� ���������ϵ�����.
                weaponManager.EquipWeapon(nearObject.name, 1);
            }
            else
            {//���� 1,2,�� ������ ��� �ִٸ� �������� ����ϰ��ִ� ������ ����� ��ȯ�ϵ�����.
                weaponManager.EquipWeapon(nearObject.name, 99); 
                //������ 0�� 1�̿��� ���� EquipWeapon���� ���� Ȱ��ȭ�Ǿ��ִ� ������üũ�ϰ� �׽����� �ٲٱ⶧���� ������ٰ��Ǵ��ߴ�.
            }
        }

    }
}
