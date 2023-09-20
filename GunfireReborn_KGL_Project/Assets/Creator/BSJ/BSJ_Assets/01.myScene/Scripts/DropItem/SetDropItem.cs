using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SetDropItem : MonoBehaviour
{
    public List<GameObject> dropItems;

    private int coinCount = 1;
    private bool dropBullet01 = false;
    private bool dropBullet02 = false;
    private bool dropGun01 = false;
    private bool dropGun02 = false;

    public void DropItem(Transform deadEnemyPos)
    {
        // ��� ������ ���ϱ�
        ChooseDropItems();

        // ���� ����
        for (int i = 0; i < coinCount; i++)
        {
            GameObject coin = Instantiate(dropItems[0], gameObject.transform);
            coin.transform.position = deadEnemyPos.position;
            coin.SetActive(true);
        }

        // �Ѿ� 1 ���
        if(dropBullet01 == true)
        {
            GameObject bullet01 = Instantiate(dropItems[1], gameObject.transform);
            bullet01.transform.position = deadEnemyPos.position;
            bullet01.SetActive(true);
        }

        // �Ѿ� 2 ���
        if (dropBullet02 == true)
        {
            GameObject bullet02 = Instantiate(dropItems[2], gameObject.transform);
            bullet02.transform.position = deadEnemyPos.position;
            bullet02.SetActive(true);
        }

        // ���� (�Ҳɺ��) ���
        if (dropGun01 == true)
        {
            GameObject gun01 = Instantiate(dropItems[3], gameObject.transform);
            gun01.transform.position = deadEnemyPos.position;
            gun01.SetActive(true);
        }

        // ���� (����) ���
        if (dropGun02 == true)
        {
            GameObject gun02 = Instantiate(dropItems[4], gameObject.transform);
            gun02.transform.position = deadEnemyPos.position;
            gun02.SetActive(true);
        }
    }

    // ����� �������� ���Ѵ�.
    public void ChooseDropItems()
    {
        // [0] ������ ������ �����. 1/3 Ȯ���� 2���� ������ ����ǰ�, 1/3 Ȯ���� 3���� ������ �����.
        // [1] �Ѿ� 1 ���Ȯ�� 1/3
        // [2] �Ѿ� 2 ���Ȯ�� 1/3
        // [3] ���� (�Ҳɺ��) ���Ȯ�� 1/10
        // [4] ���� (����) ���Ȯ�� 1/10

        // ���� ���
        int randomCoinDrop = Random.Range(0, 3);
        if (randomCoinDrop == 0) { coinCount = 1; }
        else if (randomCoinDrop == 1) { coinCount = 2; }
        else if (randomCoinDrop == 2) { coinCount = 3; }

        // �Ѿ� 1 ���
        int randomBullet01Drop = Random.Range(0, 3);
        if (randomBullet01Drop == 0) { dropBullet01 = true; }
        else { dropBullet01 = false; }

        // �Ѿ� 2 ���
        int randomBullet02Drop = Random.Range(0, 3);
        if (randomBullet02Drop == 0) { dropBullet02 = true; }
        else { dropBullet02 = false; }

        // ���� (�Ҳɺ��) ���
        int randomGun01Drop = Random.Range(0, 20);
        if (randomGun01Drop == 0) { dropGun01 = true; }
        else { dropGun01 = false; }

        // ���� (����) ���
        int randomGun02Drop = Random.Range(0, 20);
        if (randomGun02Drop == 0) { dropGun02 = true; }
        else { dropGun02 = false; }
    }
}
