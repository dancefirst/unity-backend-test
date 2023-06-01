using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEditor.Experimental;

// 로딩 바를 제어하는 스크립트

public class Progress : MonoBehaviour
{
    // Slider와 Text UI를 제어하는 변수와 로딩바 재생 시간 변수를 선언.
    [SerializeField]
    private Slider sliderProgress;
    [SerializeField]
    private TextMeshProUGUI textProgressData;
    [SerializeField]
    private float progressTime; // 로딩바 재생 시간
    
    public void Play(UnityAction action = null)
    {
        // 외부에서 로딩 바를 재생할 때 호출하는 Play() 메소드는 OnProgress() 코루틴 메소드 실행
        // 재생이 완료되었을 때 원하는 메소드를 실행할 수 있도록 UnityAction 타입의 매개변수 받기
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

            // Text 정보 설정
            textProgressData.text = $"Now Loading... {sliderProgress.value * 100:F0}%";
            // Slider 값 설정
            sliderProgress.value = Mathf.Lerp(0, 1, percent);

            yield return null;
        }

        // action이 null이 아니면 action 메소드 실행
        action?.Invoke();
    }

}
