using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class LoginScript : MonoBehaviour
{
    public InputField loginInputField;
    public InputField passwordInputField;
    [SerializeField] string NextScene;
    private string apiUrl = "http://217.71.129.139:4320/api/log.php?login={0}&password={1}";

    public void OnLoginButtonClick()
    {
        string login = loginInputField.text;
        string password = passwordInputField.text;
        StartCoroutine(Login(login, password));
    }

    [System.Serializable]
    public class User
    {
        public int id;
        public string login;
        public string token;
    }
    [System.Serializable]
    public class UserData
    {
        public User user;
    }
    IEnumerator Login(string login, string password)
    {
        string url = string.Format(apiUrl, login, password);
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                UserData ud = JsonUtility.FromJson<UserData>(www.downloadHandler.text);
                User user = ud.user;

                if (user != null && user.id != 0) 
                {
                    PlayerPrefs.SetInt("UserID", user.id);
                    PlayerPrefs.SetString("UserLogin", user.login);
                    PlayerPrefs.SetString("UserToken", user.token);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
                }
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }
}