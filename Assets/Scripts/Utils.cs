using UnityEngine;
using UnityEngine.SceneManagement;

// 씬 전환과 같은 유틸 메소드를 정의하는 utils 스크립트
// 씬 이름을 열거형으로 정의하고, 현재 제작해 둔 Logo, Login 열거형 상수를 선언
public enum SceneNames
{
    Logo=0, Login, 
}


// utils 클래스를 정적으로 정의
public static class Utils
{
    // GetActiveScnee() 메소드는 SceneManager.GetActiveScene().name으로 현재 씬 이름을 반환한다.
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    // LoadScene() - 매개변수를 문자열, 열거형으로 받을 수 있으며
    // 문자열로 받았을 때 매개변수가 비어있으면 현재 씬을 다시 로드하고,
    // 매개변수가 비어있지 않으면 매개변수에 작성된 문자열 이름 씬을 로드한다.
    public static void LoadScene(string sceneName="")
    {
        if (sceneName == "")
        {
            SceneManager.LoadScene(GetActiveScene());
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    // 열거형으로 받았을 때에는 열거형 변수에 해당하는 씬을 로드한다.
    public static void LoadScene(SceneNames sceneName)
    {
        // SceneNames 열거형으로 매개변수를 받아온 경우 ToString() 처리
        SceneManager.LoadScene(sceneName.ToString());
    }
}
