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
   
    private float[] fill = new float[5]{ 0.05f, 0.28f , 0.52f , 0.75f , 1f };

    private void Awake()
    {
        int randCount = UnityEngine.Random.Range(0, fill.Length);
        loadingBar.fillAmount = fill[randCount];

        loadingTipList.Add("�����Ƿ��� �پ�� ��κ��� ��Ȳ�� ���� ��ó�� �� �� �ִ�.");
        loadingTipList.Add("�ڽÿ��� 2002����̴�.");
        loadingTipList.Add("����� 2022�� 11�� 08�Ͽ� �����Խ��ϴ�.");
        loadingTipList.Add("�ż�â : ������ ���ֿ���");
        loadingTipList.Add("����� : �θ���������");
        loadingTipList.Add("�Ѹ��� : ��? �𸣰Ե�");

        int randImageNum = UnityEngine.Random.Range(0, loadingList.Count);
        int randTextNum = UnityEngine.Random.Range(0, loadingTipList.Count);

        loadingImage.sprite = loadingList[randImageNum];
        loadingTipText.text = loadingTipList[randTextNum];

        //StartCoroutine(LoadScene());
    }
     
    #region �񵿱� 
    //private IEnumerator LoadScene()
    //{
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("Map_01_PSY");
    //    operation.allowSceneActivation = false;

    //    while ( !operation.isDone )
    //    {
    //        int randImageNum = UnityEngine.Random.Range(0, loadingList.Count);
    //        int randTextNum = UnityEngine.Random.Range(0, loadingTipList.Count);

    //        loadingImage.sprite = loadingList[randImageNum];
    //        loadingTipText.text = loadingTipList[randTextNum];

    //        yield return new WaitForSeconds(3f);

    //        operation.allowSceneActivation = true;
    //    }
    //}
    #endregion
}
