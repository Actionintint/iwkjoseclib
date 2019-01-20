public class UnionFind
{
    int[] parent;
    public int Count { get; private set; }

    public UnionFind(int size)
    {
        var b = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            b[i] = -1;
        }
        parent = b;
        Count = size;
    }

    int Root(int x) => parent[x] < 0 ? x : parent[x] = Root(parent[x]);

    public bool Some(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        Count--;
        if (parent[x] < parent[y])
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
}
