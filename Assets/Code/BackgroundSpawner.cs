using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject[] backgroundPrefab;
    private float spawnPosition = 0;
    public float backgroundWidth = 61.44f;
    private int spawnBackground = 3;

    private List<GameObject> activeBackground = new List<GameObject>();

    [SerializeField] private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnBackground; i++)
        {
            if (i == 0)
            {
                SpawnBackground(0);
            }

            SpawnBackground(Random.Range(0, backgroundPrefab.Length - 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x - 35 > spawnPosition - (spawnBackground * backgroundWidth))
        {
            SpawnBackground(Random.Range(0, backgroundPrefab.Length));
            DeleteTile();
        }
    }

    private void SpawnBackground(int backgroundIndex)
    {
        GameObject nextBackground = Instantiate(backgroundPrefab[backgroundIndex], transform.right * spawnPosition, transform.rotation);
        activeBackground.Add(nextBackground);
        spawnPosition += backgroundWidth;
    }

    private void DeleteTile()
    {
        Destroy(activeBackground[0]);
        activeBackground.RemoveAt(0);
    }
}
