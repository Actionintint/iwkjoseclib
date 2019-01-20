public class UnionFind
{
    int[] parent;
    int[] rank;
    public int Count { get; private set; }

    public UnionFind(int size)
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
        Count--;
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
