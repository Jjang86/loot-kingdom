using System;
using System.Collections.Generic;
using UnityEngine;

public class TableTop : Singleton<TableTop> {
    [SerializeField] private GameObject piecesInspector;
    private static GameObject pieces;

    private void Awake() {
        pieces = piecesInspector;
    }

    public static void SetBoard(Board board) {
        board.transform.SetParent(pieces.transform, false);
    }

    public static void PlacePieceOnBoard(View piece, Vector3 position) {
        piece.transform.SetParent(pieces.transform, false);
        piece.transform.position = position;
    }

    public static void ClearBoard() {
        foreach (Transform child in pieces.transform) {
            Destroy(child.gameObject);
        }
    }
}
