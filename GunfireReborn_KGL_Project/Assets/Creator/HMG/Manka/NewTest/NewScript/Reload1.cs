using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reload1 : MonoBehaviour
{
    public GameObject playerGun;
    public Transform weaponPosition;
    private bool isReloading;
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Reload") && !isReloading)
        {

            // ���� õõ�� �Ʒ��� ������ �ڷ�ƾ�� �����մϴ�.
            //StartCoroutine(LowerGun());
            animator.SetTrigger("Reload");
        }
    }
    private void Search()
    {// ���� Ȱ��ȭ�� ���⸦ ã�� �Լ�.
        // weaponPosition ������ ��� GameObject�� �迭�� �ֽ��ϴ�.
        int childCount = weaponPosition.childCount;
        for (int i = 0; i < childCount; i++)
        {
            if (weaponPosition.GetChild(i).gameObject.activeSelf == true)
            {
                playerGun = weaponPosition.GetChild(i).gameObject;
            }
        }
    }

    IEnumerator LowerGun()
    {
        // ������ ���� ���·� �����մϴ�.
        isReloading = true;
        Search();
        // �Ȱ� ���� õõ�� �Ʒ��� ������ ó���� �����մϴ�.
        float elapsedTime = 0f;
        float duration = 0.5f; // ������ �ð�
        Vector3 initialArmLocalPosition = playerGun.transform.localPosition;
        Vector3 targetArmLocalPosition = initialArmLocalPosition - playerGun.transform.up * 0.5f; // �Ʒ��� ������

        Quaternion initialGunLocalRotation = playerGun.transform.localRotation;
        Quaternion targetGunLocalRotation = Quaternion.Euler(0f, -90f, -45f); // Z �ุ ȸ��

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            playerGun.transform.localPosition = Vector3.Lerp(initialArmLocalPosition, targetArmLocalPosition, t);
            playerGun.transform.localRotation = Quaternion.Slerp(initialGunLocalRotation, targetGunLocalRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ����մϴ�. (��: ���� �Ʒ��� ���� �� 3�� ���� ���)
        yield return new WaitForSeconds(1.0f);

        // �Ȱ� ���� ���� ��ġ�� ������ ó���� �����մϴ�.
        elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            playerGun.transform.localPosition = Vector3.Lerp(targetArmLocalPosition, initialArmLocalPosition, t);
            playerGun.transform.localRotation = Quaternion.Slerp(targetGunLocalRotation, initialGunLocalRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ �Ϸ� �� ���¸� ������� �ǵ����ϴ�.
        isReloading = false;
    }

}