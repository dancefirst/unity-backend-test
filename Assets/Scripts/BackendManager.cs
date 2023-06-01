using UnityEngine;
using BackEnd; // 뒤끝 SDK

// TIP
// 뒤끝의 모든 기능은 Backend 클래스 내부에 싱글톤 인스턴스로 선언되어 있으므로
// 초기화 이후에는 Backend.{필요한 기능 클래스}와 같이 접근해서 사용 가능함

// 뒤끝 Backend 클래스를 살펴보면, 게임 유저 관리/길드/차트 등 뒤끝에서 제공하는 모든 기능을 정적 변수로 제공하고 있다.

public class BackendManager : MonoBehaviour
{
    private void Awake()
    {
        // Update() 메소드의 Backend.AsyncPoll(); 호출을 위해 오브젝트를 파괴하지 않는다
        // AsyncPoll() 메소드를 호출해야 별도 큐에 저장된 콜백 함수를 호출할 수 있기 때문에
        // 모든 씬에서 삭제되지 않고 계속 남아있도록 DontDestoryOnLand(gameObject) 
        DontDestroyOnLoad(gameObject);

        // 뒤끝 서버 초기화
        BackendSetup();
    }

    private void BackendSetup()
    {
        // 뒤끝 초기화
        var bro = Backend.Initialize(true);

        // 뒤끝 초기화에 대한 응답
        if (bro.IsSuccess())
        {
            // 초기화 성공 시 statusCode 204 Success
            Debug.Log($"뒤끝 서버 초기화 성공 - {bro}");
        }
        else
        {
            // 초기화 실패 시 400번대 에러 발생
            Debug.LogError($"초기화 실패 : {bro}");
        }
    }

    private void Update()
    {
        // 서버의 비동기 메소드 호출(콜백 함수 풀링)을 위해 작성
        // Update 메소드에서 Backend의 초기화가 완료된 직후부터 Backend.AsyncPoll(); 메소드를 호출한다.
        if (Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }
}
