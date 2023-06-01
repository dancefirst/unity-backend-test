using UnityEngine;
using UnityEngine.UI;
using TMPro;

// ���� ���� �������� ����ϴ� UI �ʱ�ȭ, �޽��� ���� ���� ���� �����ϴ�
// LoginBase ��ũ��Ʈ

public class LoginBase : MonoBehaviour
{
    // ���� ȭ�鿡 �޽����� ����� �� ����ϴ� textMessage ���� ����
    [SerializeField]
    private TextMeshProUGUI textMessage;

    // 

    // �ʵ� ������ �߸� �ԷµǾ��� �� -> �ʵ� ������ �����Ѵ�
    // �޽��� ������ ����, ����� �ʵ� ������ �ʱ�ȭ�ϴ� ResetUI() �޼ҵ� �����Ѵ�.
    // �ʵ尡 ���� �� �ϼ��� �ֱ� ������ params Ű���带 ����� �������� �Ű������� �����Ѵ�.
    protected void ResetUI(params Image[] images)
    {
        // textMessage�� ����ϴ� ������ ����ְ�
        // �ݺ����� ȣ���Ͽ� �Ű������� �Ѿ�� ��� �ʵ� �̹����� ������� ����
        textMessage.text = string.Empty;

        for ( int i = 0; i < images.Length; ++i)
        {
            images[i].color = Color.white;
        }
    }


    // �Ű������� �ִ� ������ ����Ѵ�.
    protected void SetMessage(string msg)
    {
        textMessage.text = msg;
           
    }

    // �Է� ������ �ִ� InputField�� ���Ͽ� ����
    // �Ű������� �޾ƿ� �ʵ��� ������ ���������� �����ϰ�
    // msg ���ڿ��� textMessage�� ����Ѵ�.
    protected void GuideForincorrectlyEnteredData(Image image, string msg)
    {
        textMessage.text = msg;
        image.color = Color.red;

    }

    // �ʵ� ���� ����ִ��� Ȯ��
    // �Է� �ʵ忡 ���� ���Է� �� �ش� �ʵ� ������ ���������� ����
    // textMessage�� "result �ʵ带 ä���ּ���" �ؽ�Ʈ ���
    protected bool IsFieldDataEmpty(Image image, string field, string result)
    {
        if (field.Trim().Equals(""))
        {
            GuideForincorrectlyEnteredData(image, $"\"{result}\" �ʵ带 ä���ּ���.");
            return true;
        }

        return false;
    }
}
