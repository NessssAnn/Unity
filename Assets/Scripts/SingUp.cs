using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SingUp : MonoBehaviour
{

    [SerializeField] GameObject SingUpPanel;
    [SerializeField] GameObject RegisterPanel;
    [SerializeField] GameObject ForUsersPanel;
    [SerializeField] GameObject ForСonfectionerPanel;
    [SerializeField] GameObject PurchasePanel;
    [SerializeField] GameObject SallingPanel;
    [SerializeField] GameObject AdminPanel;

    public TMP_Text Message;

    [SerializeField] TMP_Text password;
    [SerializeField] TMP_Text userLog;

    public string user;

    public void Log()
    {
        StartCoroutine(LoginUser());
    }

    private IEnumerator LoginUser()
    {
        user = userLog.text;
        string login = userLog.text;
        string pas = password.text;
        WWWForm form = new WWWForm();
        form.AddField("login", login);
        form.AddField("pass", pas);
        WWW www = new WWW("http://localhost/reg_unity/login_user.php", form);
        yield return www;
        
        if (www.text == "Кондитер")
        {
            Message.text = www.text;
            FirstConf();
        }
        else if (www.text == "Покупатель")
        {
            Message.text = www.text;
            FirstUser();
        }
        else if (www.text == "Администратор")
        {
            Message.text = www.text;
            FirstAdmin();
        }
        else if (www.text == "wrong")
        {
            Message.text = "Неправильные данные";
        }
        if (www.error != null)
        {
            Debug.Log(www.error);
            yield break;
        }
        Debug.Log("Answer:" + www.text);
    }

    public void FirstUser()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("зашел в свой аккаунт"));
    }

    public void FirstAdmin()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("администратор зашел в свой аккаунт"));
    }

    public void FirstConf()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("кондитер зашел в свой аккаунт"));
    }

    public void Purchase()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("купил торт"));
    }

    public void LogOut()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("вышел из аккаунта"));
    }

    public void ComeBack()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("вернулся к покупкам"));
    }

    public void Sale()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("продал торт"));
    }

    public void ComeBackConf()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("вернулся к продажам"));
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
        Debug.Log(login+"   "+activity);
        if (www.text == "ok")
        {
            if (activity == "купил торт") OpenPurchase();
            else if (activity == "вышел из аккаунта") OpenSingUp();
            else if (activity == "вернулся к покупкам") OpenUser();
            else if (activity == "зашел в свой аккаунт") OpenUser();
            else if (activity == "кондитер зашел в свой аккаунт") OpenConf();
            else if (activity == "продал торт") OpenSalling();
            else if (activity == "вернулся к продажам") OpenConf();
            else if (activity == "администратор зашел в свой аккаунт") OpenAdmin();
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
        ForUsersPanel.SetActive(false);
        ForСonfectionerPanel.SetActive(false);
        SingUpPanel.SetActive(true);
    }

    public void OpenRegister()
    {
        SingUpPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }

    public void OpenUser()
    {
        SingUpPanel.SetActive(false);
        PurchasePanel.SetActive(false);
        ForUsersPanel.SetActive(true);
    }

    public void OpenAdmin()
    {
        SingUpPanel.SetActive(false);
        AdminPanel.SetActive(true);
    }

    public void OpenConf()
    {
        SingUpPanel.SetActive(false);
        SallingPanel.SetActive(false);
        ForСonfectionerPanel.SetActive(true);
    }

    public void OpenPurchase()
    {
        ForUsersPanel.SetActive(false);
        PurchasePanel.SetActive(true);
    }

    public void OpenSalling()
    {
        ForСonfectionerPanel.SetActive(false);
        SallingPanel.SetActive(true);
    }

}
