using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginViewController : View {
    [SerializeField] private Button loginButton;
    [SerializeField] private Button signupButton;
    [SerializeField] private Button forgotPasswordButton;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    private void Start() {
        loginButton.onClick.AddListener(() => { navigationView.Push(Factory.CreateView<MainViewController>()); });
        signupButton.onClick.AddListener(() => { navigationView.Push(Factory.CreateView<SignupViewController>()); });
    }

    // private void Login() {
        
    // }
}