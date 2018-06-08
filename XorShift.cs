using System;

class XorShift
{
    uint x = 123456789;
    uint y = 362436069;
    uint z = 521288629;
    uint w = 88675123;

    public XorShift()
    {
        var t = (uint)Environment.TickCount;
        x ^= t;
        y ^= Shift(t, 17);
        z ^= Shift(t, 31);
        w ^= Shift(t, 18);
    }

    uint Shift(uint u, int n) => u << n | u >> 32 - n;

    public int Next()
    {
        var t = x ^ x << 11;
        x = y; y = z; z = w;
        t = w = w ^ w >> 19 ^ t ^ t >> 8;
        if (t > int.MaxValue) t = ~t;
        return (int)(t == int.MaxValue ? --t : t);
    }

    public int Next(int maxValue) => (int)(NextDouble() * maxValue);

    public double NextDouble() => (double)Next() / int.MaxValue;
}
