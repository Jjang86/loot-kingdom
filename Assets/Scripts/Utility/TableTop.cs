using System;
using System.Collections.Generic;
using UnityEngine;

public class TableTop : Singleton<TableTop> {
    [SerializeField] private GameObject tableTop;

    public void SetBoard(Board board) {
        board.transform.SetParent(tableTop.transform, false);
    }

    public void AddPiece(View piece, Vector3 position) {
        piece.transform.SetParent(tableTop.transform, false);
        piece.transform.position = position;
    }
}
