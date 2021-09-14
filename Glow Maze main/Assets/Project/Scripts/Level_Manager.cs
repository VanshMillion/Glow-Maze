using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Manager : MonoBehaviour
{
    [Header("Level Texture")]
    [SerializeField] private Texture2D levelTexture;

    [Header("Tiles Prefabs")]
    [SerializeField] private GameObject wallTilePrefab;
    [SerializeField] private GameObject roadTilePrefab;

    private Color wallPixelColor = Color.white;
    private Color roadPixelColor = Color.black;

    private float unitPerPixel;

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        unitPerPixel = wallTilePrefab.transform.lossyScale.x;
        float halfUnitPerPixel = unitPerPixel / 2f;

        float width = levelTexture.width;
        float height = levelTexture.height;

        Vector3 offset = (new Vector3(width / 2f, 0f, height / 2f) * unitPerPixel)
                          - new Vector3(halfUnitPerPixel, 0f, halfUnitPerPixel);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                //Get Pixel Color:
                Color pixelColor = levelTexture.GetPixel(x, y);

                Vector3 spawnPos = ((new Vector3(x, 0f, y) * unitPerPixel) - offset);

                if(pixelColor == wallPixelColor)
                {
                    //Wall
                    Spawn(wallTilePrefab, spawnPos);
                }
                else if(pixelColor == roadPixelColor)
                {
                    //Road
                    Spawn(roadTilePrefab, spawnPos);
                }
            }
        }
    }

    private void Spawn(GameObject tilePrefab, Vector3 position)
    {
        //Fix Y position
        position.y = tilePrefab.transform.position.y;

        GameObject obj = Instantiate(tilePrefab, position, Quaternion.identity, transform);
    }
}
