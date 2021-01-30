using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainView : View, ITimerDelegate {
    [SerializeField] private RectTransform menuRect;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button diceButton;
    [SerializeField] private TextMeshProUGUI diceRoll;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI diamonds;
    [SerializeField] private TextMeshProUGUI rolls;
    [SerializeField] private TextMeshProUGUI timeText;


    private PlayerPieceView playerPiece;
    private Board boardView;
    private bool tileAnimating = false;
    private List<Tile> tiles;
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
        NotificationCenter.Subscribe(this, Notifications.Currency.numRollsChanged, value => { rolls.text = value.ToString(); });
        NotificationCenter.Subscribe(this, Notifications.Currency.goldChanged, value => { gold.text = value.ToString(); });
        CurrencyManager.Instance.timerDelegate = this;
    }

    private void OnDisable() {
        NotificationCenter.Unsubscribe(this, Notifications.Currency.numRollsChanged);
        NotificationCenter.Unsubscribe(this, Notifications.Currency.goldChanged);
    }

    private void Update() {
        if (!tileAnimating && CurrencyManager.Instance.numRolls > 0) { diceButton.interactable = true; }
    }

    private void Start() {
        playerPiece = Factory.Instance.CreateView<PlayerPieceView>();
        boardView = Factory.Instance.CreateView<Board4>();
        tiles = boardView.tiles;
        numTiles = tiles.Count;
        
        TableTop.Instance.PlacePieceOnBoard(playerPiece, tiles[playerPiece.currentTileIndex].position);
        TableTop.Instance.SetBoard(boardView);

        gold.text = CurrencyManager.Instance.gold.ToString();
        diamonds.text = CurrencyManager.Instance.diamonds.ToString();
        rolls.text = CurrencyManager.Instance.numRolls.ToString();

        menuButton.onClick.AddListener(() => { menuOpen = !menuOpen; });
        diceButton.onClick.AddListener(() => { RollDice(); });

        profileButton.onClick.AddListener(() => {
            var inventoryView = Factory.Instance.CreateView<InventoryView>();
            inventoryView.transform.SetParent(gameObject.transform, false);
        });

        logoutButton.onClick.AddListener(() => {
            NotificationCenter.Notify(Notifications.UI.logout);
            Destroy(gameObject);
        });
    }

    private async void RollDice() {
        tileAnimating = true;
        CurrencyManager.Instance.numRolls--;
        diceButton.interactable = false;
        var rollAmount = GetRollAmount();
        diceRoll.text = $"Rolled a {rollAmount.ToString()}!";
        await playerPiece.MoveMultipleTiles(rollAmount, tiles);
        // await playerPiece.MoveToNextTile(tiles);
        if (CurrencyManager.Instance.numRolls > 0) { diceButton.interactable = true; }
        tileAnimating = false;
    }

    private int GetRollAmount() { return UnityEngine.Random.Range(1, numTiles); }
    public void OnShowTimer() { timeText.gameObject.SetActive(true); }
    public void OnHideTimer() { timeText.gameObject.SetActive(false); }
    public void OnSetTimerText(string time) { timeText.text = time; }
}