class UnionFind
{
    int[] buffer;

    public UnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = i;
        }
        buffer = b;
    }

    int Root(int x) => buffer[x] == x ? x : buffer[x] = Root(buffer[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        buffer[x] = y;
    }
}

class UnionFind2
{
    int[] buffer;
    int[] rank;

    public UnionFind2(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = i;
        }
        buffer = b;
        rank = new int[size];
    }

    int Root(int x) => buffer[x] == x ? x : buffer[x] = Root(buffer[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        if (rank[x] < rank[y])
        {
            buffer[x] = y;
        }
        else
        {
            buffer[y] = x;
            if (rank[x] == rank[y]) rank[x]++;
        }
    }
}

class MeguruUnionFind
{
    int[] buffer;
    int count;

    public MeguruUnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = -1;
        }
        buffer = b;
        count = size;
    }

    int Root(int x) => buffer[x] < 0 ? x : buffer[x] = Root(buffer[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        count--;
        if (buffer[x] > buffer[y])
        {
            buffer[y] += buffer[x];
            buffer[x] = y;
        }
        else
        {
            buffer[x] += buffer[y];
            buffer[y] = x;
        }
    }

    public int Size(int x) => -buffer[Root(x)];

    public int Count() => count;
}

class WeightedUnionFind//kjc
{
    int[] buffer;
    int[] rank;
    int[] weight;

    public WeightedUnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = i;
        }
        buffer = b;
        rank = new int[size];
        weight = new int[size];
    }

    int Root(int x)
    {
        if (buffer[x] == x)
        {
            return x;
        }
        else
        {
            var r = Root(buffer[x]);
            weight[x] += weight[buffer[x]];
            return buffer[x] = r;
        }
    }

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        if (rank[x] < rank[y])
        {
            buffer[x] = y;
        }
        else
        {
            buffer[y] = x;
            if (rank[x] == rank[y]) rank[x]++;
        }
    }
}
