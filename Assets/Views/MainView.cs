using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View {
    [SerializeField] private RectTransform menuRect;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button diceButton;
    [SerializeField] private Transform xTransform;
    [SerializeField] private Transform yTransform;

    private PlayerPieceView playerPiece;
    private Board boardView;
    private int targetTile = 1;
    private Vector3 velocity;
    private float smoothTime = 0.20f;
    private bool tileAnimating = false;

    private enum Menu {
        Closed = 100,
        Open = 0
    }

    private bool _menuOpen;
    private bool menuOpen {
        get => _menuOpen;
        set {
            _menuOpen = value;
            var pos = menuButton.transform.position;
            if (value) { menuRect.DOAnchorPosX((int)Menu.Open, 0.3f); }
            else { menuRect.DOAnchorPosX((int)Menu.Closed, 0.3f); }
        }
    }

    private int RandomNumber() {
        return UnityEngine.Random.Range(1, 12);
    }

    private void SetTargetTile(int diceRoll) {
        targetTile = targetTile + diceRoll;
        if (targetTile > boardView.tiles.Count) { targetTile = targetTile % boardView.tiles.Count; }
    }

    private void MovePlayerPiece(bool animate) {
        if (animate) { 
            tileAnimating = true;
            playerPiece.transform.position = Vector3.SmoothDamp(playerPiece.transform.position, boardView.tiles[targetTile-1].transform.position, ref velocity, smoothTime);
        }
        else {
            playerPiece.transform.position = boardView.tiles[targetTile-1].transform.position;
        }
    }

    private void SetTargetPosition(Vector3 pos) {
        boardView.tiles[targetTile-1].transform.position = pos;
        velocity = Vector3.zero;
    }

    private void Start() {
        playerPiece = Factory.Instance.CreateView<PlayerPieceView>();
        LootKingdomBoard.Instance.AddPiece(playerPiece);
        boardView = Factory.Instance.CreateView<Board4>();
        LootKingdomBoard.Instance.SetBoard(boardView);
        MovePlayerPiece(false);

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

        diceButton.onClick.AddListener(() => {
            var diceRoll = RandomNumber();
            SetTargetTile(diceRoll);
            SetTargetPosition(boardView.tiles[targetTile-1].transform.position);
        });
    }

    private void Update() {
        // Move our position a step closer to the target.
        if (playerPiece.transform.position != boardView.tiles[targetTile-1].transform.position) {
            MovePlayerPiece(true);
        }
    }
}