using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public List<Sprite> loadingList = new List<Sprite>();
    public List<string> loadingTipList = new List<string>();
    public Image loadingImage;
    public TextMeshProUGUI loadingTipText;
    public Image loadingBar;

    private float[] fill = new float[3] { 0.06f, 0.52f, 1f };

    private void Awake()
    {
        int randCount = UnityEngine.Random.Range(0, fill.Length);
        loadingBar.fillAmount = fill[randCount];

        #region �ε� �� List
        loadingTipList.Add("�����Ƿ��� �پ�� ��κ��� ��Ȳ�� ���� ��ó�� �� �� �ִ�.");
        loadingTipList.Add("�ڽÿ��� 2002����̴�.");
        loadingTipList.Add("����� 2022�� 11�� 08�Ͽ� �����Խ��ϴ�.");
        loadingTipList.Add("�ż�â : ������ ���ֿ���");
        loadingTipList.Add("����� : �θ���������");
        loadingTipList.Add("�Ѹ��� : ��? �𸣰Ե�");
        #endregion

        // �������� �ε� �̹��� �� �ؽ�Ʈ�� ����Ѵ�. {
        int randImageNum = UnityEngine.Random.Range(0, loadingList.Count);
        int randTextNum = UnityEngine.Random.Range(0, loadingTipList.Count);

        loadingImage.sprite = loadingList[randImageNum];
        loadingTipText.text = loadingTipList[randTextNum];
        // } �������� �ε� �̹��� �� �ؽ�Ʈ�� ����Ѵ�.
    }

    private void Start()
    {
        Debug.Log("����");
        if(GameManager.instance?.nowStage >= 1 && GameManager.instance?.nowStage <= 3)
        {
            GameManager.instance.nowStage++;
            Debug.Log("����" + GameManager.instance.nowStage);
            StartCoroutine(LoadSceneMap("Main_Map_0" + GameManager.instance.nowStage));
        }
    }

    #region �񵿱� �ε�
    /// <summary>
    /// �񵿱� �ε� �Լ� ( Map 01 )
    /// </summary>
    public IEnumerator LoadSceneMap( string sceneName )
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;  // ���� �ε��ϴµ� �غ� �ȵ�.

        while (!operation.isDone && operation.allowSceneActivation == false)  // ���� �ε尡 ���� ������ �ݺ�
        {
            yield return new WaitForSeconds(3f);    // ���� �ð� 3�� �����̸� �ش�.

            operation.allowSceneActivation = true;  // �� �ε��� �غ� ������.
        }
    }
    #endregion
}
