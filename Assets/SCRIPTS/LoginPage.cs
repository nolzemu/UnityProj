using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LogoPage : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Text errorText;

    public string databaseURL = "http://217.71.129.139:4320/api/log.php"; // URL ��� �������� ��������������

    public void OnLoginButtonClicked()
    {
        StartCoroutine(Login(usernameField.text, passwordField.text));
    }

    IEnumerator Login(string username, string password)
    {
        string url = $"{databaseURL}?username={username}&password={password}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            errorText.text = "������ ����������� � �������.";
        }
        else
        {
            if (www.downloadHandler.text == "Success")
            {
                Debug.Log("�������������� ������ �������!");
                // ����� ���������� �� ��������� �����
            }
            else
            {
                Debug.Log("�������� ����� ��� ������.");
                errorText.text = "�������� ����� ��� ������.";
            }
        }
    }
}
