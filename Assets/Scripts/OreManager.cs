using UnityEngine;

public class OreManager : MonoBehaviour
{
    public GameObject[] tiles;
    public GameObject goldOrePrefab;

    public float timer;

    public static int oreCount = 0;
    private int maxOreCount = 3;

    private void Start()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        float delayTime = 50f;
        if (timer >= delayTime && oreCount < maxOreCount)
        {
            timer = 0;

            int randomTile = Random.Range(0, tiles.Length);
            var newOre = Instantiate(goldOrePrefab, tiles[randomTile].GetComponent<Tile>().orePosition.position, Quaternion.identity);

            tiles[randomTile].GetComponent<Tile>().isOreAvailable = true;
            tiles[randomTile].GetComponent<Tile>().activeOre = newOre;

            oreCount++;
        }
    }
}
