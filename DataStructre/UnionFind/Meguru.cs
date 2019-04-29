public class UnionFind
{
    int[] parent;
    int count;
    public int Count => count;

    public UnionFind(int size)
    {
        parent = new int[size];
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = -1;
        }
        count = size;
    }

    int Root(int x) => parent[x] < 0 ? x : parent[x] = Root(parent[x]);

    public bool Some(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        count--;
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
