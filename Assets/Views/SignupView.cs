using UnityEngine;
using UnityEngine.UI;

public class SignupView : View {
    public Button signupButton;
    public Button loginButton;

    void Awake() {
        signupButton.onClick.AddListener(() => {
            Destroy(gameObject);
        });

        loginButton.onClick.AddListener(() => {
            Destroy(gameObject);
        });
    }
}