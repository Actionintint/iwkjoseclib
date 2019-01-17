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
            weightDiff[y] = w;
        }
    }

    public int Size(int x) => -parent[Root(x)];

    public int Count() => count;
}
