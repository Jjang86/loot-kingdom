using UnityEngine;

public class Tile : View {

    public Vector3 position {
        get => transform.position;
        set => transform.position = value;
    }

    public virtual void OnLand() {
        CurrencyManager.Instance.goldEndAmount = CurrencyManager.Instance.goldEndAmount + CurrencyManager.onLandGold;
    }
}