using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private UIManager uiManager;

    #region ItemList
    private List<string> itemNameList = new List<string> { "�ű��� ����", "ź�� ����", "Ư��ź", "����ź", "�Ϲ�ź" };
    private List<string> itemInfoList = new List<string>
    {"�ű��� ����. �ӿ� ���� ��������� ����, ������ 30%�� ������� ȸ��",
     "��� ź���� �����Ͽ� �� ä���",
     "Ư�� ź���� �����Ͽ� �� ä���",
     "���� ź���� �����Ͽ� �� ä���",
     "�Ϲ� ź���� �����Ͽ� �� ä���"};

    /// <summary>
    /// ItemNameList ������Ƽ
    /// </summary>
    public List<string> ItemNameList { get { return itemNameList; } }
    /// <summary>
    /// ItemInfoList ������Ƽ
    /// </summary>
    public List<string> ItemInfoList { get { return itemInfoList; } }
    #endregion

    #region Reroll
    private int count = 3;
    /// <summary>
    /// Reroll Count ������Ƽ
    /// </summary>
    public int Count
    {
        get
        {
            return count;
        }
        set
        {
            count = value;

            if (count >= 0)
            {
                PlayerTest player = GameObject.Find("Player").GetComponent<PlayerTest>();
                for (int i = 0; i < player.shopScripts.Count; i++)
                {
                    player.shopScripts[i].soldOut.SetActive(false);
                    player.shopScripts[i].enabled = true;
                }
            }
        }
    }
    #endregion

    private void Start()
    {
        uiManager = GameObject.Find("@Managers").GetComponent<UIManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && uiManager.CheckActiveShopPopup())
        {
            uiManager.SetActiveShopPopup(false);

        }
    }
}
