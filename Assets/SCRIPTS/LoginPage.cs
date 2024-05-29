using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class LogoPage : MonoBehaviour
{
    public InputField usernameField;
    public InputField passwordField;
    public Text errorText;

    public string databaseURL = "http://217.71.129.139:4320/api/log.php"; // URL для проверки аутентификации

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
            errorText.text = "Ошибка подключения к серверу.";
        }
        else
        {
            if (www.downloadHandler.text == "Success")
            {
                Debug.Log("Аутентификация прошла успешно!");
                // Здесь переходите на следующую сцену
            }
            else
            {
                Debug.Log("Неверный логин или пароль.");
                errorText.text = "Неверный логин или пароль.";
            }
        }
    }
}
