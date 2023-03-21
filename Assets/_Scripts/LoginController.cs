using System.Collections;
using System.Collections.Generic;
using _Scripts.Network;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginController : MonoBehaviour {

    public Button LoginButton;
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;

    void Start() {

        LoginButton.onClick.AddListener(Login);
        UsernameInput.text = "dalcikferhateren@ogr.iu.edu.tr";
        PasswordInput.text = "testUser";
    }

    async void Login()
    {
        if (await RealmManager.Instance.Login(UsernameInput.text, PasswordInput.text) != "")
        {
            print("login ");
            SceneManager.LoadScene("SampleScene");

        }
    }

    void Update() {
        if(Input.GetKey("escape")) {
            Application.Quit();
        }
    }

}
