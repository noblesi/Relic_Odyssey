using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator_Tilemap : MonoBehaviour
{
    [Header("Map Size")]
    [SerializeField] private int mapWidth = 30;
    [SerializeField] private int mapHeight = 30;

    [Header("Density Settings")]
    [SerializeField, Range(0f, 1f)] private float wallDensity = 0.1f;
    [SerializeField, Range(0f, 1f)] private float decorDensity = 0.05f;

    [Header("Tilemap & Tiles")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase floorTile;
    [SerializeField] private TileBase wallTile;
    [SerializeField] private TileBase decorTile;
    [SerializeField] private TileBase startTile;
    [SerializeField] private TileBase exitTile;

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
        for (int i = 0; i < wallCount; i++)
        {
            int x = Random.Range(1, mapWidth - 1);
            int y = Random.Range(1, mapHeight - 1);
            mapData[x, y] = TileType.Wall;
        }

        int decorCount = Mathf.FloorToInt(mapWidth * mapHeight * decorDensity);
        for (int i = 0; i < decorCount; i++)
        {
            int x = Random.Range(1, mapWidth - 1);
            int y = Random.Range(1, mapHeight - 1);
            if (mapData[x, y] == TileType.Floor)
                mapData[x, y] = TileType.Decor;
        }
    }

    private void RenderMap()
    {
        tilemap.ClearAllTiles();

        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);
                switch(mapData[x, y])
                {
                    case TileType.Floor:
                        tilemap.SetTile(pos, floorTile);
                        break;
                    case TileType.Wall:
                        tilemap.SetTile(pos, wallTile);
                        break;
                    case TileType.Decor:
                        tilemap.SetTile(pos, floorTile);
                        tilemap.SetTileFlags(pos, TileFlags.None);
                        tilemap.SetTile(pos, decorTile);
                        break;
                }
            }
        }
    }

    void PlaceStartAndExit()
    {
        Vector3Int startPos = new Vector3Int(1, 1, 0);
        Vector3Int exitPos = new Vector3Int(mapWidth - 2, mapHeight - 2, 0);

        tilemap.SetTile(startPos, startTile);
        tilemap.SetTile(exitPos, exitTile);
    }
}
