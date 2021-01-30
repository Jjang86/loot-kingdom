using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerPieceView : View {
    private Vector3 velocity;
    private float smoothTime = 0.05f;

    private Vector3 position {
        get => transform.position;
        set => transform.position = value;
    }

    public async Task MoveToNextTile(int currentTile, List<Tile> tiles) {
        var nextTile = (currentTile + 1) % tiles.Count;
        while ((Math.Round(this.position.x, 1) != Math.Round(tiles[nextTile].position.x, 1)) || (Math.Round(this.position.z, 1) != Math.Round(tiles[nextTile].position.z, 1))) {
            this.position = Vector3.SmoothDamp(this.position, tiles[nextTile].position, ref velocity, smoothTime);
            await Task.Yield();
        }
    }
}