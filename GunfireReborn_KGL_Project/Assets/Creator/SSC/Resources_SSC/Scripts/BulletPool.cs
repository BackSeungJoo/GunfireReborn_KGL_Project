using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    // ������ƮǮ �Ŵ��� �̱���
    public static BulletPool instance;

    // ������Ʈ Ǯ�� ��Ƶ� ������
    public GameObject bulletPrefab;

    // �������� ��Ƶ� �޸� Stack
    Stack<Bullet> rifleBullet = new Stack<Bullet>();

    // 
    private void Awake()
    {

        instance = this;

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
        if(instance.rifleBullet.Count > 0)
        {
            var obj = instance.rifleBullet.Pop();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = instance.CreateNewObject();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    // ������Ʈ ��ȯ
    public static void ReturnObject(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);

        bullet.transform.SetParent(instance.transform);
        instance.rifleBullet.Push(bullet);
    }

}
