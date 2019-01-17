﻿class WeightedUnionFind
{
    int[] parent;
    int[] rank;
    int[] weightDiff;

    public WeightedUnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = i;
        }
        parent = b;
        rank = new int[size];
        weightDiff = new int[size];
    }

    int Root(int x)
    {
        if (parent[x] == x)
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
        w += Weight(x) - Weight(y);
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        if (rank[x] < rank[y])
        {
            parent[x] = y;
            weightDiff[x] = -w;
        }
        else
        {
            parent[y] = x;
            weightDiff[y] = w;

            if (rank[x] == rank[y]) rank[x]++;
        }
    }
}
