using System;
using System.Collections.Generic;
using System.Linq;
using UniTools;
using UnityEngine;

public class PlatformGenerator : IDisposable
{
    private CharacterBaseView character;
    private List<TileView> tiles = new List<TileView>();
    private UnityEngine.Pool.ObjectPool<TileView> tilePool;
    private UnityEngine.Pool.ObjectPool<InteractableView> interactableViewPool;
    private TileView lastTile;
    private GenerateSettingsConfig settings;
    private const float minDistanceToCorrectCoordinates = 400;
    public readonly Vector3 Direction = new Vector3(1, 0, 0);


    /// Invoke for initialization generator
    public void Generate(CharacterBaseView characterView, GenerateSettingsConfig generationSettings, Action<TileView> onTileGenerate = null, Action<InteractableView> onInteractableGenerate = null)
    {
        settings = generationSettings;

        character = characterView;

        interactableViewPool = new(
            createFunc: () =>
            {
                var interactable = UnityEngine.Object.Instantiate(settings.interactableViewsPrefabs.GetRandom());
                onInteractableGenerate?.Invoke(interactable);

                return interactable;
            },
            actionOnGet: tile =>
            {
                tile.SetActive(true);
            },
            actionOnRelease: tile =>
            {
                tile.SetActive(false);
            },
            collectionCheck: false);

        tilePool = new(
            createFunc: () =>
            {
                var tile = UnityEngine.Object.Instantiate(settings.tilePrefab);
                onTileGenerate?.Invoke(tile);

                return tile;
            },
            actionOnGet: tile =>
            {
                if (lastTile != null)
                {
                    tile.transform.position = lastTile.transform.position +
                        (Direction * ((lastTile.tileLenght / 2) + (tile.tileLenght / 2)));
                }

                tile.spawnPoints.ForEach(sp =>
                {
                    if (!RandomTools.GetChance(settings.chansForSpawnInteractable)) return;

                    var interactableView = interactableViewPool.Get();
                    interactableView.transform.position = sp.position;

                    interactableView.onInteract = character =>
                    {
                        interactableView.SetActive(false);
                    };

                    tile.interactableViews.Add(interactableView);
                });

                tiles.Add(tile);
                lastTile = tile;

                tile.SetActive(true);
            },
            actionOnRelease: tile =>
            {
                tile.SetActive(false);

                tile.interactableViews.ForEach(iv =>
                {
                    interactableViewPool.Release(iv);
                });
                tile.interactableViews.Clear();

                tiles.Remove(tile);
            });

        for (int i = 0; i < 5; i++)
        {
            tilePool.Get();
        }

        var centreTile = tiles[tiles.Count / 2];
        characterView.SetPosition(centreTile.transform.position.WithY(y => y + 2));

        if (settings.distanceToCorrectCoordinates < minDistanceToCorrectCoordinates)
            Debug.LogWarning("distanceToCorrectCoordinates less than minimum");
    }
    /// Invoke for tiles rebuild with ne character position
    public void UpdateTiles()
    {
        if (tiles.Count == 0 || character == null) return;

        if (character.transform.position.x + settings.distanceToSpawnTile > lastTile.transform.position.x)
        {
            var newTile = tilePool.Get();
            Log($"generate tile, in position: {newTile.transform.position}");
        }

        tiles.ToList().ForEach(tile =>
        {
            if (character.transform.position.x - settings.distanceToReleaseTile > tile.transform.position.x)
            {
                tilePool.Release(tile);
                Log($"remove tile, in position: {tile.transform.position}");
            }
        });

        var distanceToSceneCentre = Vector3.Distance(character.transform.position, Vector3.zero);
        if (distanceToSceneCentre >= settings.distanceToCorrectCoordinates && distanceToSceneCentre >= minDistanceToCorrectCoordinates) CorrectCoordinates();
    }

    private void CorrectCoordinates()
    {
        var oldCharacterPos = character.transform.position;
        character.SetPosition(Vector3.zero);

        var distance = Vector3.Distance(oldCharacterPos, character.transform.position);

        tiles.ForEach(tile =>
        {
            tile.transform.position += tile.transform.position.Direction(character.transform.position) * distance;
        });
    }
    //to quickly search generator logs in the console
    private void Log(string message)
    {
        Debug.Log($"GENERATOR|{message}");
    }

    public void Dispose()
    {
        interactableViewPool.Dispose();
        tilePool.Dispose();
    }
}
