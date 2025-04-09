using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Terrain))]
public class DesertBiomeGenerator : MonoBehaviour
{
    [Header("Desert Settings")]
    public float duneHeight = 50f;
    public float duneFrequency = 0.01f;
    public float duneSharpness = 2f;
    public Color sandColor = new Color(0.96f, 0.87f, 0.7f);
    public Texture2D sandTexture;
    public Texture2D rockTexture;
    public GameObject[] desertProps; // Кактусы, камни и т.д.

    private Terrain terrain;
    private bool[,] gridOccupied;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        GenerateDesert();
    }

    void GenerateDesert()
    {
        // Настройка террейна
        terrain.terrainData = GenerateTerrainData(terrain.terrainData);
        ApplySandTextures();
    }

    TerrainData GenerateTerrainData(TerrainData terrainData)
    {
        int resolution = terrainData.heightmapResolution;
        float[,] heights = new float[resolution, resolution];

        // Генерация дюн с помощью шума Перлина
        for (int x = 0; x < resolution; x++)
        {
            for (int y = 0; y < resolution; y++)
            {
                float xCoord = x * duneFrequency;
                float yCoord = y * duneFrequency;

                // Базовый шум
                float height = Mathf.PerlinNoise(xCoord, yCoord);

                // Добавляем резкие перепады для дюн
                height = Mathf.Pow(height, duneSharpness);

                heights[x, y] = height * duneHeight / terrainData.size.y;
            }
        }

        terrainData.SetHeights(0, 0, heights);
        return terrainData;
    }

    void ApplySandTextures()
    {
        TerrainLayer[] terrainLayers = new TerrainLayer[2];

        // Основной песок
        terrainLayers[0] = new TerrainLayer();
        terrainLayers[0].diffuseTexture = sandTexture;
        terrainLayers[0].tileSize = new Vector2(50, 50);

        // Каменистые участки
        terrainLayers[1] = new TerrainLayer();
        terrainLayers[1].diffuseTexture = rockTexture;
        terrainLayers[1].tileSize = new Vector2(30, 30);
        terrainLayers[1].tileOffset = new Vector2(10, 10);

        terrain.terrainData.terrainLayers = terrainLayers;
    }
}