using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    private int curStyle;
    private Sprite wallSprite;
    private Sprite floorSprite;
    public Sprite boxSprite;
    public Sprite trapSprite;

    public List<Sprite> wallSpriteList;
    public List<Sprite> floorSpriteList;

    private Grid grid;
    private Tilemap[] tilemapLayers;
    private Tilemap groundLayer, wallLayer, propLayer, trapLayer;

    private Tile wallTile, floorTile, boxTile, trapTile;

    public int[] hasPath = { 0, 0, 0, 0 };
    public int boxNumber = 0;

    private int[,,] mapExample = {
            {{0, 0, 0 },{0, 1, 0 },{0, 0, 0 } },
            {{0, 0, 0 },{1, 1, 1 },{0, 0, 0 } },
            {{0, 1, 0 },{0, 1, 0 },{0, 1, 0 } },
            {{0, 1, 0 },{1, 1, 1 },{0, 1, 0 } },
            {{1, 1, 0 },{1, 1, 0 },{0, 0, 0 } },
            {{0, 0, 0 },{0, 1, 1 },{0, 1, 1 } },
            {{1, 1, 1 },{1, 1, 1 },{1, 1, 1 } }
        };

    // Start is called before the first frame update
    void Start()
    {

        

        curStyle = JourneyManager.getInstance().tileStyle;
        wallSprite = wallSpriteList[curStyle];
        floorSprite = floorSpriteList[curStyle];

        grid = GetComponentInChildren<Grid>();
        tilemapLayers = grid.GetComponentsInChildren<Tilemap>();
        groundLayer = tilemapLayers[0];
        wallLayer = tilemapLayers[1];
        propLayer = tilemapLayers[2];
        trapLayer = tilemapLayers[3];

        boxTile = ScriptableObject.CreateInstance<Tile>();
        boxTile.sprite = boxSprite;
        wallTile = ScriptableObject.CreateInstance<Tile>();
        wallTile.sprite = wallSprite;
        floorTile = ScriptableObject.CreateInstance<Tile>();
        floorTile.sprite = floorSprite;
        trapTile = ScriptableObject.CreateInstance<Tile>();
        trapTile.sprite = trapSprite;

        initTiles();

        for (int i = 0; i < 50; i++)
        {
            int xPos = Random.Range(-12, 12);
            int yPos = Random.Range(-12, 12);

            if (checkPixelAvailable(xPos, yPos, 3))
            {
                int mdl = Random.Range(0, 7);
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        if (mapExample[mdl, j, k] == 1)
                        {
                            wallLayer.SetTile(new Vector3Int(xPos -1 + j, yPos - 1 + k, 0), wallTile);
                        }
                    }
                }
            }
        }

        int curBox = 0, attemp = 0;

        while (curBox < boxNumber && attemp < 100)
        {
            int xPos = Random.Range(-15, 15);
            int yPos = Random.Range(-15, 15);

            if (checkPixelAvailable(xPos, yPos, 4))
            {
                propLayer.SetTile(new Vector3Int(xPos, yPos, 0), boxTile);
                curBox++;
            }
            attemp++;
        }

        while (curBox < boxNumber)
        {
            int xPos = Random.Range(-15, 15);
            int yPos = Random.Range(-15, 15);

            if (checkPixelAvailable(xPos, yPos, 1))
            {
                propLayer.SetTile(new Vector3Int(xPos, yPos, 0), boxTile);
                curBox++;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (hasPath[i] == 0)
            {
                addEdge(i);
            }
        }

        InvokeRepeating("addTrap", 1, 5);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool checkPixelAvailable(int x, int y, int r)
    {
        for (int p = -r; p <= r; p++)
        {
            for (int q = -r; q <= r; q++)
            {
                int curX = x + p;
                int curY = y + q;
                if (wallLayer.HasTile(new Vector3Int(curX, curY, 0)) || propLayer.HasTile(new Vector3Int(curX, curY, 0)) || trapLayer.HasTile(new Vector3Int(curX, curY, 0)))
                {
                    return false;
                }
            }
        }
        return true;
    }

    public void initTiles()
    {
        for (int i = -18; i < 18; i++)
        {
            for (int j = -18; j < 18; j++)
            {
                if ((i >= -3 && i < 3) || (j >= -3 && j < 3) ||(i >= -15 && i < 15 && j >= -15 && j < 15))
                {
                    groundLayer.SetTile(new Vector3Int(i, j, 0), floorTile);
                }
                else
                {
                    wallLayer.SetTile(new Vector3Int(i, j, 0), wallTile);
                }
            }
        }
        for (int j = -6; j < 6; j++)
        {
            if (j < -3 || j > 2)
            {
                for (int i = 1; i < 8; i++)
                {
                    wallLayer.SetTile(new Vector3Int(17 + i, j, 0), wallTile);
                    wallLayer.SetTile(new Vector3Int(-18 - i, j, 0), wallTile);
                    wallLayer.SetTile(new Vector3Int(j, 17 + i, 0), wallTile);
                    wallLayer.SetTile(new Vector3Int(j, -18 - i, 0), wallTile);
                }
            }
            else
            {
                for (int i = 1; i < 8; i++)
                {
                    groundLayer.SetTile(new Vector3Int(17 + i, j, 0), floorTile);
                    groundLayer.SetTile(new Vector3Int(-18 - i, j, 0), floorTile);
                    groundLayer.SetTile(new Vector3Int(j, 17 + i, 0), floorTile);
                    groundLayer.SetTile(new Vector3Int(j, -18 - i, 0), floorTile);
                }
            }
        }
    }

    public void addEdge(int dir)
    {
        switch(dir)
        {
            case 0:
                addHorizenEdge(false);
                break;
            case 1:
                addVerticalEdge(false);
                break;
            case 2:
                addHorizenEdge(true);
                break;
            case 3:
                addVerticalEdge(true);
                break;
        }
    }

    public void addHorizenEdge(bool leftEdge)
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

    public void addVerticalEdge(bool upEdge)
    {
        int start = 15, dir = 1;
        if (upEdge)
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
                    wallLayer.SetTile(new Vector3Int(j, curPos, 0), wallTile);
                }
            }
            else
            {
                for (int j = -6; j < 6; j++)
                {
                    groundLayer.SetTile(new Vector3Int(j, curPos, 0), null);
                    wallLayer.SetTile(new Vector3Int(j, curPos, 0), null);
                }
            }
        }
    }

    public void addTrap()
    {
        int xPos = Random.Range(-15, 15);
        int yPos = Random.Range(-15, 15);

        if (checkPixelAvailable(xPos, yPos, 2))
        {
            trapLayer.SetTile(new Vector3Int(xPos, yPos, 0), trapTile);
        }
    }
}
