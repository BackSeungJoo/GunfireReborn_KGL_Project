using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    #region Dash 
    private Image t_DashBG;
    private TextMeshProUGUI text_dashCoolTime;
    #endregion

    #region DashEffect
    private Transform dashEffectGroup;
    private Transform[] dashEffectImages;
    #endregion

    private void Start()
    {
        #region Dash
        t_DashBG = transform.Find("T_DashBG").GetComponent<Image>();
        text_dashCoolTime = t_DashBG.transform.Find("Text_dashCoolTime").GetComponent<TextMeshProUGUI>();

        t_DashBG.enabled = false;           // Dash UI BG ��Ȱ��ȭ
        text_dashCoolTime.enabled = false;  // Dash UI Text ��Ȱ��ȭ
        #endregion

        #region DashEffect

        dashEffectGroup = transform.Find("DashEffectGroup").GetComponent<Transform>();

        // dashEffectGroup �ڽ� ������Ʈ ������ŭ�� dashEffectImages �迭 ����
        dashEffectImages = new Transform[dashEffectGroup.childCount];  

        for ( int i = 0; i < dashEffectGroup.childCount; i++ )
        {
            // dashEffectImages�� dashEffectGroup�� �ڽ��� �ִ´�.
            dashEffectImages[i] = dashEffectGroup.GetChild(i);
        }
        #endregion
    }

    #region Dash
    /// <summary>
    /// Dash ��Ÿ�� �ڷ�ƾ ���� �Լ�
    /// </summary>
    public void CountDashCoolTime()
    {
        StartCoroutine(DecreaseDash(3f));
    }

    /// <summary>
    /// Dash ��Ÿ�� ���� �ڷ�ƾ �Լ�
    /// </summary>
    public IEnumerator DecreaseDash(float cool)
    {
        t_DashBG.enabled = true;          // Dash UI BG Ȱ��ȭ
        text_dashCoolTime.enabled = true; // Dash UI Text Ȱ��ȭ

        float coolText = 3;  // ��Ÿ�� Text�� �ʱ� ���� 3���� ����

        while (t_DashBG.fillAmount < 1)  // Dasg UI BG�� Fill�� 1�� �� ������ �ݺ�
        {
            t_DashBG.fillAmount += 1 * Time.smoothDeltaTime / cool;  // ��Ÿ���� �������� fill�� �����Ӹ��� �����ش�.
            coolText -= Time.smoothDeltaTime;  // ��Ÿ�� Text�� �����Ӹ��� ���ش�.
            text_dashCoolTime.text = string.Format("{0:N1}", coolText);  // ��Ÿ�� Text�� �Ҽ��� ���ڸ����� ������ ����Ѵ�.

            yield return null;
        }

        t_DashBG.fillAmount = 0f;          // Dash UI BG�� fill�� 0���� �����Ѵ�.
        t_DashBG.enabled = false;          // Dash UI BG ��Ȱ��ȭ
        text_dashCoolTime.enabled = false; // Dash UI Text ��Ȱ��ȭ
    }
    #endregion

    #region DashEffect
    /// <summary>
    /// Dash �ߵ� �� ����Ǵ� �ڷ�ƾ �Լ�
    /// </summary>
    public IEnumerator DashEffect()
    {
        int dashEffectRand = Random.Range(0, dashEffectImages.Length);

        dashEffectImages[dashEffectRand].transform.localScale = Vector3.one;   // Dash Effect Image�� Scale�� 1�� �����Ѵ�.

        yield return new WaitForSeconds(0.2f);  // 2�� �����̸� �ش�.

        dashEffectImages[dashEffectRand].transform.localScale = Vector3.zero;  // Dash Effect Image�� Scale�� 0���� �����Ѵ�.
    }
    #endregion
}
