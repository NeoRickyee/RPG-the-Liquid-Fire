using UnityEngine;
[System.Serializable]
public struct Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.x + b.x, a.y + b.y);
    }
    public static Point operator -(Point a, Point b)
    {
        return new Point(a.x - b.x, a.y - b.y);
    }
    public static bool operator ==(Point a, Point b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Point a, Point b)
    {
        return !(a == b);
    }
    public override bool Equals(object obj)
    {
        if (!(obj is Point))
            return false;

        Point p = (Point)obj;
        return this == p;
    }
    public override int GetHashCode()
    {
        return x ^ y;
    }
    public override string ToString()
    {
        return string.Format("({0}, {1})", x, y);
    }
}