﻿using System;

public class UnionFind
{
    int[] parent;
    int count;
    public int Count => count;

    public UnionFind(int length)
    {
        parent = new int[length];
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i] = -1;
        }
        count = length;
    }

    int Root(int x) => parent[x] < 0 ? x : parent[x] = Root(parent[x]);

    public bool Same(int x, int y) => Root(x) == Root(y);

    public void Unite(int x, int y)
    {
        x = Root(x);
        y = Root(y);
        if (x == y) return;
        count--;
        if (parent[x] < parent[y])
        {
            parent[x] += parent[y];
            parent[y] = x;
        }
        else
        {
            parent[y] += parent[x];
            parent[x] = y;
        }
    }

    public int Size(int x) => -parent[Root(x)];
}
