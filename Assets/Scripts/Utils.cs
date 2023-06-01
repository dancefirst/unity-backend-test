using UnityEngine;
using UnityEngine.SceneManagement;

// �� ��ȯ�� ���� ��ƿ �޼ҵ带 �����ϴ� utils ��ũ��Ʈ
// �� �̸��� ���������� �����ϰ�, ���� ������ �� Logo, Login ������ ����� ����
public enum SceneNames
{
    Logo=0, Login, 
}


// utils Ŭ������ �������� ����
public static class Utils
{
    // GetActiveScnee() �޼ҵ�� SceneManager.GetActiveScene().name���� ���� �� �̸��� ��ȯ�Ѵ�.
    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    // LoadScene() - �Ű������� ���ڿ�, ���������� ���� �� ������
    // ���ڿ��� �޾��� �� �Ű������� ��������� ���� ���� �ٽ� �ε��ϰ�,
    // �Ű������� ������� ������ �Ű������� �ۼ��� ���ڿ� �̸� ���� �ε��Ѵ�.
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

    // ���������� �޾��� ������ ������ ������ �ش��ϴ� ���� �ε��Ѵ�.
    public static void LoadScene(SceneNames sceneName)
    {
        // SceneNames ���������� �Ű������� �޾ƿ� ��� ToString() ó��
        SceneManager.LoadScene(sceneName.ToString());
    }
}
