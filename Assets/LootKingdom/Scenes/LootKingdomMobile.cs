using UnityEngine;

public class LootKingdomMobile : MonoBehaviour {
    [SerializeField] private GameObject tableTop;
    [SerializeField] private GameObject avatarSpotlight;
    private NavigationView mainNavView;

    void Start() {
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
}
