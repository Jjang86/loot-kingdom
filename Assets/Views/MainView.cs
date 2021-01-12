using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainView : View {
    [SerializeField] private GameObject board;
    [SerializeField] private RectTransform menuRect;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button profileButton;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button diceButton;
    [SerializeField] private Transform xTransform;
    [SerializeField] private Transform yTransform;

    private PlayerPieceView playerPiece;
    private Board boardView;
    private int currentPosition = 1;

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

    private int GetNewCurrentPosition(int diceRoll) {
        currentPosition = currentPosition + diceRoll;
        if (currentPosition > boardView.tiles.Count) { currentPosition = currentPosition % boardView.tiles.Count; }
        return currentPosition;
    }

    private void SetPlayerPiece() {
        playerPiece.transform.position = boardView.tiles[currentPosition-1].transform.position;
    }

    private void Start() {
        playerPiece = Factory.Instance.CreateView<PlayerPieceView>();
        playerPiece.transform.SetParent(board.transform, false);
        boardView = Factory.Instance.CreateView<Board4>();
        boardView.transform.SetParent(board.transform, false);
        SetPlayerPiece();

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
            var newPosition = GetNewCurrentPosition(diceRoll);
            SetPlayerPiece();
        });
    }
}