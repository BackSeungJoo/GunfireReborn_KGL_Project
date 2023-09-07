using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //���� Ȱ��ȭ���ִ� ������ Ȯ���ϱ����Ѻ���
    public bool[] ActiveSlot;
    //���� ���Կ� �ִ� ������ Ȯ���ϱ����� ����
    public string[] slotWeapons;
    //��� ������ ������
    public string[] weapons;
    //������������ �������� ������ ����迭
    public GameObject[] weaponPrefabs;
    //�÷��̾��� IK�Լ��� ������������ ����
    private IK playerIK;
    //������������ �ȿ��ִ� ����������Ʈ���� �����״� �ϱ����� ����迭
    public GameObject[] Equip_weapons;

    // Update is called once per frame


    private void Start()
    {
        playerIK = gameObject.GetComponent<IK>();
        weapons[0] = "Pistol";
        weapons[1] = "Rifle";
        //Todo: weapons[]�迭�� �����͵� �߰�������Ѵ�.
        //weapons�迭�� ������� ������ ����������迭�� �����Ͱ��ƾ��Ѵ�. ���� Equip_weapons�� �����͵����ƾ��Ѵ�.
        //������ ����迭�� �ݵ�� �ν�����â���� �����̵Ǿ��־���Ѵ�.

        //���� ���ο� ���⸦ �߰��ϰ�ʹٸ� weapons�� weaponPrefabs�迭�� �ʼ������� �߰��ؾ��Ѵ�.
    }
    void Update()
    {
        if(Input.GetButtonDown("Swap1"))
        {
            //Todo : ���� ����ϰ��ִ� ��� ������Կ��ִ��� Ȯ���ϰ� 1���̶�� �״�γ��ΰ�
            //2��, Ȥ�� 3���̶��  ���� ���� ����ְų� �������̶�� �ִϸ��̼��� ����ϰ�
            //1���� �����ִ� ���⸦ �ٽ�Ȱ��ȭ��Ű�� ik�� �����Ѵ�.
        }
        else if(Input.GetButtonDown("Swap2"))
        {
            //
        }
        else if(Input.GetButtonDown("Swap3"))
        {

        }
    }

    public void EquipWeapon(string weaponName,int First)
    {
        if (First == 0)
        {   //���� ù��° ������ ����־ �����Ȱ����
            //ù��° ������ �������� ���� ���������� �ٲ۴�.
            slotWeapons[0] = weaponName;
            //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
            playerIK.ChangeIK(weaponName);
            //�׸��� �ȿ��ִ� ���⸦ Ȱ��ȭ��Ų��.
            Equip_weapons[SearchWeapon()].SetActive(true);
            //
        }
        else if (First == 1)
        {   //���� �ι�° ������ ����־ �����Ȱ����
            //�ι�° ������ �������� ���� ���������� �ٲ۴�.
            slotWeapons[1] = weaponName;
            //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
            playerIK.ChangeIK(weaponName);
            //�׸��� �ȿ��ִ� ���⸦ Ȱ��ȭ��Ų��.
            Equip_weapons[SearchWeapon()].SetActive(true);
            //
        }
        else
        {
            //���� 1,2�������� ��� ���Ⱑ �����Ǿ��ִ� �����
            //���� Ȱ��ȭ�� ������ ����������� üũ�ϰ�
            //�׽����� �������� ���� ���������� �ٲ۴�.
            slotWeapons[CheckActiveslot()] = weaponName;
            //���� ������������ �̸��� Ȯ���ؼ� IK�� �ٲ۴�.
            playerIK.ChangeIK(weaponName);
            //�׸��� �ȿ��ִ� ���⸦ Ȱ��ȭ��Ų��.
            Equip_weapons[SearchWeapon()].SetActive(true);
            //
        }
    }

    public int CheckActiveslot()
    {
        //�ƹ����⵵ �ȸ������´� �⺻����3�������� Ȱ��ȭ�Ǿ��ϱ⿡ 2�� �⺻���̴�.
        int ActiveSlotNum=2;
        for(int i =0; i<3; i++)
        {
            if(ActiveSlot[i] == true)
            {
                ActiveSlotNum = i;
            }
        }

        return ActiveSlotNum;
    }

    public void WeaponDrop()
    {
        int ActiveWeaponNum = 999;
        //�����ϰ��ִ� ������ ������ Ȯ���Ѵ�.
        ActiveWeaponNum = SearchWeapon();

        if (ActiveWeaponNum != 999)
        {//���� �����������ִ� ���Ⱑ ��ü���⸮��Ʈ�� �ִٸ�  (���� ���ٸ� 999����)
         //prefabs�迭���� �� �ε�����ȣ�� instantiate�Ѵ�.

            // �÷��̾��� ���� ��ġ�� �����´�. (��: transform.position�� ���)
            Vector3 playerPosition = transform.position;

            // �÷��̾��� ���� ��ġ���� z ��ǥ�� +2�� �̵��� ��ġ�� Instantiate�Ѵ�.
            Vector3 spawnPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 2);

            // Instantiate �Լ��� ����Ͽ� ���⸦ �����Ѵ�.
            Instantiate(weaponPrefabs[ActiveWeaponNum], spawnPosition, Quaternion.identity);
            // ���������� ���տ� �ִ� ���⸦ false�� ��Ȱ��ȭ��Ų��.
            Equip_weapons[ActiveWeaponNum].SetActive(false);
        }
        else
        {
            Debug.LogFormat("�����������ִ� ���Ⱑ ��ü ����迭�� ����.");
        }
    }

    public int SearchWeapon()
    {// ���� �������ִ� ���Ⱑ ��ü����Ʈ�� ����ε����� �ִ��� Ȯ���ϴ��Լ�. ���ٸ� 999�� ��ȯ�Ѵ�.
        for(int i=0; i<weapons.Length;i++)
        {
            if (slotWeapons[CheckActiveslot()] == weapons[i])
            {
                return i;
            }
        }
        return 999;
    }
}
