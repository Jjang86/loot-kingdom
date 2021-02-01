using System;
using UnityEngine;
public enum SpecialTile {
    None,
    Go,
    AttackFriend,
    Dungeon,
    Minigame
}

public class Tile : View {
    [SerializeField] private SpecialTile specialTile;

    public Vector3 position {
        get => transform.position;
        set => transform.position = value;
    }

    public void OnLand() {
        GoldOnLand();
    }

    public void OnFinalLand() {
        OnSpecialTileLand();
    }

    private void GoldOnLand() {
        CurrencyManager.Instance.goldEndAmount += CurrencyManager.onLandGold;
    }

    private void OnSpecialTileLand() {
        switch(specialTile) {
            case SpecialTile.None:
                break;
            case SpecialTile.Go:
                CurrencyManager.Instance.diamondEndAmount += CurrencyManager.diamondAmountOnGo;
                break;
            case SpecialTile.AttackFriend:
                break;
            case SpecialTile.Dungeon:
                break;
            case SpecialTile.Minigame:
                break;
            default:
                throw new Exception("The special tile is incorrect. Please verify the special tile name");
        }
    }
}