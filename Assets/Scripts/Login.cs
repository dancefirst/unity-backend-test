using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;


public class Login : LoginBase
{
    // 로그인에 사용하는 ID 필드, PW 필드, 로그인 버튼 UI 변수 선언
    [SerializeField]
    private Image imageID; // ID 필드 색상 변경
    [SerializeField]
    private TMP_InputField inputFieldID; // ID 필드 텍스트 정보 추출
    [SerializeField]
    private Image imagePW; // PW 필드 색상 변경
    [SerializeField]
    private TMP_InputField inputFieldPW; // PW 필드 텍스트 정보 추출
    [SerializeField]
    private Button btnLogin; // 로그인 버튼 (상호작용 가능/불가능)

    public void OnClickLogin()
    {
        // 로그인 버튼 클릭 시 호출
        // 매개변수로 입력한 InputField UI 색상과 Message 내용 초기화
        ResetUI(imageID, imagePW);

        // 필드 값이 비어있는지 체크
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "아이디")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "비밀번호")) return;

        // 로그인 버튼을 연타하지 못하도록 버튼 상호작용 비활성화
        btnLogin.interactable = false;

        // 서버에 로그인을 요청하는 동안 화면에 출력하는 내용 업데이트
        // ex) 로그인 관련 텍스트 출력, 톱니바퀴 아이콘 회전 등
        StartCoroutine(nameof(LoginProcess));

        // 서버와 통신해 로그인을 시도하고, 성공/실패 처리하는 ResponseToLogin() 메소드 호출
        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
    }

    // 로그인 시도 후 서버로부터 전달받은 message를 기반으로 로직 처리
    private void ResponseToLogin(string ID, string PW)
    {
        // 서버에 로그인 요청
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
        StopCoroutine(nameof(LoginProcess));

        // 로그인 성공
        if (callback.IsSuccess())
        {
            SetMessage($"{inputFieldID.text}님 환영합니다.");
        }
        // 로그인 실패
        else
        {
            // 로그인에 실패했을 때는 다시 로그인을 해야하기 때문에 "로그인" 버튼 상호작용 활성화한다.
            btnLogin.interactable = true;

            string message = string.Empty;
            switch (int.Parse(callback.GetStatusCode()))
            {
                    case 401: // 존재하지 않는 아이디, 잘못된 비밀번호
                        message = callback.GetMessage().Contains("customId") ? $"존재하지 않는 아이디입니다.\nStatus code {callback.GetStatusCode()}\n{callback.GetMessage()}" : "잘못된 비밀번호 입니다.";
                        Debug.Log($"Server message -> {callback.GetMessage()}");
                        break;
                    case 403: // 유저 or 디바이스 차단
                        message = callback.GetMessage().Contains("user") ? "차단당한 유저입니다." : "차단당한 디바이스입니다.";
                        break;
                    case 410: // 탈퇴 진행 중
                        message = "탈퇴가 진행 중인 유저입니다.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
            }

            // StatusCode 401에서 "잘못된 비밀번호 입니다." 일 때
            if (message.Contains("비밀번호"))
            {
                    GuideForincorrectlyEnteredData(imagePW, message);
            }   
            else
            {
                    GuideForincorrectlyEnteredData(imageID, message);
            }
            
        }
        });
    }

    // 서버와 통신해 로그인을 시도할 때 게임에 재생하는 애니메이션
    // 현재 프로젝트에서는 "로그인 중입니다" 텍스트와 함께 지연시간 출력
    private IEnumerator LoginProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;
            SetMessage($"로그인 중입니다... {time:F1}");

            yield return null;
        }
    }

}
