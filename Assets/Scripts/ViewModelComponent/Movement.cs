using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Movement : MonoBehaviour
{
    public int range;
    public int jumpHeight;
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
        // find child object Jumper
        jumper = transform.Find("Jumper");
    }
    public virtual List<Tile> GetTilesInRange(Board board)
    {
        List<Tile> retValue = board.Search(unit.tile, ExpandSearch);
        Filter(retValue);
        return retValue;
    }
    protected virtual bool ExpandSearch(Tile from, Tile to)
    {
        // TODO: +1 might not be ideal
        // I think +1 should be removed
        return (from.distance+1) <= range;
    }
    // Disallow certain blocks to be the final location of a unit
    // For example, an Ally or an enemy
    protected virtual void Filter(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
        {
            if (tiles[i].content != null)
                tiles.RemoveAt(i);
        }
    }
    // handles the animation of traversing tiles
    public abstract IEnumerator Traverse(Tile tile);

    protected virtual IEnumerator Turn(Directions dir)
    {
        TransformLocalEulerTweener t =
            (TransformLocalEulerTweener)transform.RotateToLocal(
                dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad
            );
        // When rotating between North and West, we must make an exception
        // so it looks like the unit rotates the most efficient way
        if (Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
        {
            // 0-degree and 360-degree are the same
            t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        }
        else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
        {
            t.endValue = new Vector3(t.endValue.x, 360f, t.endValue.z);
        }
        unit.dir = dir;

        // When turning is complete, t is destroyed and becomes null
        // This code blocks execution until turning is complete
        // so that the GameObject does not turn and slide sideway
        while (t != null)
            yield return null;
    }
}
