using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    [SerializeField] private int mapWidth = 30;
    [SerializeField] private int mapHeight = 30;
    [SerializeField] private int minRoomSize = 5;
    [SerializeField] private int maxRoomSize = 9;
    [SerializeField] private int maxRooms = 8;
    [SerializeField, Range(0.0f, 1.0f)] private float decorDensity = 0.05f;

    [Header("Prefabs")]
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private GameObject decorPrefab;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private GameObject exitPrefab;

    private enum TileType { Empty, Floor, Wall };
    private TileType[,] mapData;
    private List<RectInt> rooms = new List<RectInt>();

    private void Start()
    {
        GenerateMap();
        RenderMap();
        PlaceStartAndExit();
    }

    private void GenerateMap()
    {
        mapData = new TileType[mapWidth, mapHeight];

        for(int i = 0; i < maxRooms; i++)
        {
            int w = Random.Range(minRoomSize, maxRoomSize + 1);
            int h = Random.Range(minRoomSize, maxRoomSize + 1);
            int x = Random.Range(1, mapWidth - w - 1);
            int y = Random.Range(1, mapHeight - h - 1);
            RectInt newRoom = new RectInt(x, y, w, h);

            bool overlaps = false;
            foreach(var room in rooms)
            {
                if (newRoom.Overlaps(room))
                {
                    overlaps = true;
                    break;
                }
            }
            if (overlaps) continue;

            CreateRoom(newRoom);

            if(rooms.Count > 0)
            {
                Vector2Int prevCenter = Vector2Int.FloorToInt(rooms[rooms.Count - 1].center);
                Vector2Int currentCenter = Vector2Int.FloorToInt(newRoom.center);

                if (Random.value < 0.5f)
                {
                    CreateHorizontalCorridor(prevCenter.x, currentCenter.x, prevCenter.y);
                    CreateVerticalCorridor(prevCenter.y, currentCenter.y, prevCenter.x);
                }
                else
                {
                    CreateVerticalCorridor(prevCenter.y, currentCenter.y, prevCenter.x);
                    CreateHorizontalCorridor(prevCenter.x, currentCenter.x, prevCenter.y);
                }
            }

            rooms.Add(newRoom);
        }

        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                if (mapData[x, y] == TileType.Floor)
                    continue;

                bool adjacentToFloor = false;
                foreach(var offset in new Vector2Int[] {
                    new Vector2Int(1, 0), new Vector2Int(-1, 0),
                    new Vector2Int(0, 1), new Vector2Int(0, -1)})
                {
                    int nx = x + offset.x;
                    int ny = y + offset.y;
                    if(nx >= 0 && nx < mapWidth && ny >= 0 && ny < mapHeight &&
                        mapData[nx, ny] == TileType.Floor)
                    {
                        adjacentToFloor = true;
                        break;
                    }
                }
                if (adjacentToFloor)
                    mapData[x, y] = TileType.Wall;
            }
        }
    }

    private void CreateRoom(RectInt room)
    {
        for (int x = room.xMin; x < room.xMax; x++)
            for (int y = room.yMin; y < room.yMax; y++)
                mapData[x, y] = TileType.Floor;
    }

    private void CreateHorizontalCorridor(int x1, int x2, int y)
    {
        int start = Mathf.Min(x1, x2);
        int end = Mathf.Max(x1, x2);
        for (int x = start; x < end; x++)
            mapData[x, y] = TileType.Floor;
    }

    private void CreateVerticalCorridor(int y1, int y2, int x)
    {
        int start = Mathf.Min(y1, y2);
        int end = Mathf.Max(y1, y2);
        for (int y = start; y < end; y++)
            mapData[x, y] = TileType.Floor;
    }

    private void RenderMap()
    {
        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                Vector3 pos = new Vector3(x, y, 0);
                if (mapData[x, y] == TileType.Floor)
                {
                    Instantiate(floorPrefab, pos, Quaternion.identity);
                    if(Random.value < decorDensity)
                        Instantiate(decorPrefab, pos, Quaternion.identity);
                }
                else if (mapData[x, y] == TileType.Wall)
                {
                    Instantiate(wallPrefab, pos, Quaternion.identity);
                }
            }
        }
    }

    private void PlaceStartAndExit()
    {
        if(rooms.Count > 0)
        {
            Vector3 startPos = new Vector3(rooms[0].center.x, rooms[0].center.y, 0);
            Instantiate(startPrefab, startPos, Quaternion.identity);
            Vector3 exitPos = new Vector3(rooms[rooms.Count - 1].center.x, rooms[rooms.Count - 1].center.y, 0);
            Instantiate(exitPrefab, exitPos, Quaternion.identity);
        }
    }
}
