using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithManager : MonoBehaviour
{
    private BlackSmith[] blackSmiths;

    private int count = 3; // �ܿ� ��ȭ Ƚ��

    #region ������Ƽ
    public int Count  // �ܿ� ��ȭ Ƚ�� ������Ƽ
    {
        get
        {
            return count;
        }
        set
        {
            count = value;

            for (int i = 0; i < blackSmiths.Length; i++)
            {
                // Debug.Log("����?");

                blackSmiths[i].SetUpgradeCount(count);

                if (count <= 0)
                {
                    // Debug.Log("ī��Ʈ 0�ε� �� ��");
                    blackSmiths[i].SetUpgradeBT();
                }
            }
        }

    }
    #endregion


    private void Awake()
    {
        blackSmiths = FindObjectsOfType<BlackSmith>();  // BlackSmith ��ũ��Ʈ�� ������ �ִ�
                                                        // ������Ʈ�� �����ͼ� �迭�� �ִ´�.
    }

    /// <summary>
    /// ��ȭ ��ư Click �̺�Ʈ
    /// </summary>
    /// <param name="index">BlackSmith �迭�� �ε���</param>
    public void OnUpgrade(int index) 
    {
        Count--;

        blackSmiths[index].WeaponUpgradeCount++;    // ���� ��ȭ Ƚ�� +1
        blackSmiths[index].SetUpgradeWeaponName();  

        blackSmiths[index].UpgradePrice += 100;     // ��ȭ ���� ���� +100
        blackSmiths[index].SetUpgradeCoin();
    }
}
