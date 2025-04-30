using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Size")]
    [SerializeField] private int mapWidth = 30;
    [SerializeField] private int mapHeight = 30;

    [Header("Density Settings")]
    [SerializeField, Range(0f, 1f)] private float wallDensity = 0.1f;
    [SerializeField, Range(0f, 1f)] private float decorDensity = 0.05f;

    [Header("Prefabs")]
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject decorPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject endPrefab;

    private enum TileType { Empty, Floor, Wall, Decor }
    private TileType[,] mapData;

    private void Start()
    {
        GenerateMap();
        RenderMap();
        PlaceStartAndExit();
    }

    private void GenerateMap()
    {
        mapData = new TileType[mapWidth, mapHeight];

        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                mapData[x, y] = TileType.Floor;
            }
        }

        int wallCount = Mathf.FloorToInt(mapWidth * mapHeight * wallDensity);
        for(int i = 0; i < wallCount; i++)
        {
            int x = Random.Range(1, mapWidth - 1);
            int y = Random.Range(1, mapHeight - 1);
            mapData[x, y] = TileType.Wall;
        }

        int decorCount = Mathf.FloorToInt(mapHeight * mapHeight * decorDensity);
        for(int i = 0; i < decorCount; i++)
        {
            int x = Random.Range(1, mapWidth - 1);
            int y = Random.Range(1, mapHeight - 1);
            if (mapData[x, y] == TileType.Floor)
                mapData[x, y] = TileType.Decor;
        }
    }

    private void RenderMap()
    {
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                switch(mapData[x, y])
                {
                    case TileType.Floor:
                        Instantiate(floorPrefab, pos, Quaternion.identity);
                        break;
                    case TileType.Wall:
                        Instantiate(wallPrefab, pos, Quaternion.identity);
                        break;
                    case TileType.Decor:
                        Instantiate(floorPrefab, pos, Quaternion.identity);
                        Instantiate(decorPrefab, pos, Quaternion.identity);
                        break;
                }
            }
        }
    }

    private void PlaceStartAndExit()
    {
        Vector3 startPos = new Vector3(1, 1, 0);
        Instantiate(startPrefab, startPos, Quaternion.identity);

        Vector3 endPos = new Vector3(mapWidth - 2, mapHeight - 2, 0);
        Instantiate(endPrefab, endPos, Quaternion.identity);
    }
}
