using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    Color selectedTileColor = new Color(0,1,1,1);
    Color defaultTileColor = new Color(1,1,1,1);

    public void Load(LevelData data)
    {
        for (int i = 0; i < data.tiles.Count; ++i)
        {
            GameObject instance = Instantiate(tilePrefab) as GameObject;
            Tile t = instance.GetComponent<Tile>();
            t.Load(data.tiles[i]);
            tiles.Add(t.pos, t);
        }
    }

    // TODO: not ideal
    // improve to use Directions
    Point[] dirs = new Point[4]
    {
        new Point(0,1),
        new Point(0,-1),
        new Point(1,0),
        new Point(-1,0)
    };

    // Path finding
    public Tile GetTile(Point p)
    {
        if (tiles.ContainsKey(p))
        {
            return tiles[p];
        }
        return null;
    }
    void SwapReference (ref Queue<Tile> a, ref Queue<Tile> b)
    {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }
    // find a list of tiles,
    // starting from a specific tile
    // meeting a criteria specified by addTile
    public List<Tile> Search(Tile start, Func<Tile, Tile, bool> addTile)
    {
        // reset pathfinding fields
        ClearSearch();

        List<Tile> retValue = new List<Tile>();
        retValue.Add(start);
        Queue<Tile> checkNext = new Queue<Tile>();
        Queue<Tile> checkNow = new Queue<Tile>();
        start.distance = 0;
        checkNow.Enqueue(start);
        while(checkNow.Count > 0)
        {
            Tile t = checkNow.Dequeue();
            for (int i = 0; i < dirs.Count(); ++i)
            {
                Tile next = GetTile(t.pos + dirs[i]);
                if (next == null || next.distance <= t.distance + 1)
                    continue;
                if (addTile(t, next))
                {
                    next.distance = t.distance + 1;
                    next.prev = t;
                    checkNext.Enqueue(next);
                    retValue.Add(next);
                }
            }
            if (checkNow.Count == 0)
            {
                // TODO: not ideal
                // checkNow ref is preserved and seems will be checked again
                SwapReference(ref checkNow, ref checkNext);
            }
        }

        return retValue;
    }
    void ClearSearch()
    {
        foreach (Tile t in tiles.Values)
        {
            t.prev = null;
            t.distance = int.MaxValue;
        }
    }

    // highlight tiles
    public void SelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            tiles[i].GetComponent<Renderer>().material.SetColor("_BaseColor", selectedTileColor);
        }
    }
    // de-highlight tiles
    public void DeSelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            tiles[i].GetComponent<Renderer>().material.SetColor("_BaseColor", defaultTileColor);
        }
    }

}
