using UnityEngine;
using BackEnd; // �ڳ� SDK

// TIP
// �ڳ��� ��� ����� Backend Ŭ���� ���ο� �̱��� �ν��Ͻ��� ����Ǿ� �����Ƿ�
// �ʱ�ȭ ���Ŀ��� Backend.{�ʿ��� ��� Ŭ����}�� ���� �����ؼ� ��� ������

// �ڳ� Backend Ŭ������ ���캸��, ���� ���� ����/���/��Ʈ �� �ڳ����� �����ϴ� ��� ����� ���� ������ �����ϰ� �ִ�.

public class BackendManager : MonoBehaviour
{
    private void Awake()
    {
        // Update() �޼ҵ��� Backend.AsyncPoll(); ȣ���� ���� ������Ʈ�� �ı����� �ʴ´�
        // AsyncPoll() �޼ҵ带 ȣ���ؾ� ���� ť�� ����� �ݹ� �Լ��� ȣ���� �� �ֱ� ������
        // ��� ������ �������� �ʰ� ��� �����ֵ��� DontDestoryOnLand(gameObject) 
        DontDestroyOnLoad(gameObject);

        // �ڳ� ���� �ʱ�ȭ
        BackendSetup();
    }

    private void BackendSetup()
    {
        // �ڳ� �ʱ�ȭ
        var bro = Backend.Initialize(true);

        // �ڳ� �ʱ�ȭ�� ���� ����
        if (bro.IsSuccess())
        {
            // �ʱ�ȭ ���� �� statusCode 204 Success
            Debug.Log($"�ڳ� ���� �ʱ�ȭ ���� - {bro}");
        }
        else
        {
            // �ʱ�ȭ ���� �� 400���� ���� �߻�
            Debug.LogError($"�ʱ�ȭ ���� : {bro}");
        }
    }

    private void Update()
    {
        // ������ �񵿱� �޼ҵ� ȣ��(�ݹ� �Լ� Ǯ��)�� ���� �ۼ�
        // Update �޼ҵ忡�� Backend�� �ʱ�ȭ�� �Ϸ�� ���ĺ��� Backend.AsyncPoll(); �޼ҵ带 ȣ���Ѵ�.
        if (Backend.IsInitialized)
        {
            Backend.AsyncPoll();
        }
    }
}
