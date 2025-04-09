using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class MapGenerate : MonoBehaviour
{
    [System.Serializable]
    public class BiomeType
    {
        public GameObject prefab;
        public int size = 1000;
        public int width = 1;    // ������ � �������
        public int height = 1;   // ������ � �������
        public int count = 1;    // ����������
        public Color gizmoColor; // ���� ��� ������������
    }

    [Header("Grid Settings")]
    public int gridWidth = 6;
    public int gridHeight = 6;
    public int cellSize = 1000;
    public Vector2 gridOffset;

    [Header("Biome Settings")]
    public List<BiomeType> biomeTypes = new List<BiomeType>();

    public Transform biomesParent;

    private bool[,] grid;
    private List<Vector2Int> availableSpots = new List<Vector2Int>();

    void Start()
    {
        GenerateBiomes();
    }

    void GenerateBiomes()
    {
        InitializeGrid();
        PlaceAllBiomes();
    }

    void InitializeGrid()
    {
        grid = new bool[gridWidth, gridHeight];
        availableSpots.Clear();

        // �������� � ������
        Vector2Int center = new Vector2Int(gridWidth / 2, gridHeight / 2);
        availableSpots.Add(center);
    }

    void PlaceAllBiomes()
    {
        // ������� ��������� ������� �����
        foreach (var type in biomeTypes)
        {
            if (type.width > 1 || type.height > 1) // ��� ������� ����
            {
                for (int i = 0; i < type.count; i++)
                {
                    PlaceBiome(type, true);
                }
            }
        }

        // ����� ��������� ��������� �����
        foreach (var type in biomeTypes)
        {
            if (type.width == 1 && type.height == 1) // ��� ��������� ����
            {
                for (int i = 0; i < type.count; i++)
                {
                    PlaceBiome(type, false);
                }
            }
        }
    }

    void PlaceBiome(BiomeType type, bool isLarge)
    {
        ShuffleAvailableSpots();

        foreach (var spot in availableSpots)
        {
            // ��� ������� ������ ��������� �������������� ������� ����������
            if (isLarge && !IsFarFromOtherLargeBiomes(spot, type))
                continue;

            if (TryPlaceAt(spot.x, spot.y, type))
                return;
        }

        Debug.LogWarning($"�� ������� ���������� ���� {type.width}x{type.height}");
    }

    bool TryPlaceAt(int startX, int startY, BiomeType type)
    {
        // ��������� �������
        if (startX < 0 || startY < 0 ||
            startX + type.width > gridWidth ||
            startY + type.height > gridHeight)
            return false;

        // ��������� ����������� �������
        for (int x = startX; x < startX + type.width; x++)
        {
            for (int y = startY; y < startY + type.height; y++)
            {
                if (grid[x, y])
                    return false;
            }
        }

        // �������� ������
        for (int x = startX; x < startX + type.width; x++)
        {
            for (int y = startY; y < startY + type.height; y++)
            {
                grid[x, y] = true;
            }
        }

        // ������� �������������� �����
        RemoveOverlappingSpots(new RectInt(startX, startY, type.width, type.height));

        // ��������� ����� ����� ������
        AddSurroundingSpots(startX, startY, type.width, type.height);

        // ������� ����
        CreateBiomeObject(startX, startY, type);

        return true;
    }

    bool IsFarFromOtherLargeBiomes(Vector2Int spot, BiomeType type)
    {
        // ��������� ���������� �� ������ ������� ������
        int minDistance = 2; // ����������� ���������� � �������

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                if (grid[x, y] && (x != spot.x || y != spot.y))
                {
                    // ��������� ������ ��������� �����
                    bool isNeighborLarge = false;
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            int nx = x + dx;
                            int ny = y + dy;
                            if (nx >= 0 && ny >= 0 && nx < gridWidth && ny < gridHeight)
                            {
                                if (grid[nx, ny] && (dx != 0 || dy != 0))
                                {
                                    isNeighborLarge = true;
                                    break;
                                }
                            }
                        }
                        if (isNeighborLarge) break;
                    }

                    if (isNeighborLarge)
                    {
                        float distance = Vector2Int.Distance(spot, new Vector2Int(x, y));
                        if (distance < minDistance)
                            return false;
                    }
                }
            }
        }
        return true;
    }

    void AddSurroundingSpots(int x, int y, int width, int height)
    {
        // ��������� ����� ������ ������������ �����
        int padding = 1; // ������ � 1 ������

        // ������� � ������ �������
        for (int dx = -padding; dx < width + padding; dx++)
        {
            int px = x + dx;
            if (px < 0 || px >= gridWidth) continue;

            // ����
            int pyTop = y - padding;
            if (pyTop >= 0 && !grid[px, pyTop])
                AddUniqueSpot(new Vector2Int(px, pyTop));

            // ���
            int pyBottom = y + height - 1 + padding;
            if (pyBottom < gridHeight && !grid[px, pyBottom])
                AddUniqueSpot(new Vector2Int(px, pyBottom));
        }

        // ����� � ������ ������� (�������� ����, ������� ��� ���������)
        for (int dy = -padding + 1; dy < height + padding - 1; dy++)
        {
            int py = y + dy;
            if (py < 0 || py >= gridHeight) continue;

            // ����
            int pxLeft = x - padding;
            if (pxLeft >= 0 && !grid[pxLeft, py])
                AddUniqueSpot(new Vector2Int(pxLeft, py));

            // �����
            int pxRight = x + width - 1 + padding;
            if (pxRight < gridWidth && !grid[pxRight, py])
                AddUniqueSpot(new Vector2Int(pxRight, py));
        }
    }

    void AddUniqueSpot(Vector2Int spot)
    {
        if (!availableSpots.Contains(spot) && !grid[spot.x, spot.y])
            availableSpots.Add(spot);
    }

    void RemoveOverlappingSpots(RectInt area)
    {
        for (int i = availableSpots.Count - 1; i >= 0; i--)
        {
            if (area.Contains(availableSpots[i]))
                availableSpots.RemoveAt(i);
        }
    }

    void ShuffleAvailableSpots()
    {
        // ������������ ����� ��� ���������� ������� ����������
        for (int i = 0; i < availableSpots.Count; i++)
        {
            int randomIndex = Random.Range(i, availableSpots.Count);
            var temp = availableSpots[randomIndex];
            availableSpots[randomIndex] = availableSpots[i];
            availableSpots[i] = temp;
        }
    }

    void CreateBiomeObject(int gridX, int gridY, BiomeType type)
    {
        if (type.width == 1)
        {
            Vector3 position = new Vector3(

            gridX * cellSize + (type.width * cellSize) / 2 + gridOffset.x - 125,
            0,
            gridY * cellSize + (type.height * cellSize) / 2 + gridOffset.y - 125
        );

            GameObject biome = Instantiate(type.prefab, position, Quaternion.identity, biomesParent);
            biome.transform.localScale = new Vector3(
                type.width * type.size,
                1,
                type.height * type.size
            );
        }
        else 
        {
            Vector3 position1 = new Vector3(
            gridX * cellSize + (type.width * cellSize) / 2 + gridOffset.x - 250,
            0,
            gridY * cellSize + (type.height * cellSize) / 2 + gridOffset.y - 250
            );

            GameObject biome = Instantiate(type.prefab, position1, Quaternion.identity, biomesParent);
            biome.transform.localScale = new Vector3(
                type.width * type.size,
                1,
                type.height * type.size
            );
        }
        
    }

    void OnDrawGizmos()
    {
        // ������ �����
        Gizmos.color = Color.white;
        for (int x = 0; x <= gridWidth; x++)
        {
            Vector3 start = new Vector3(x * cellSize + gridOffset.x, 0, gridOffset.y);
            Vector3 end = new Vector3(x * cellSize + gridOffset.x, 0, gridHeight * cellSize + gridOffset.y);
            Gizmos.DrawLine(start, end);
        }
        for (int y = 0; y <= gridHeight; y++)
        {
            Vector3 start = new Vector3(gridOffset.x, 0, y * cellSize + gridOffset.y);
            Vector3 end = new Vector3(gridWidth * cellSize + gridOffset.x, 0, y * cellSize + gridOffset.y);
            Gizmos.DrawLine(start, end);
        }

        // ������ ������� ������
        if (grid != null)
        {
            foreach (var type in biomeTypes)
            {
                
                for (int x = 0; x < gridWidth; x++)
                {
                    for (int y = 0; y < gridHeight; y++)
                    {
                        if (grid[x, y])
                        {
                            Vector3 center = new Vector3(
                                x * cellSize + cellSize / 2 + gridOffset.x,
                                0,
                                y * cellSize + cellSize / 2 + gridOffset.y);
                            Gizmos.DrawCube(center, new Vector3(cellSize, 0.1f, cellSize));
                        }
                        Gizmos.color = type.gizmoColor;
                    }
                }
            }
        }

        // ������ ��������� �����
        Gizmos.color = Color.yellow;
        foreach (var spot in availableSpots)
        {
            Gizmos.DrawSphere(new Vector3(
                spot.x * cellSize + cellSize / 2 + gridOffset.x,
                0,
                spot.y * cellSize + cellSize / 2 + gridOffset.y), 100);
        }
    }
}