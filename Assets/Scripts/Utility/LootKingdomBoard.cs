using System;
using System.Collections.Generic;
using UnityEngine;

public class LootKingdomBoard : Singleton<LootKingdomBoard> {
    [SerializeField] private GameObject boardContainer;

    public void SetBoard(Board board) {
        board.transform.SetParent(boardContainer.transform, false);
    }

    public void AddPiece(View piece) {
        piece.transform.SetParent(boardContainer.transform, false);
    }
}
