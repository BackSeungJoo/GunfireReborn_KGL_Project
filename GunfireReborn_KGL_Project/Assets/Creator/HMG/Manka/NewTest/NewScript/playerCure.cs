using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
//using static UnityEditor.FilePathAttribute;

public class playerCure : MonoBehaviourPun
{
    //�÷��̾� ���¸� �������� ����
    private playerHp hp;
  
    // Start is called before the first frame update
    void Start()
    {
        
        hp = GetComponent<playerHp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }

       

      
    }

   
}
