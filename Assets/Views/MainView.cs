using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private int currentTile;

    private int _numRolls = 10;
    private int numRolls {
        get => _numRolls;
        set {
            _numRolls = value;
            rolls.text = value.ToString();
        }
    }

    private enum Menu {
        Closed = 100,
        Open = 0
    }

    private bool menuOpen {
        get => menuOpen;
        set {
            menuOpen = value;
            var pos = menuButton.transform.position;
            if (value) { menuRect.DOAnchorPosX((int)Menu.Open, 0.3f); }
            else { menuRect.DOAnchorPosX((int)Menu.Closed, 0.3f); }
        }
    }

    private void Start() {
        currentTile = 0;
        gold.text = "1000";
        diamonds.text = "50";
        rolls.text = numRolls.ToString();

        playerPiece = Factory.Instance.CreateView<PlayerPieceView>();
        LootKingdomBoard.Instance.AddPiece(playerPiece);
        boardView = Factory.Instance.CreateView<Board4>();
        LootKingdomBoard.Instance.SetBoard(boardView);
        playerPiece.transform.position = boardView.tiles[0].transform.position;

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

        diceButton.onClick.AddListener(async () => {
            tileAnimating = true;
            numRolls--;
            diceButton.interactable = false;
            var rollAmount = GetDiceRoll();
            diceRoll.text = $"Rolled a {rollAmount.ToString()}!";
            for (int i = 0; i < rollAmount; i++) {                
                await MovePlayerPiece();
                currentTile++;
            }
            if (numRolls > 0) { diceButton.interactable = true; }
            tileAnimating = false;
        });
    }

    private int GetDiceRoll() {
        return UnityEngine.Random.Range(1, NumTiles());
    }

    private async Task MovePlayerPiece() {
        await MoveToNextTile();
    }

    private async Task MoveToNextTile() {
        var nextTile = (currentTile +1) % NumTiles();
        while ((Math.Round(playerPiece.transform.position.x, 1) != Math.Round(boardView.tiles[nextTile].transform.position.x, 1)) || (Math.Round(playerPiece.transform.position.z, 1) != Math.Round(boardView.tiles[nextTile].transform.position.z, 1))) {
            playerPiece.transform.position = Vector3.SmoothDamp(playerPiece.transform.position, boardView.tiles[nextTile].transform.position, ref velocity, smoothTime);
            await Task.Yield();
        }
    }

    private int NumTiles() {
        return boardView.tiles.Count;
    }

    void Update() {
        if (numRolls < 10) {
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
                numRolls++;
                timeRemaining = TIME_TO_GET_DICE_ROLL;
            }
        }

        if (!tileAnimating && numRolls > 0) { diceButton.interactable = true; }
    }

    void DisplayTime(float timeToDisplay) {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}