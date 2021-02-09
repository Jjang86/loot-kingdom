using System;
using System.Collections.Generic;
using UnityEngine;

public class TableTop : Singleton<TableTop> {
    [SerializeField] private GameObject pieces;

    public void SetBoard(Board board) {
        board.transform.SetParent(pieces.transform, false);
    }

    public void PlacePieceOnBoard(View piece, Vector3 position) {
        piece.transform.SetParent(pieces.transform, false);
        piece.transform.position = position;
    }

    public void ClearBoard() {
        foreach (Transform child in pieces.transform) {
            Destroy(child.gameObject);
        }
    }
}
