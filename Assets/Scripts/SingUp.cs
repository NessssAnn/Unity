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
    [SerializeField] GameObject For�onfectionerPanel;
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
        
        if (www.text == "��������")
        {
            Message.text = www.text;
            FirstConf();
        }
        else if (www.text == "����������")
        {
            Message.text = www.text;
            FirstUser();
        }
        else if (www.text == "�������������")
        {
            Message.text = www.text;
            FirstAdmin();
        }
        else if (www.text == "wrong")
        {
            Message.text = "������������ ������";
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
        StartCoroutine(NoteUser("����� � ���� �������"));
    }

    public void FirstAdmin()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("������������� ����� � ���� �������"));
    }

    public void FirstConf()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("�������� ����� � ���� �������"));
    }

    public void Purchase()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("����� ����"));
    }

    public void LogOut()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("����� �� ��������"));
    }

    public void ComeBack()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("�������� � ��������"));
    }

    public void Sale()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("������ ����"));
    }

    public void ComeBackConf()
    {
        StopAllCoroutines();
        StartCoroutine(NoteUser("�������� � ��������"));
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
            if (activity == "����� ����") OpenPurchase();
            else if (activity == "����� �� ��������") OpenSingUp();
            else if (activity == "�������� � ��������") OpenUser();
            else if (activity == "����� � ���� �������") OpenUser();
            else if (activity == "�������� ����� � ���� �������") OpenConf();
            else if (activity == "������ ����") OpenSalling();
            else if (activity == "�������� � ��������") OpenConf();
            else if (activity == "������������� ����� � ���� �������") OpenAdmin();
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
        For�onfectionerPanel.SetActive(false);
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
        For�onfectionerPanel.SetActive(true);
    }

    public void OpenPurchase()
    {
        ForUsersPanel.SetActive(false);
        PurchasePanel.SetActive(true);
    }

    public void OpenSalling()
    {
        For�onfectionerPanel.SetActive(false);
        SallingPanel.SetActive(true);
    }

}
