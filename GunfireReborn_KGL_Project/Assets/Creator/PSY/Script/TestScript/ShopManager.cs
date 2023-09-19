using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private UIManager uiManager;

    public ItemDataManager itemDataManager;

    private int count = 3;

    #region ������Ƽ
    public int Count  // Reroll Count ������Ƽ
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
                    Debug.Log("3");
                    
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
        itemDataManager = GameObject.Find("@Managers").GetComponent<ItemDataManager>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.SetActiveShopPopup(false);
            uiManager.SetActiveInven(false);
            uiManager.SetActiveBlackSmith(false);

        }
    }
}
