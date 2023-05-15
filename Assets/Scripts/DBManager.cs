using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DBManager : MonoBehaviour
{
    [SerializeField] GameObject SingUpPanel;
    [SerializeField] GameObject RegisterPanel;

    public string output;

    public TMP_Text Message;

    [SerializeField] TMP_Text userName;
    [SerializeField] TMP_Text passwordone;
    [SerializeField] TMP_Text passwordtwo;
    [SerializeField] TMP_Text userLog;

    public string user;

    public void Reg()
    {
        StartCoroutine(RegisterUser());
    }

    private IEnumerator RegisterUser()
    {
        user = userLog.text;
        string name = userName.text;
        string login = userLog.text;
        string pasone = passwordone.text;
        string pastwo = passwordtwo.text;
        WWWForm form = new WWWForm();
        form.AddField("name", name);
        form.AddField("passone", pasone);
        form.AddField("passtwo", pastwo);
        form.AddField("login", login);
        form.AddField("role", output);
        WWW www = new WWW("http://localhost/reg_unity/register_user.php", form);
        yield return www;
        if (www.text == "ok")
        {
            FirstUser();
        }
        else if (www.text == "existing")
        {
            Message.text = "Такой аккаунт уже существует";
        }
        else if (www.text == "unknown")
        {
            Message.text = "Выберите роль";
        }
        else if (www.text == "different")
        {
            Message.text = "Пароли не совпадают";
        }
        else if (www.text == "empty")
        {
            Message.text = "Заполните все поля";
        }
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
    }

    public void FirstUser()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("зарегистрировался"));
    }

    private IEnumerator NoteUser(string text)
    {
        string login = user;
        string activity = text;
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("activity", activity);
        WWW www = new WWW("http://localhost/reg_unity/notes.php", form);
        yield return www;
        Debug.Log(login + "   " + activity);
        if (www.text == "ok")
        {
            if (activity == "зарегистрировался") OpenSingUp();
        }
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
        Debug.Log("Answer:" + www.text);
    }

    public void OpenSingUp()
    {
        RegisterPanel.SetActive(false);
        SingUpPanel.SetActive(true);
    }

    public void Drop(int val)
    {
        if (val == 0)
        {
            output = "";
        }
        if (val == 1)
        {
            output = "Покупатель";
        }
        if (val == 2)
        {
            output = "Кондитер";
        }
    }
}
