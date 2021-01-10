using UnityEngine;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject backgroundContainer;
    private LoginView loginView;
    private SignupView signupView;
    private MainView mainView;
    private SettingsView settingsView;


    void Awake() {
        loginView = Factory.Instance.CreateView<LoginView>();
        signupView = Factory.Instance.CreateView<SignupView>();
        mainView = Factory.Instance.CreateView<MainView>();
        settingsView = Factory.Instance.CreateView<SettingsView>();
        
        loginView.transform.SetParent(gameObject.transform, false);



        loginView.loginButton.onClick.AddListener(() => {
            mainView.transform.SetParent(gameObject.transform, false);
            OpenView(mainView);
        });

        loginView.signupButton.onClick.AddListener(() => {
            signupView.transform.SetParent(gameObject.transform, false);
            OpenView(signupView);
        });

        signupView.signupButton.onClick.AddListener(() => {
            OpenView(loginView);
        });

        signupView.loginButton.onClick.AddListener(() => {
            OpenView(loginView);
        });

        mainView.menuButton.onClick.AddListener(() => {
            settingsView.transform.SetParent(gameObject.transform, false);
            OpenView(settingsView);
        });

        settingsView.logoutButton.onClick.AddListener(() => {
            OpenView(loginView);
        });
    }

    private void OpenView(View view) {
        loginView.gameObject.SetActive(false);
        mainView.gameObject.SetActive(false);
        signupView.gameObject.SetActive(false);
        settingsView.gameObject.SetActive(false);

        view.gameObject.SetActive(true);
    }
}
