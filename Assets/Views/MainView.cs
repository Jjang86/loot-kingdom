using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MainView : View {
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

    private const float TIME_TO_GET_DICE_ROLL = 5.0f;

    private PlayerPieceView playerPiece;
    private Board boardView;
    private Vector3 currentPosition;
    private Vector3 finalPosition;
    private Vector3 velocity;
    private float smoothTime = 0.05f;
    private float timeRemaining = TIME_TO_GET_DICE_ROLL;
    private bool timerIsRunning = false;
    private bool tileAnimating = false;
    private List<Tile> tiles;
    private int numTiles;

    private int _currentTile = 0;
    private int currentTile {
        get => _currentTile;
        set {
            _currentTile = value % numTiles;

        }
    }

    private enum MenuPosition {
        Closed = 100,
        Open = 0
    }

    private bool menuOpen {
        get => menuOpen;
        set {
            menuOpen = value;
            var pos = menuButton.transform.position;
            if (value) { menuRect.DOAnchorPosX((int)MenuPosition.Open, 0.3f); }
            else { menuRect.DOAnchorPosX((int)MenuPosition.Closed, 0.3f); }
        }
    }

    private void OnEnable() {
        NotificationCenter.Subscribe(this, Notifications.Currency.numRollsChanged, value => {
            rolls.text = value.ToString();
        });
        NotificationCenter.Subscribe(this, Notifications.Currency.goldChanged, value => {
            gold.text = value.ToString();
        });
    }

    private void OnDisable() {
        NotificationCenter.Unsubscribe(this, Notifications.Currency.numRollsChanged);
        NotificationCenter.Unsubscribe(this, Notifications.Currency.goldChanged);
    }

    private void Update() {
        RunDiceTimer();

        if (!tileAnimating && CurrencyManager.numRolls > 0) { diceButton.interactable = true; }
    }

    private void Start() {
        playerPiece = Factory.Instance.CreateView<PlayerPieceView>();
        boardView = Factory.Instance.CreateView<Board4>();
        tiles = boardView.tiles;
        numTiles = tiles.Count;
        
        TableTop.Instance.AddPiece(playerPiece, tiles[currentTile].position);
        TableTop.Instance.SetBoard(boardView);
        
        gold.text = CurrencyManager.gold.ToString();
        diamonds.text = CurrencyManager.diamonds.ToString();
        rolls.text = CurrencyManager.numRolls.ToString();

        menuButton.onClick.AddListener(() => {
            menuOpen = !menuOpen;
        });

        profileButton.onClick.AddListener(() => {
            var inventoryView = Factory.Instance.CreateView<InventoryView>();
            inventoryView.transform.SetParent(gameObject.transform, false);
        });

        logoutButton.onClick.AddListener(() => {
            var loginView = Factory.Instance.CreateView<LoginView>();
            loginView.transform.SetParent(gameObject.transform, false);
        });

        diceButton.onClick.AddListener(() => { HandleDiceRoll(); });
    }

    private async void HandleDiceRoll() {
        tileAnimating = true;
            CurrencyManager.numRolls--;
            diceButton.interactable = false;
            var rollAmount = GetDiceRoll();
            diceRoll.text = $"Rolled a {rollAmount.ToString()}!";
            for (int i = 0; i < rollAmount; i++) {                
                await MovePlayerPiece();
                tiles[currentTile].OnLand();
                currentTile++;
            }
            if (CurrencyManager.numRolls > 0) { diceButton.interactable = true; }
            tileAnimating = false;
    }

    private int GetDiceRoll() {
        return UnityEngine.Random.Range(1, numTiles);
    }

    private async Task MovePlayerPiece() {
        await MoveToNextTile();
    }

    private async Task MoveToNextTile() {
        var nextTile = (currentTile + 1) % numTiles;
        while ((Math.Round(playerPiece.position.x, 1) != Math.Round(tiles[nextTile].position.x, 1)) || (Math.Round(playerPiece.position.z, 1) != Math.Round(tiles[nextTile].position.z, 1))) {
            playerPiece.position = Vector3.SmoothDamp(playerPiece.position, tiles[nextTile].position, ref velocity, smoothTime);
            await Task.Yield();
        }
    }

    private void RunDiceTimer() {
        if (CurrencyManager.numRolls < 10) {
            timerIsRunning = true;
            timeText.gameObject.SetActive(true);
        }
        else {
            timerIsRunning = false;
            timeText.gameObject.SetActive(false);
        }
        if (timerIsRunning) {
            if (timeRemaining > 0) {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else {
                CurrencyManager.numRolls++;
                timeRemaining = TIME_TO_GET_DICE_ROLL;
            }
        }

    }

    private void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}