using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public enum Type { Coin, Weapon, Ammo, Food}

    public bool canGetState;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canGetState == false)
        {

        }
    }

    public void ShowInfo()
    {
        canGetState = true;
        Debug.LogFormat("����� ��������������");
    }    

    public void NotShowInfo()
    {
        canGetState = false;
        Debug.LogFormat("�������� �Ⱥ����־�");
    }
}


