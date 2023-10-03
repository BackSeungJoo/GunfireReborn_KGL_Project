using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Reload : MonoBehaviour
{
    public GameObject playerGun;
    public Transform weaponPosition;
    private bool isReloading;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Reload") && !isReloading)
        {
            animator.SetTrigger("Reload");
            
            // ���� õõ�� �Ʒ��� ������ �ڷ�ƾ�� �����մϴ�.
            //StartCoroutine(LowerGun());
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
        // ���ε� ���·� �����մϴ�.
        isReloading = true;
        // Ž���� �����մϴ�.
        Search();

        // �Ȱ� ���� õõ�� �Ʒ��� ������ ó���� �����մϴ�.
        float elapsedTime = 0f;
        float duration = 0.5f; // ������ �ð�

        // ���� �ʱ� ���� ��ġ�� �����մϴ�.
        Vector3 initialArmLocalPosition = playerGun.transform.localPosition;

        // �Ʒ��� ���� ������ ���� ��ǥ ���� ��ġ�� ����մϴ�.
        Vector3 targetArmLocalPosition = initialArmLocalPosition - playerGun.transform.up * 0.5f; // �Ʒ��� ������

        // ���� �ʱ� ���� ȸ�� ���� �����մϴ�.
        Quaternion initialGunLocalRotation = playerGun.transform.localRotation;

        // ���� Z�� �������� ȸ����Ű�� ���� ��ǥ ���� ȸ�� ���� ����մϴ�.
        Quaternion targetGunLocalRotation = Quaternion.Euler(0f, -90f, -45f);

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            // ���� ���� ��ġ�� �����մϴ�.
            playerGun.transform.localPosition = Vector3.Lerp(initialArmLocalPosition, targetArmLocalPosition, t);

            // ���� ���� ȸ���� �����մϴ�.
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

            // ���� ���� ��ġ�� �ٽ� �ʱ� ��ġ�� �����մϴ�.
            playerGun.transform.localPosition = Vector3.Lerp(targetArmLocalPosition, initialArmLocalPosition, t);

            // ���� ���� ȸ���� �ٽ� �ʱ� ȸ������ �����մϴ�.
            playerGun.transform.localRotation = Quaternion.Slerp(targetGunLocalRotation, initialGunLocalRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ �Ϸ� �� ���¸� ������� �ǵ����ϴ�.
        isReloading = false;
    }

}
