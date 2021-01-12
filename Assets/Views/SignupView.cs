using UnityEngine;
using UnityEngine.UI;

public class SignupView : View {
    [SerializeField] private Button signupButton;
    [SerializeField] private Button loginButton;

    void Awake() {
        signupButton.onClick.AddListener(() => {
            Destroy(gameObject);
        });

        loginButton.onClick.AddListener(() => {
            Destroy(gameObject);
        });
    }
}