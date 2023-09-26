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

    // Start is called before the first frame update
    void Start()
    {
        cam = FindObjectOfType<CinemachineVirtualCamera>();
        weaponManager = GetComponent<WeaponManager1>();
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
                GetItem();
            }
        }
        else
        {   //���̰� �ƹ��͵� �����������
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
                Debug.LogFormat("�� �ȵǴ°ž�?");
                Destroy(nearObject.gameObject);
                Debug.LogFormat("�ı��� �ȵȴٰ�?");
            }
        }
    }

    private void ChangeWeapon()
    {
        if(photonView.IsMine)
        {
            if (weaponManager.slotWeapons[0] == null)
            {//���� 1�������� ����ִٸ�  1���� ���������ϵ�����.
                Debug.Log("1�����Կ�����");
                weaponManager.EquipWeapon(nearObject.name, 0);
            }
            else if (weaponManager.slotWeapons[1] == null)
            {//���� 2�������� ����ִٸ�  2���� ���������ϵ�����.
                Debug.Log("2�����Կ�����");
                weaponManager.EquipWeapon(nearObject.name, 1);
            }
            else
            {//���� 1,2,�� ������ ��� �ִٸ� �������� ����ϰ��ִ� ������ ����� ��ȯ�ϵ�����.
                Debug.Log("1,2���� Ȱ��ȭ�� ���Կ� ����");
                weaponManager.EquipWeapon(nearObject.name, 99); //������ 0�� 1�̿��� ���� EquipWeapon���� ���� Ȱ��ȭ�Ǿ��ִ� ������üũ�ϰ� �׽����� �ٲٱ⶧���� ������ٰ��Ǵ��ߴ�.
            }
        }

    }
}
