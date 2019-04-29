public class UnionFind
{
    int[] parent;
    int[] rank;
    int count;
    public int Count => count;

    public UnionFind(int size)
    {
        parent = new int[size];
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = i;
        }
        rank = new int[size];
        count = size;
    }

    int Root(int x) => parent[x] == x ? x : parent[x] = Root(parent[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        count--;
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
