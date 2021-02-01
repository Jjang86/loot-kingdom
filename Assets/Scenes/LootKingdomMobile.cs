using UnityEngine;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject backgroundContainer;

    private LoginView loginView;

    void Awake() {
        loginView = Factory.Instance.CreateView<LoginView>();   
        loginView.transform.SetParent(gameObject.transform, false);

        loginView.loginButton.onClick.AddListener(() => {
            var mainView = Factory.Instance.CreateView<MainViewController>();
            mainView.transform.SetParent(gameObject.transform, false);
            loginView.gameObject.SetActive(false);
        });

        loginView.signupButton.onClick.AddListener(() => {
            var signupView = Factory.Instance.CreateView<SignupView>();
            signupView.transform.SetParent(gameObject.transform, false);
        });

        NotificationCenter.Subscribe(this, Notifications.UI.logout, OnLogout);
    }

    public void OnLogout() {
        loginView.gameObject.SetActive(true);
    }
}
