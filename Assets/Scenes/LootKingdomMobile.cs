using UnityEngine;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject tableTop;
    [SerializeField] private GameObject avatarSpotlight;
    private LoginView loginView;

    void Start() {
        loginView = Factory.CreateView<LoginView>();   
        loginView.transform.SetParent(gameObject.transform, false);

        loginView.loginButton.onClick.AddListener(() => {
            var mainView = Factory.CreateView<MainViewController>();
            mainView.transform.SetParent(gameObject.transform, false);
            loginView.gameObject.SetActive(false);
        });

        loginView.signupButton.onClick.AddListener(() => {
            var signupView = Factory.CreateView<SignupView>();
            signupView.transform.SetParent(gameObject.transform, false);
        });

        NotificationCenter.Subscribe(this, Notifications.UI.logout, OnLogout);
        NotificationCenter.Subscribe(this, Notifications.Camera.avatarSpotlightActive, OnActiveAvatarSpotlight);
        NotificationCenter.Subscribe(this, Notifications.Camera.tableTopActive, OnActiveTableTop);
    }

    private void OnLogout() {
        loginView.gameObject.SetActive(true);
    }

    private void OnActiveAvatarSpotlight() {
        tableTop.SetActive(false);
        avatarSpotlight.SetActive(true);
    }

    private void OnActiveTableTop() {
        avatarSpotlight.SetActive(false);
        tableTop.SetActive(true);
    }
}
