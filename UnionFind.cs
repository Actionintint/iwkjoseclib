class UnionFind
{
    int[] parent;

    public UnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = i;
        }
        parent = b;
    }

    int Root(int x) => parent[x] == x ? x : parent[x] = Root(parent[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        parent[x] = y;
    }
}

class UnionFind2
{
    int[] parent;
    int[] rank;

    public UnionFind2(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = i;
        }
        parent = b;
        rank = new int[size];
    }

    int Root(int x) => parent[x] == x ? x : parent[x] = Root(parent[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        if (rank[x] < rank[y])
        {
            parent[x] = y;
        }
        else
        {
            parent[y] = x;
            if (rank[x] == rank[y]) rank[x]++;
        }
    }
}

class MeguruUnionFind
{
    int[] parent;
    int count;

    public MeguruUnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = -1;
        }
        parent = b;
        count = size;
    }

    int Root(int x) => parent[x] < 0 ? x : parent[x] = Root(parent[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        count--;
        if (parent[x] > parent[y])
        {
            parent[y] += parent[x];
            parent[x] = y;
        }
        else
        {
            parent[x] += parent[y];
            parent[y] = x;
        }
    }

    public int Size(int x) => -parent[Root(x)];

    public int Count() => count;
}

class WeightedUnionFind
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

class WeightedMeguruUnionFind
{
    int[] parent;
    int[] weightDiff;
    int count;

    public WeightedMeguruUnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = -1;
        }
        parent = b;
        weightDiff = new int[size];
        count = size;
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
            weightDiff[x] = w;
        }
    }

    public int Size(int x) => -parent[Root(x)];

    public int Count() => count;
}
