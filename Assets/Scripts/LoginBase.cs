using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 게임 유저 관리에서 사용하는 UI 초기화, 메시지 내용 설정 등을 제어하는
// LoginBase 스크립트

public class LoginBase : MonoBehaviour
{
    // 게임 화면에 메시지를 출력할 때 사용하는 textMessage 변수 선언
    [SerializeField]
    private TextMeshProUGUI textMessage;

    // 

    // 필드 내용이 잘못 입력되었을 때 -> 필드 색상을 변경한다
    // 메시지 내용을 비우고, 변경된 필드 색상을 초기화하는 ResetUI() 메소드 정의한다.
    // 필드가 여러 개 일수도 있기 때문에 params 키워드를 사용해 가변길이 매개변수로 설정한다.
    protected void ResetUI(params Image[] images)
    {
        // textMessage에 출력하는 내용을 비워주고
        // 반복문을 호출하여 매개변수로 넘어온 모든 필드 이미지를 흰색으로 설정
        textMessage.text = string.Empty;

        for ( int i = 0; i < images.Length; ++i)
        {
            images[i].color = Color.white;
        }
    }


    // 매개변수에 있는 내용을 출력한다.
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
           
    }

    // 입력 오류가 있는 InputField에 대하여 적용
    // 매개변수로 받아온 필드의 색상을 빨간색으로 설정하고
    // msg 문자열을 textMessage에 출력한다.
    protected void GuideForincorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;

    }

    // 필드 값이 비어있는지 확인
    // 입력 필드에 내용 미입력 시 해당 필드 색상을 빨간색으로 설정
    // textMessage에 "result 필드를 채워주세요" 텍스트 출력
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals(""))
        {
            GuideForincorrectlyEnteredData(image, $"\"{result}\" 필드를 채워주세요.");
            return true;
        }

        return false;
    }
}
