using UnityEngine;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject tableTop;
    [SerializeField] private GameObject avatarSpotlight;
    private NavigationView mainNavView;

    void Start() {
        LootKingdomDirector.user = new User() {
            user_id = "1",
            email = "jeff.jang86@gmail.com",
            first_name = "jeff",
            last_name = "jang",
            user_name = "jjang",
            gold = 1000,
            diamonds = 250,
            experience = 0
        };

        CurrencyManager.Instance.gold = LootKingdomDirector.user.gold;
        CurrencyManager.Instance.diamonds = LootKingdomDirector.user.diamonds;
        ExperienceManager.Instance.SetCurrentLevel();
        Debug.Log(ExperienceManager.Instance.currentLevel);
        Debug.Log(ExperienceManager.Instance.SetNextLevelThreshold());

        mainNavView = Factory.CreateView<NavigationView>();
        mainNavView.SetRoot(gameObject);
        mainNavView.Push(Factory.CreateView<LoginViewController>());

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

    private void OnApplicationQuit() {
        Debug.Log($"gold: {LootKingdomDirector.user.gold}");
        Debug.Log($"diamonds: {LootKingdomDirector.user.diamonds}");
        Debug.Log($"experience: {LootKingdomDirector.user.experience}");
    }
}
