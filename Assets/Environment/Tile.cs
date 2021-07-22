using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] bool isPlaceable;

    public bool IsPlaceable { get { return isPlaceable; } }

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();
    LevelLoader levelLoader;

    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    void Start()
    {
        if (gridManager != null) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable) {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    void OnMouseDown()
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates) && levelLoader.IsPlaying) {
            bool isTowerPlacedSuccessfully = towerPrefab.CreateTower(towerPrefab, transform.position);
            
            if (isTowerPlacedSuccessfully) {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers();
            }
        }
    }
}
