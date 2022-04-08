using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] tilePrefab;
    private float spawnPosition = 0;

    //Tile width is 55
    public float tileLenght = 55;
    private int spawnTiles = 3;

    private List<GameObject> activeTiles = new List<GameObject>();

    [SerializeField] private Transform player;

    void Start()
    {
        for (int i = 0; i < spawnTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(3);
            }

            SpawnTile(Random.Range(0, tilePrefab.Length - 1));
        }
    }

    void Update()
    {
        if (player.position.x - 30 > spawnPosition - (spawnTiles * tileLenght))
        {
            SpawnTile(Random.Range(0, tilePrefab.Length));
            DeleteTile();
        }
    }

    private void SpawnTile(int tileIndex)
    {
        GameObject nextTile = Instantiate(tilePrefab[tileIndex], transform.right * spawnPosition, transform.rotation);
        activeTiles.Add(nextTile);
        spawnPosition += tileLenght;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}