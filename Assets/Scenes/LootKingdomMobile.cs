using UnityEngine;
using DG.Tweening;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject backgroundContainer;

    private LoginView loginView;
    private MainView mainView;
    private MenuView menuView;

    private enum Menu {
        Closed = 0,
        Open = -100
    }

    private bool _menuOpen;
    private bool menuOpen {
        get => _menuOpen;
        set {
            _menuOpen = value;
            var pos = mainView.menuButton.transform.position;
            if (value) { mainView.menuButton.GetComponent<RectTransform>().DOAnchorPosX((int)Menu.Open, 0.3f); }
            else { mainView.menuButton.GetComponent<RectTransform>().DOAnchorPosX((int)Menu.Closed, 0.3f); }
        }
    }


    void Awake() {
        loginView = Factory.Instance.CreateView<LoginView>();
        mainView = Factory.Instance.CreateView<MainView>();
        menuView = Factory.Instance.CreateView<MenuView>();

        mainView.transform.SetParent(gameObject.transform, false);        
        loginView.transform.SetParent(gameObject.transform, false);

        loginView.loginButton.onClick.AddListener(() => {
            ShowView(mainView);
        });

        mainView.menuButton.onClick.AddListener(() => {
            menuOpen = !menuOpen;
            // menuView.transform.SetParent(gameObject.transform, false);
            // ShowView(menuView);
        });

        mainView.profileButton.onClick.AddListener(() => {
            var inventoryView = Factory.Instance.CreateView<InventoryView>();
            inventoryView.transform.SetParent(gameObject.transform, false);
            ShowView(inventoryView);
        });

        menuView.logoutButton.onClick.AddListener(() => {
            ShowView(loginView);
        });

        menuView.closeButton.onClick.AddListener(() => {
            ShowView(mainView);
        });
    }

    private void ShowView(View view) {
        loginView.gameObject.SetActive(false);
        view.gameObject.SetActive(true);
    }
}
