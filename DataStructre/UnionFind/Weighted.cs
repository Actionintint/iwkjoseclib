using System;

public class WeightedUnionFind
{
    int[] parent;
    int[] weightDiff;
    int count;
    public int Count => count;
    public int Length =>parent.Length;

    public WeightedUnionFind(int length)
    {
        parent = new int[length];
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = -1;
        }
        weightDiff = new int[length];
        count = length;
    }

    int Root(int x)
    {
        if (parent[x] < 0)
        {
            return x;
        }
        else
        {
            var r = Root(parent[x]);
            weightDiff[x] += weightDiff[parent[x]];
            return parent[x] = r;
        }
    }

    int Weight(int x)
    {
        Root(x);
        return weightDiff[x];
    }

    public int Diff(int x, int y) => Weight(y) - Weight(x);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y, int w)
    {
        w += weightDiff[x] - weightDiff[y];
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        count--;
        if (parent[x] > parent[y])
        {
            parent[y] += parent[x];
            parent[x] = y;
            weightDiff[x] = -w;
        }
        else
        {
            parent[x] += parent[y];
            parent[y] = x;
            weightDiff[y] = w;
        }
    }

    public int Size(int x) => -parent[Root(x)];
}
