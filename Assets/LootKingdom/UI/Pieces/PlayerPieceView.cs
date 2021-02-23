using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerPieceView : View {
    
    private enum Heading {
        North = 0,
        East = 90,
        South = 180,
        West = -90
    }

    [SerializeField] private RectTransform rect;
    private Vector3 velocity;
    private float smoothTime = 0.05f;

    public int currentTileIndex = 0;

    private Heading _heading = Heading.West;
    private Heading heading {
        get => _heading;
        set {
            _heading = value;
            switch (value) {
                case Heading.North:
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)Heading.North, transform.eulerAngles.z);
                    break;
                case Heading.East:
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)Heading.East, transform.eulerAngles.z);
                    break;
                case Heading.South:
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)Heading.South, transform.eulerAngles.z);
                    break;
                case Heading.West:
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, (float)Heading.West, transform.eulerAngles.z);
                    break;
                default:
                    throw new Exception("Heading set incorrectly. Check the heading value");
            }
        }
    }

    private Vector3 position {
        get => transform.position;
        set => transform.position = value;
    }

    public async Task MoveStraightToTile(TileView destination) {
        CheckAndSetHeading(this.position, destination.position);
        destination.OnLand();
        while ((Math.Round(this.position.x, 1) != Math.Round(destination.position.x, 1)) || (Math.Round(this.position.z, 1) != Math.Round(destination.position.z, 1))) {
            this.position = Vector3.SmoothDamp(this.position, destination.position, ref velocity, smoothTime);
            await Task.Yield();
        }
    }

    public async Task MoveToNextTile(List<TileView> tiles, bool finalTile = false) {
        var nextTileIndex = (currentTileIndex + 1) % tiles.Count;
        if (finalTile) { { tiles[nextTileIndex].OnFinalLand(); }}
        await MoveStraightToTile(tiles[nextTileIndex]);
        currentTileIndex++;
    }

    public async Task MoveMultipleTiles(int rollAmount, List<TileView> tiles) {
        for (int rollCounter = 0; rollCounter < rollAmount; rollCounter++) {
            var nextTileIndex = (currentTileIndex + 1) % tiles.Count;
            await MoveToNextTile(tiles, rollCounter == rollAmount - 1);
        }
    }

    private void CheckAndSetHeading(Vector3 source, Vector3 destination) {
        float xValue = (float)Math.Round(destination.x, 1) - (float)Math.Round(source.x, 1);
        float zValue = (float)Math.Round(destination.z, 1) - (float)Math.Round(source.z, 1);

        if (xValue < 0) { heading = Heading.West; }
        if (xValue > 0) { heading = Heading.East; }
        if (zValue > 0) { heading = Heading.North; }
        if (zValue < 0) { heading = Heading.South; }
    }
}