using UnityEngine;

public class Unit : MonoBehaviour
{
    public Tile tile { get; protected set; }
    public Directions dir;

    public void Place(Tile target)
    {
        if (tile != null && tile.content == gameObject)
            tile.content = null;
        tile = target;
        target.content = gameObject;
    }
    public void Match()
    {
        transform.localPosition = tile.center;
        transform.localEulerAngles = dir.ToEuler();
    }
}
