using System;
using System.Collections.Generic;

class Edge//構造体にしたい？したらDFSのgraph[v][i]が参照を返してくれないとダメ（無理）
{
    public int to, capacity, revarse;
}

class FordFulkerson//蟻本式　capacityを更新するので1度しか使えない
{
    List<Edge>[] graph;
    bool[] used;

    public FordFulkerson(int size)
    {
        graph = new List<Edge>[size];
        for (int i = 0; i < size; i++)
        {
            graph[i] = new List<Edge>();
        }
        used = new bool[size];
    }

    public void Add(int from, int to, int capacity)
    {
        graph[from].Add(new Edge { to = to, capacity = capacity, revarse = graph[to].Count });
        graph[to].Add(new Edge { to = from, revarse = graph[from].Count - 1 });
    }

    int DFS(int v, int t, int f)
    {
        if (v == t) return f;
        used[v] = true;
        for (int i = 0; i < graph[v].Count; i++)
        {
            var e = graph[v][i];
            if (!used[e.to] && e.capacity > 0)
            {
                var d = DFS(e.to, t, Math.Min(f, e.capacity));
                if (d > 0)
                {
                    e.capacity -= d;
                    graph[e.to][e.revarse].capacity += d;
                    return d;
                }
            }
        }
        return 0;
    }

    public int MaxFlow(int s, int t)
    {
        var flow = 0;
        while (true)
        {
            used = new bool[used.Length];
            var f = DFS(s, t, int.MaxValue);
            if (f == 0) return flow;
            flow += f;
        }
    }
}

class Dinic//蟻本コピペ
{
    List<Edge>[] graph;
    int[] level;
    int[] iter;

    public Dinic(int size)
    {
        graph = new List<Edge>[size];
        for (int i = 0; i < size; i++)
        {
            graph[i] = new List<Edge>();
        }
        level = new int[size];
        iter = new int[size];
    }

    public void Add(int from, int to, int capacity)
    {
        graph[from].Add(new Edge { to = to, capacity = capacity, revarse = graph[to].Count });
        graph[to].Add(new Edge { to = from, revarse = graph[from].Count - 1 });
    }

    void BFS(int s)
    {
        for (int i = 0; i < level.Length; i++)
        {
            level[i] = -1;
        }
        var q = new Queue<int>();
        level[s] = 0;
        q.Enqueue(s);
        while (q.Count != 0)
        {
            var v = q.Dequeue();
            for (int i = 0; i < graph[v].Count; i++)
            {
                var e = graph[v][i];
                if (e.capacity > 0 && level[e.to] < 0)
                {
                    level[e.to] = level[v] + 1;
                    q.Enqueue(e.to);
                }
            }
        }
    }

    int DFS(int v, int t, int f)
    {
        if (v == t) return f;
        for (int i = iter[v]; i < graph[v].Count; i++)
        {
            var e = graph[v][i];
            if (e.capacity > 0 && level[v] < level[e.to])
            {
                var d = DFS(e.to, t, Math.Min(f, e.capacity));
                if (d > 0)
                {
                    e.capacity -= d;
                    graph[e.to][e.revarse].capacity += d;
                    return d;
                }
            }
        }
        return 0;
    }

    public int MaxFlow(int s, int t)
    {
        var flow = 0;
        while (true)
        {
            BFS(s);
            if (level[t] < 0) return flow;
            iter = new int[iter.Length];
            var f = 0;
            while ((f = DFS(s, t, int.MaxValue)) > 0)
            {
                flow += f;
            }
        }
    }
}
