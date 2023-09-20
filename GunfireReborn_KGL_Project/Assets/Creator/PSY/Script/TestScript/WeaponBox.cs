using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#region WeaponData Ŭ����
public class WeaponData
{
    public string name;
    public int damage;
    public int bulletSize;
    public float criticalPer;
    public string bulletType;
    public string info;
    public string use;

    /// <summary>
    /// �⺻���� ������
    /// </summary>
    public WeaponData() { }

    /// <summary>
    /// �ܺε����͸� �������ִ� ������
    /// </summary>
    public WeaponData(string name, int damage, int bulletSize, float criticalPer, string bulletType, string info, string use)
    {
        this.name = name;
        this.damage = damage;
        this.bulletSize = bulletSize;
        this.criticalPer = criticalPer;
        this.bulletType = bulletType;
        this.info = info;
        this.use = use;
    }
}
#endregion

public class WeaponBox : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public WeaponData data;

    #region ��Ŭ�� Drop
    private Image dropBg;        // drop���
    private Image dropGauge;     // drop ������ ( ���൵ )

    private bool isStop = false; // Ŭ���� ������� Ȯ���� ����
    #endregion

    #region F ����
    private Image weaponImage;               // ���� �̹���
    private Image bulletTypeImage;           // ���� ź�� ���� �̹���

    private TextMeshProUGUI weaponName;      // ���� �̸�
    private TextMeshProUGUI damageText;      // ���� �����
    private TextMeshProUGUI bulletSizeText;  // ���� źâ �뷮
    private TextMeshProUGUI criticalText;    // ���� ġ��Ÿ Ȯ��
    private TextMeshProUGUI bulletTypeText;  // ���� ź�� ���� �ؽ�Ʈ
    private TextMeshProUGUI infoText;        // ���� ����
    private TextMeshProUGUI useText;         // ��� ���� �ؽ�Ʈ
    #endregion

    private void Awake()
    {
        SettingUI();
    }

    private void SettingUI()
    {
        #region ��Ŭ�� Drop
        dropBg = transform.Find("DropBg").GetComponent<Image>();
        dropGauge = dropBg.transform.Find("DropImage").GetComponent<Image>();

        dropBg.gameObject.SetActive(false);
        #endregion

        #region F ����
        weaponImage = transform.Find("WeaponBg/WeaponImage").GetComponent<Image>();
        bulletTypeImage = transform.Find("WeaponBg/WeaponTypeBg/WeaponTypeText/WeapomTypeImage").GetComponent<Image>();
        weaponName = transform.Find("WeaponName").GetComponent<TextMeshProUGUI>();
        damageText = transform.Find("WeaponStat/Damage/DamageText").GetComponent<TextMeshProUGUI>();
        bulletSizeText = transform.Find("WeaponStat/BulletSize/BulletSizeText").GetComponent<TextMeshProUGUI>();
        criticalText = transform.Find("WeaponStat/Critical/CriticalText").GetComponent<TextMeshProUGUI>();
        bulletTypeText = transform.Find("WeaponBg/WeaponTypeBg/WeaponTypeText").GetComponent<TextMeshProUGUI>();
        infoText = transform.Find("WeaponInfo").GetComponent<TextMeshProUGUI>();
        useText = transform.Find("isUse").GetComponent<TextMeshProUGUI>();
        #endregion
    }

    #region ��Ŭ�� Drop
    /// <summary>
    /// ���콺�� ������ �� �̺�Ʈ ( Drop )
    /// </summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��Ŭ���� ����ȵǰ� ����
        if ( eventData.button == PointerEventData.InputButton.Left )  // �𸣸� �ܿ� 
        {
            return;
        }

        isStop = false;
        StartCoroutine(OnDropGauge());
    }

    /// <summary>
    /// ���콺�� ������ �� ���� �ڷ�ƾ �Լ� ( Drop )
    /// </summary>
    private IEnumerator OnDropGauge()
    {
        dropBg.gameObject.SetActive(true);
        while ( dropGauge.fillAmount < 1 )
        {
            if ( isStop )
            {
                yield break;
            }

            dropGauge.fillAmount += Time.deltaTime;

            yield return null;
        }

        yield break;
    }

    /// <summary>
    /// ���콺�� ���� �� �̺�Ʈ ( Drop )
    /// </summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        isStop = true;
        StartCoroutine(OffDropGauge());
    }

    /// <summary>
    /// ���콺�� ���� �� ���� �ڷ�ƾ �Լ� ( Drop )
    /// </summary>
    private IEnumerator OffDropGauge()
    {
        while ( dropGauge.fillAmount > 0)
        {
            if (!isStop)
            {
                yield break;
            }

            dropGauge.fillAmount -= Time.deltaTime;

            yield return null;
        }

        dropBg.gameObject.SetActive(false);

        yield break;
    }

    /// <summary>
    /// ���� ������Ʈ�� ��Ȱ��ȭ �Ǿ��� �� ����Ǵ� �Լ� ( Drop )
    /// </summary>
    private void OnEnable()
    {  // 1ȸ���� �ƴ� ��Ȱ��ȭ �� ������ ��� ����Ǵ� �Լ��̴�.
        if ( dropGauge?.fillAmount > 0 )
        {
            dropGauge.fillAmount = 0;
            dropBg.gameObject.SetActive(false);
        }
    }
    #endregion

    #region F ����
    /// <summary>
    /// WeaponData���� �Լ�
    /// </summary>
    /// <param name="otherData">�ٲ� WeaponData</param>
    public void SetData(WeaponData otherData)
    {
        data = otherData;

        weaponName.text = data.name;
        damageText.text = data.damage.ToString();
        bulletSizeText.text = data.bulletSize.ToString();
        criticalText.text = data.criticalPer.ToString();
        infoText.text = data.info;
        useText.text = data.use;
    }
    #endregion
}
