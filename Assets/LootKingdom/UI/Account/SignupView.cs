using UnityEngine;
using UnityEngine.UI;

public class SignupViewController : View {
    [SerializeField] private Button signupButton;
    [SerializeField] private Button loginButton;

    void Awake() {
        signupButton.onClick.AddListener(() => {
            navigationView.Pop();
        });

        loginButton.onClick.AddListener(() => {
            navigationView.Pop();
        });
    }
}