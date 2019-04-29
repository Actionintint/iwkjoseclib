public class UnionFind
{
    int[] parent;
    int count;
    public int Count => count;

    public UnionFind(int size)
    {
        parent = new int[size];
        for (int i = 0; i < b.Length; i++)
        {
            parent[i] = i;
        }
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
        parent[x] = y;
    }
}
