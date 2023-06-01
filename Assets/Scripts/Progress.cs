using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEditor.Experimental;

// �ε� �ٸ� �����ϴ� ��ũ��Ʈ

public class Progress : MonoBehaviour
{
    // Slider�� Text UI�� �����ϴ� ������ �ε��� ��� �ð� ������ ����.
    [SerializeField]
    private Slider sliderProgress;
    [SerializeField]
    private TextMeshProUGUI textProgressData;
    [SerializeField]
    private float progressTime; // �ε��� ��� �ð�
    
    public void Play(UnityAction action = null)
    {
        // �ܺο��� �ε� �ٸ� ����� �� ȣ���ϴ� Play() �޼ҵ�� OnProgress() �ڷ�ƾ �޼ҵ� ����
        // ����� �Ϸ�Ǿ��� �� ���ϴ� �޼ҵ带 ������ �� �ֵ��� UnityAction Ÿ���� �Ű����� �ޱ�
        StartCoroutine(OnProgress(action));
    }

    private IEnumerator OnProgress(UnityAction action)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / progressTime;

            // Text ���� ����
            textProgressData.text = $"Now Loading... {sliderProgress.value * 100:F0}%";
            // Slider �� ����
            sliderProgress.value = Mathf.Lerp(0, 1, percent);

            yield return null;
        }

        // action�� null�� �ƴϸ� action �޼ҵ� ����
        action?.Invoke();
    }

}
