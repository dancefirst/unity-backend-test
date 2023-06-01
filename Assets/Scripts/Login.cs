using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BackEnd;


public class Login : LoginBase
{
    // �α��ο� ����ϴ� ID �ʵ�, PW �ʵ�, �α��� ��ư UI ���� ����
    [SerializeField]
    private Image imageID; // ID �ʵ� ���� ����
    [SerializeField]
    private TMP_InputField inputFieldID; // ID �ʵ� �ؽ�Ʈ ���� ����
    [SerializeField]
    private Image imagePW; // PW �ʵ� ���� ����
    [SerializeField]
    private TMP_InputField inputFieldPW; // PW �ʵ� �ؽ�Ʈ ���� ����
    [SerializeField]
    private Button btnLogin; // �α��� ��ư (��ȣ�ۿ� ����/�Ұ���)

    public void OnClickLogin()
    {
        // �α��� ��ư Ŭ�� �� ȣ��
        // �Ű������� �Է��� InputField UI ����� Message ���� �ʱ�ȭ
        ResetUI(imageID, imagePW);

        // �ʵ� ���� ����ִ��� üũ
        if (IsFieldDataEmpty(imageID, inputFieldID.text, "���̵�")) return;
        if (IsFieldDataEmpty(imagePW, inputFieldPW.text, "��й�ȣ")) return;

        // �α��� ��ư�� ��Ÿ���� ���ϵ��� ��ư ��ȣ�ۿ� ��Ȱ��ȭ
        btnLogin.interactable = false;

        // ������ �α����� ��û�ϴ� ���� ȭ�鿡 ����ϴ� ���� ������Ʈ
        // ex) �α��� ���� �ؽ�Ʈ ���, ��Ϲ��� ������ ȸ�� ��
        StartCoroutine(nameof(LoginProcess));

        // ������ ����� �α����� �õ��ϰ�, ����/���� ó���ϴ� ResponseToLogin() �޼ҵ� ȣ��
        ResponseToLogin(inputFieldID.text, inputFieldPW.text);
    }

    // �α��� �õ� �� �����κ��� ���޹��� message�� ������� ���� ó��
    private void ResponseToLogin(string ID, string PW)
    {
        // ������ �α��� ��û
        Backend.BMember.CustomLogin(ID, PW, callback =>
        {
        StopCoroutine(nameof(LoginProcess));

        // �α��� ����
        if (callback.IsSuccess())
        {
            SetMessage($"{inputFieldID.text}�� ȯ���մϴ�.");
        }
        // �α��� ����
        else
        {
            // �α��ο� �������� ���� �ٽ� �α����� �ؾ��ϱ� ������ "�α���" ��ư ��ȣ�ۿ� Ȱ��ȭ�Ѵ�.
            btnLogin.interactable = true;

            string message = string.Empty;
            switch (int.Parse(callback.GetStatusCode()))
            {
                    case 401: // �������� �ʴ� ���̵�, �߸��� ��й�ȣ
                        message = callback.GetMessage().Contains("customId") ? $"�������� �ʴ� ���̵��Դϴ�.\nStatus code {callback.GetStatusCode()}\n{callback.GetMessage()}" : "�߸��� ��й�ȣ �Դϴ�.";
                        Debug.Log($"Server message -> {callback.GetMessage()}");
                        break;
                    case 403: // ���� or ����̽� ����
                        message = callback.GetMessage().Contains("user") ? "���ܴ��� �����Դϴ�." : "���ܴ��� ����̽��Դϴ�.";
                        break;
                    case 410: // Ż�� ���� ��
                        message = "Ż�� ���� ���� �����Դϴ�.";
                        break;
                    default:
                        message = callback.GetMessage();
                        break;
            }

            // StatusCode 401���� "�߸��� ��й�ȣ �Դϴ�." �� ��
            if (message.Contains("��й�ȣ"))
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

    // ������ ����� �α����� �õ��� �� ���ӿ� ����ϴ� �ִϸ��̼�
    // ���� ������Ʈ������ "�α��� ���Դϴ�" �ؽ�Ʈ�� �Բ� �����ð� ���
    private IEnumerator LoginProcess()
    {
        float time = 0;

        while (true)
        {
            time += Time.deltaTime;
            SetMessage($"�α��� ���Դϴ�... {time:F1}");

            yield return null;
        }
    }

}
