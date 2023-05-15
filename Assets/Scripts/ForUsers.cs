using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ForUsers : MonoBehaviour
{
    [SerializeField] GameObject SingUpPanel;
    [SerializeField] GameObject AdminPanel;


    public TMP_Dropdown dropdown;
    public string user;
    public TMP_Text Message;


    void Start()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        StartCoroutine(Administrate());
    }

    public void Delete()
    {
        user = dropdown.options[dropdown.value].text;
        StartCoroutine(Del(user));
    }

    private IEnumerator Administrate()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        string role = "Администратор";
        WWWForm form = new WWWForm();
        form.AddField("role", role);
        WWW www = new WWW("http://localhost/reg_unity/admin.php", form);
        yield return www;
        List<string> opt = new List<string>();
        var users = www.text.Split(';');
        foreach (var item in users)
        {
            opt.Add(item);
        }
        dropdown.AddOptions(opt);
        dropdown.RefreshShownValue();
    }

    private IEnumerator Del(string text)
    {
        WWWForm form = new WWWForm();
        form.AddField("user", text);
        Debug.Log(text);
        WWW www = new WWW("http://localhost/reg_unity/delete.php", form);
        yield return www;
        if (www.text == "ok")
        {
            Debug.Log(www.text);
            Message.text = "Пользователь " + user + " удален!";
            StartCoroutine(NoteUser("удалил пользователя"));
            StartCoroutine(Administrate());
        }
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
    }

    private IEnumerator NoteUser(string text)
    {
        string login = "Admin";
        string activity = text;
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("activity", activity);
        WWW www = new WWW("http://localhost/reg_unity/notes.php", form);
        yield return www;
        Debug.Log(login + "   " + activity);
        if (www.text == "ok")
        {
            if (activity == "вышел из аккаунта") OpenSingUp();
        }
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
        Debug.Log("Answer:" + www.text);
    }

    public void LogOut()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("вышел из аккаунта"));
    }

    public void OpenSingUp()
    {
        AdminPanel.SetActive(false);
        SingUpPanel.SetActive(true);
    }

}
