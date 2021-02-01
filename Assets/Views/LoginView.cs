using UnityEngine;
using UnityEngine.UI;

public class LoginView : View {
    public Button loginButton;
    public Button signupButton;

    
    void Awake() {
        signupButton.onClick.AddListener(() => {
            var signupView = Factory.CreateView<SignupView>();
            signupView.transform.SetParent(gameObject.transform, false);
        });
    }
}