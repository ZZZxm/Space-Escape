using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    public Sprite wallSprite;
    public Sprite boxSprite;

    private Grid grid;
    private Tilemap[] tilemapLayers;
    private Tilemap groundLayer, wallLayer, propLayer;

    private Tile wallTile, boxTile;

    public bool hasLeftEdge = false;
    public bool hasRightEdge = false;
    public int boxNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponentInChildren<Grid>();
        tilemapLayers = grid.GetComponentsInChildren<Tilemap>();
        groundLayer = tilemapLayers[0];
        wallLayer = tilemapLayers[1];
        propLayer = tilemapLayers[2];

        wallTile = ScriptableObject.CreateInstance<Tile>();
        wallTile.sprite = wallSprite;
        boxTile = ScriptableObject.CreateInstance<Tile>();
        boxTile.sprite = boxSprite;

        for (int i = 0; i < 40; i++)
        {
            int xPos = Random.Range(-13, 13);
            int yPos = Random.Range(-13, 13);

            if (!wallLayer.HasTile(new Vector3Int(xPos, yPos, 0)))
            {
                wallLayer.SetTile(new Vector3Int(xPos, yPos, 0), wallTile);
            }
            
        }

        int curBox = 0;

        while (curBox < boxNumber)
        {
            int xPos = Random.Range(-15, 15);
            int yPos = Random.Range(-15, 15);

            if (checkPixelAvailable(xPos, yPos))
            {
                propLayer.SetTile(new Vector3Int(xPos, yPos, 0), boxTile);
                curBox++;
            }
        }

        if (hasLeftEdge)
        {
            addEdge(true);
        }

        if (hasRightEdge)
        {
            addEdge(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool checkPixelAvailable(int x, int y)
    {
        for (int p = -2; p <= 2; p++)
        {
            for (int q = -2; q <= 2; q++)
            {
                int curX = x + p;
                int curY = y + q;
                if (wallLayer.HasTile(new Vector3Int(curX, curY, 0)) || propLayer.HasTile(new Vector3Int(curX, curY, 0)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void addEdge(bool leftEdge)
    {
        int start = 15, dir = 1;
        if (leftEdge)
        {
            start = -16;
            dir = -1;
        }
        for (int i = 0; i < 10; i++)
        {
            int curPos = start + dir * i;
            if (i < 3)
            {
                for (int j = -3; j < 3; j++)
                {
                    wallLayer.SetTile(new Vector3Int(curPos, j, 0), wallTile);
                }
            }
            else
            {
                for (int j = -6; j < 6; j++)
                {
                    groundLayer.SetTile(new Vector3Int(curPos, j, 0), null);
                    wallLayer.SetTile(new Vector3Int(curPos, j, 0), null);
                }
            }
        }
    }
}
