using UnityEngine;

public class PlayerPieceView : View {
    public Vector3 position {
        get => transform.position;
        set => transform.position = value;
    }
}