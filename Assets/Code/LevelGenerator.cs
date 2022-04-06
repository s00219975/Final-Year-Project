using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public int width;

    public GameObject[] tilePrefab;
    private float spawnPosition = 0;

    //Tile width size is 55
    public float tileLenght = 55;
    private int spawnTiles = 6;

    private List<GameObject> activeTiles = new List<GameObject>();

    [SerializeField] private Transform player;


    // Start is called before the first frame update
    void Start()
    {
        SpawnTile(0);

        for (int i = 0; i < spawnTiles; i++)
        {
            SpawnTile(Random.Range(0, tilePrefab.Length));
        }
    }

    // Update is called once per frame
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
