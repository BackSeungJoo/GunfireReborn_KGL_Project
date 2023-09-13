using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // ������ƮǮ �Ŵ��� �̱���
    public static BulletPool instence;

    // ������Ʈ Ǯ�� ��Ƶ� ������
    public GameObject bulletPrefab;

    // �������� ��Ƶ� �޸� Stack
    Stack<Bullet> rifleBullet = new Stack<Bullet>();

    Dictionary<Bullet, Stack> keyValuePairs = new Dictionary<Bullet, Stack>();  
    // 
    private void Awake()
    {

        instence = this;

        Initialized(10); 
    }

    // �� �������� ������ �޼���
    private Bullet CreateNewObject()
    {
        var newObj = Instantiate(bulletPrefab, transform).GetComponent<Bullet>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    private void Initialized(int count)
    {
        for(int i = 0; i < count; i++)
        {
            rifleBullet.Push(CreateNewObject());
        }
    }


    // ������Ʈ ȣ��
    public static Bullet GetObject()
    {
        if(instence.rifleBullet.Count > 0)
        {
            var obj = instence.rifleBullet.Pop();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instence.CreateNewObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    // ������Ʈ ��ȯ
    public static void ReturnObject(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bullet.transform.SetParent(instence.transform);
        instence.rifleBullet.Push(bullet);
    }

}
