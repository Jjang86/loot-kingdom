using UnityEngine;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject backgroundContainer;
    private float timer;
    private float delayAmount = 0.01f;

    private LoginView loginView;

    void Awake() {
        loginView = Factory.Instance.CreateView<LoginView>();   
        loginView.transform.SetParent(gameObject.transform, false);

        loginView.loginButton.onClick.AddListener(() => {
            var mainView = Factory.Instance.CreateView<MainView>();
            mainView.transform.SetParent(gameObject.transform, false);
            Destroy(loginView.gameObject);
        });

        loginView.signupButton.onClick.AddListener(() => {
            var signupView = Factory.Instance.CreateView<SignupView>();
            signupView.transform.SetParent(gameObject.transform, false);
        });
    }

    private void Update() {
        if (CurrencyManager.gold < CurrencyManager.goldEndAmount) {
            timer += Time.deltaTime;

            if (timer >= delayAmount) {
                timer = 0f;
                CurrencyManager.gold++;
            }
        }
    }
}
