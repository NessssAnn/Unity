using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public class UserData
{
    public Player playerData;
    public Error error;
}


[System.Serializable]
public class Error
{
    public string errorText;
    public bool isError;
}


[System.Serializable]
public class Player
{
    public string nickname;
    public string colorMain;
    public string colorSecond;

    public Player (string nick, string c1, string c2)
    {
        nickname = nick;
        colorMain = c1;
        colorSecond = c2;
    }

    public void SetMainColor(string color) => colorMain = color;
    public void SetSecondColor(string color) => colorSecond = color;
    public void SetNickname(string name) => nickname = name;
}

public class WebManager : MonoBehaviour
{
    public UserData userData = new UserData ();
    [SerializeField] private string targetURL;

    public string GetUserData(UserData data)
    {
        return JsonUtility.ToJson(data);
    }

    public UserData SetUserData(string data)
    {
        return JsonUtility.FromJson<UserData>(data);
    }

    private void Start()
    {
        userData.error = new Error() { errorText = "text", isError = true };
        userData.playerData = new Player("Ann", "wow", "not wow");
    }



    public void Login(string login, string pasword)
    {
        StopAllCoroutines();
        Logging(login, pasword);
    }

    public void Registration(string login, string password, string password2, string nickname)
    {
        StopAllCoroutines();
        Registering(login, password, password2, nickname);
    }

    public void  Logging(string login, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "logging");
        form.AddField("login", login);
        form.AddField("password", password);
        StartCoroutine(SendData(form));
    }

    public void Registering(string login, string password1, string password2, string nickname)
    {
        WWWForm form = new WWWForm();
        form.AddField("type", "register");
        form.AddField("login", login);
        form.AddField("password1", password1);
        form.AddField("password2", password2);
        form.AddField("nickname", nickname);
        StartCoroutine(SendData(form));
    }

    IEnumerator SendData (WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(targetURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                userData = SetUserData(www.downloadHandler.text);
            }
        }
    }
}
