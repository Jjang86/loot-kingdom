using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainViewController : View, ITimerDelegate {
    [SerializeField] private RectTransform menuRect;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button diceButton;
    [SerializeField] private TextMeshProUGUI diceRoll;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI diamond;
    [SerializeField] private TextMeshProUGUI roll;
    [SerializeField] private TextMeshProUGUI timeText;

    private PlayerPieceView playerPiece;
    private Board boardView;
    private Dice dice;
    private bool tileAnimating = false;
    private List<TileView> tiles;
    private int numTiles;

    private enum MenuPosition {
        Closed = 100,
        Open = 0
    }
    
    private bool _menuOpen = false;
    private bool menuOpen {
        get => _menuOpen;
        set {
            _menuOpen = value;
            var pos = menuButton.transform.position;
            if (value) { menuRect.DOAnchorPosX((int)MenuPosition.Open, 0.3f); }
            else { menuRect.DOAnchorPosX((int)MenuPosition.Closed, 0.3f); }
        }
    }

    private void OnEnable() {
        NotificationCenter.Subscribe(this, Notifications.Currency.numRollsChanged, value => { roll.text = value.ToString(); });
        NotificationCenter.Subscribe(this, Notifications.Currency.goldChanged, value => { gold.text = value.ToString(); });
        NotificationCenter.Subscribe(this, Notifications.Currency.diamondChanged, value => { diamond.text = value.ToString(); });
        NotificationCenter.Subscribe(this, Notifications.Dice.animationComplete, async rollAmount => {
            diceRoll.text = $"Rolled a {rollAmount.ToString()}!";
            await playerPiece.MoveMultipleTiles((int)(rollAmount), tiles);
            if (CurrencyManager.Instance.numRolls > 0) { diceButton.interactable = true; }
            tileAnimating = false;
        });
        CurrencyManager.Instance.timerDelegate = this;
    }

    private void OnDisable() {
        NotificationCenter.Unsubscribe(this, Notifications.Currency.numRollsChanged);
        NotificationCenter.Unsubscribe(this, Notifications.Currency.goldChanged);
        NotificationCenter.Unsubscribe(this, Notifications.Currency.diamondChanged);
    }

    private void Update() {
        if (!tileAnimating && CurrencyManager.Instance.numRolls > 0) { diceButton.interactable = true; }
    }

    private void Start() {
        playerPiece = Factory.CreateView<PlayerPieceView>();
        boardView = Factory.CreateView<Board4>();
        dice = Factory.CreateView<Dice>();
        tiles = boardView.tiles;
        numTiles = tiles.Count;
        
        TableTop.PlacePieceOnBoard(playerPiece, tiles[playerPiece.currentTileIndex].position);
        TableTop.SetBoard(boardView);

        gold.text = CurrencyManager.Instance.gold.ToString();
        diamond.text = CurrencyManager.Instance.diamonds.ToString();
        roll.text = CurrencyManager.Instance.numRolls.ToString();

        menuButton.onClick.AddListener(() => { menuOpen = !menuOpen; });
        diceButton.onClick.AddListener(() => { RollDice(); });
        logoutButton.onClick.AddListener(() => { navigationView.Pop(); });
        profileButton.onClick.AddListener(() => { navigationView.Push(Factory.CreateView<InventoryViewController>()); });
    }

    private void RollDice() {
        tileAnimating = true;
        CurrencyManager.Instance.numRolls--;
        diceButton.interactable = false;
        dice.Roll();
    }
    
    private float GetRandomEulerAngle() { return Random.Range(0, 360); }
    public void OnShowTimer() { timeText.gameObject.SetActive(true); }
    public void OnHideTimer() { timeText.gameObject.SetActive(false); }
    public void OnSetTimerText(string time) { timeText.text = time; }
    private void OnDestroy() { TableTop.ClearBoard(); }
}