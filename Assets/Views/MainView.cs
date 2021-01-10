using UnityEngine;
using UnityEngine.UI;

public class MainView : View {
    public GameObject boardContainer;
    public Button menuButton;

    private void Start() {
        var boardView = Factory.Instance.CreateView<Board4>();
        boardView.transform.SetParent(boardContainer.transform, false);
    }
}