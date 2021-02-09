using UnityEngine;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject tableTop;
    [SerializeField] private GameObject avatarSpotlight;
    private NavigationView mainNavView;

    void Start() {
        mainNavView = Factory.CreateView<NavigationView>();
        mainNavView.SetRoot(gameObject);
        
        var loginView = Factory.CreateView<LoginView>();

        mainNavView.Push(loginView);

        loginView.loginButton.onClick.AddListener(() => {
            var mainView = Factory.CreateView<MainView>();
            loginView.navigationView.Push(mainView);
        });

        loginView.signupButton.onClick.AddListener(() => {
            var signupView = Factory.CreateView<SignupView>();
            loginView.navigationView.Push(signupView);
        });

        NotificationCenter.Subscribe(this, Notifications.Camera.avatarSpotlightActive, OnActiveAvatarSpotlight);
        NotificationCenter.Subscribe(this, Notifications.Camera.tableTopActive, OnActiveTableTop);
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
