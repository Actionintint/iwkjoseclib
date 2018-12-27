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
