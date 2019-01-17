using System.Collections.Generic;
using System.Linq;

struct Point
{
    public double x;
    public double y;

    public Point(double a, double b)
    {
        x = a;
        y = b;
    }
}

class Vartexs
{
    int Compare(Point p, Point q)
    {
        if (p.x != q.x) return p.x < q.x ? -1 : 1;
        else return p.y < q.y ? -1 : 1;
    }

    public List<Point> ConvexHull(List<Point> ps)
    {
        int n = ps.Count;
        ps.Sort(Compare);
        var k = 0;
        var qs = new List<Point>(2 * n);
        for (int i = 0; i < n; i++)
        {
            while (k > 1 && det(dist(qs[k - 1], qs[k - 2]), dist(ps[i], qs[k - 1])) <= 0) k--;
            qs[k++] = ps[i];
        }
        for (int i = n - 2, t = k; i >= 0; i--)
        {
            while (k > t && det(dist(qs[k - 1], qs[k - 2]), dist(ps[i], qs[k - 1])) <= 0) k--;
            qs[k++] = ps[i];
        }
        return qs.Take(k - 1).ToList();
    }

    double det(Point x, Point y) => x.x * y.y - x.y * y.x;
    Point dist(Point x, Point y) => new Point(x.x - y.x, x.y - y.y);
}